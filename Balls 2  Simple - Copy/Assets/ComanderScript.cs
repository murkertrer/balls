using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ComanderScript : MonoBehaviour {
	Attributes at;
	PlayerHandle ph;
	Vector3 mousePosition1;
	Vector3 aimObjPosition1;
	bool isSelecting = false;
	bool commandingUnits;
	public List<SelectableUnitComponent> commandoSelectedObjects = new System.Collections.Generic.List<SelectableUnitComponent>();
	public GameObject selectionCirclePrefab;
	float y2;
	float x2;
	float z;
	public Color commanderColor;
	public Color controllerColor;
	public SpriteRenderer sprite;

	void OnEnable()
	{
		at = GetComponent<Attributes> ();
		ph = at.GetComponent<PlayerHandle> ();

		x2 = Screen.width / 2;
		y2 = Screen.height / 2;
		z = at.aimObjCommander.localPosition.z;
		sprite = at.aimObjCommander.GetComponent<SpriteRenderer> ();
	}
	void Update()
	{
		if (at.iAmFPSPlayer) {

			//Make Appropiate UI Notifications


			if (Input.GetKeyDown (at.commander)) {

				at.aimObjCommander.gameObject.SetActive (true);
				sprite.color = at.commanderColor;

				if (Input.GetKey (at.controller)) {
					sprite.color = at.controllerAndCommanderColor;
				}

			}
			if (Input.GetKey (at.commander)) {
				MoveAimObjCommander ();

				if (!Input.GetKey (at.controller)) {
					Select ();
				}

				CommandoMovement ();
			}
			if (Input.GetKeyUp (at.commander)) {
				isSelecting = false;	
				if (!Input.GetKey (at.controller)) {
					at.aimObjCommander.gameObject.SetActive (false);
					ResetAimCommander ();

				} else {
				
					sprite.color = at.controllerColor;
				}
			}

			if (Input.GetKeyDown (at.controller)) {
				sprite.color = at.controllerColor;
				at.aimObjCommander.gameObject.SetActive (true);

				if (Input.GetKey (at.commander)) {
					sprite.color = at.controllerAndCommanderColor;
				}

			}
			if (Input.GetKeyUp (at.controller)) {
				if (!Input.GetKey (at.commander)) {
					at.aimObjCommander.gameObject.SetActive (false);
					ResetAimCommander ();
				} else {
					sprite.color = at.commanderColor;
				
				}

			}
		}
	}
	void LateUpdate()
	{
		if (at.iAmFPSPlayer) {
			if (Input.GetKey (at.controller)) {
				CommandoControl ();
			}
			if (at.gimBall && at.isSelected) {
				if (Input.GetMouseButtonDown (1)) {
					if (!Input.GetKey (at.commander) && !Input.GetKey (at.controller)) {
						Ray ray = new Ray (at.myCam.transform.position, (at.aimObj.transform.position - at.myCam.transform.position));
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit, 1000, ~2)) {
							at.rtsM.SetMultipleDestinations (hit.point);				
						}
					}
				}
			}
		}
	}
	void CommandoControl()
	{
		Ray ray = new Ray (at.myCam.transform.position,( at.aimObjCommander.transform.position - at.myCam.transform.position));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000, ~2)) {
			if (commandoSelectedObjects.Count > 0) {
				//at.kT.GetPointInQuestion (hit.point);
				foreach (var selectableObject in commandoSelectedObjects) {	
					if (Input.GetKey (at.controller)) {
						selectableObject.transform.root.GetComponent<AimingSystemSimpleDouble> ().LookAtSaysCommander (hit.point);
						at.aSSD.LookAtPointInQuestion (hit.point);
					}

					if (Input.GetMouseButton (0)) {
						//selectableObject.transform.root.GetComponent<GenericThrowControl> ().ActivateExpelFromACommander ();
					}

					if (Input.GetMouseButtonDown (1)) {
						//change waypoint tollerance according to number;
						RtsMovement rtsm = selectableObject.transform.root.GetComponent<RtsMovement> ();
						rtsm.SetMultipleDestinations (hit.point);
						rtsm.ChangeWayPointTolerance (commandoSelectedObjects.Count);
						//Also move myself
						at.rtsM.SetMultipleDestinations (hit.point);
						//Vector3 offsetFromPoint = hit.point -  selectableObject.transform.position;
						//KEEP Origianl Distances
						//Make Formations
						//selectableObject.transform.root.GetComponent<RtsMovement> ().SetMultipleDestinations (offsetFromPoint);
						//
					}
				}
			} else {
				if (Input.GetMouseButtonDown (1)) {
					at.rtsM.SetMultipleDestinations (hit.point);
				}
			}
		}
	}
	void Select()
	{
		// If we press the left mouse button, begin selection and remember the location of the mouse
		if( Input.GetMouseButtonDown( 0 ) )
		{
			isSelecting = true;
			mousePosition1 = at.myCam.WorldToScreenPoint( at.aimObjCommander.transform.position);;

			foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
			{
				if( selectableObject.selectionCircle != null )
				{
					Destroy( selectableObject.selectionCircle.gameObject );
					selectableObject.selectionCircle = null;
				}
			}
		}
		// If we let go of the left mouse button, end selection
		if( Input.GetMouseButtonUp( 0 ) )
		{
			// A BETTER WAY TO CLEAR COMMANDO SELECTED OBJECTS?
			commandoSelectedObjects.Clear();
			foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					commandoSelectedObjects.Add( selectableObject );
					print (selectableObject.gameObject.name);
				}
			}

			//INFORM UI OF SELECTED OBJECTS;
			/*
			var sb = new StringBuilder();
			sb.AppendLine( string.Format( "Selecting [{0}] Units", commandoSelectedObjects.Count ) );
			foreach( var selectedObject in commandoSelectedObjects )
				sb.AppendLine( "-> " + selectedObject.gameObject.name );
			Debug.Log( sb.ToString() );
			*/

			isSelecting = false;
		}

		// Highlight all objects within the selection box
		if( isSelecting )
		{
			foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					if( selectableObject.selectionCircle == null )
					{
						//selectableObject.selectionCircle = Instantiate( selectionCirclePrefab );
						//selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
						//selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );
					}
				}
				else
				{
					if( selectableObject.selectionCircle != null )
					{
						//Destroy( selectableObject.selectionCircle.gameObject );
						//selectableObject.selectionCircle = null;
					}
				}
			}
		}
	}
	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		if( !isSelecting )
			return false;

		var camera = at.myCam;
		var viewportBounds = Utils.GetViewportBounds( camera, mousePosition1, at.myCam.WorldToScreenPoint( at.aimObjCommander.transform.position));
		return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
	}
	void OnGUI()
	{
		if( isSelecting )
		{
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect( mousePosition1, at.myCam.WorldToScreenPoint( at.aimObjCommander.transform.position) );
			Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
		}
	}
	void MoveAimObjCommander()
	{

		Vector2 screenPos = at.myCam.WorldToViewportPoint (at.aimObjCommander.transform.position);
		if (screenPos.y < .9f && screenPos.y > .1f) {
			y2 += Input.GetAxis ("Mouse Y") * 10;
		}

		//Levleing mechanism
		if (screenPos.y > .9f) {
			y2 += -1.3f;
		}
		if (screenPos.y < .1f) {
			y2 += 1.3f;
		}
		if (screenPos.x < .9f && screenPos.x > .1f) {
			if (!Input.GetMouseButtonDown (1)) {
			}
			if (!Input.GetMouseButton (1)) {		
				x2 += Input.GetAxis ("Mouse X") * 10;
			}
		}
		if (screenPos.x > .9f) {
			x2 += -1.3f;
		}
		if (screenPos.x < .1f) {
			x2 += 1.3f;
		}

		Vector3 screenPoint = new Vector3 (x2, y2, z);
		Vector3 WorldPos = at.myCam.ScreenToWorldPoint (screenPoint);
		at.aimObjCommander.transform.position = WorldPos;	
	}
	public void ResetAimCommander()
	{		
		x2 = Screen.width / 2;
		y2 = Screen.height / 2;

		Vector3 screenPoint = new Vector3 (x2, y2, z);
		Vector3 WorldPos = at.myCam.ScreenToWorldPoint (screenPoint);
		at.aimObjCommander.transform.position = WorldPos;	

	}
	public void CommandoMovement()
	{
		Ray ray = new Ray (at.myCam.transform.position, (at.aimObjCommander.transform.position - at.myCam.transform.position));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000, ~2)) {
		if (commandoSelectedObjects.Count > 0) {
			//Cahnge To Comando Controller
				if (Input.GetKey (KeyCode.LeftControl)) {}
				/*
				if (Input.GetMouseButton (0)) {
					foreach (var selectableObject in commandoSelectedObjects) {	
						//How to get point in question and pass it first time?
						at.kT.GetPointInQuestion(hit.point);
						selectableObject.transform.root.GetComponent<AimingSystemSimpleDouble> ().LookAtPointInQuestion (hit.point);
					}
				}
				*/
			
			if (Input.GetMouseButtonDown (1)) {


				if (Input.GetKey (at.commander)) {}
				foreach (var selectableObject in commandoSelectedObjects) {					
					selectableObject.transform.root.GetComponent<RtsMovement> ().SetMultipleDestinations (hit.point);
				}
					/*
				 else {


					foreach (var selectableObject in commandoSelectedObjects) {					
						selectableObject.transform.root.GetComponent<RtsMovement> ().SetUniqueDestination (hit.point);
					}
				}
				*/
			}
		} 
		else {
				//iF ONLY ME

				/*

			if (Input.GetMouseButtonDown(1)) {
				if (Input.GetKey (at.shifter)) {
					GetComponent<RtsMovement> ().SetMultipleDestinations (hit.point);

				} else {
					GetComponent<RtsMovement> ().SetMultipleDestinations (hit.point);
				}
			}

*/
		}
	}
} 

}

