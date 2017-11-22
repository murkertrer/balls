using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadarOne : MonoBehaviour {
	public float radarSize = 30;

	public Transform bd2;
	public Transform radarCardinality;
	public bool showProyectiles;
	public bool showLines;
	public Transform playerCardinality;
	public Transform radarPlayer;
	public Transform ball;
	public List<GameObject> radarObjects = new List<GameObject>  ();
	public List<GameObject> radarObjectsOriginals = new List<GameObject>  ();
	public GameObject tmp;
	public Test rdrO;
	float radarTriggerSize;
	float radarSizeBoundary = 10;
	float radarMin = 1;
	public Material proyectileMat;
	public Material enemyMat;
	public Material allyMat;
	public Material enemyMatProyectile;
	public Material allyMatProyectile;
	public float radarObjTransparency = .4f;
	public Vector3 lclScale = new Vector3 (.05f, .05f, .05f);
	public Vector3 lclScaleProyectiles = new Vector3 (.05f, .05f, .05f);
	public Transform ballDummy;
	public Transform radarDummy;
	public bool externalDisplayRadar = true;
	float radarDummyBounds;
	float radius ;
	bool active;
	public Transform actualRadar;
	Attributes at;
	public bool radarOn;


	void Start () {
		 radarDummyBounds = radarDummy.GetComponent<Renderer> ().bounds.extents.magnitude;
		at = ball.root.GetComponent<Attributes> ();
		actualRadar = at.actualRadar;
		GetComponent<SphereCollider> ().radius = radarSize;
		radius = GetComponent<SphereCollider> ().radius;
		//Check Formula here, to make localscale according to Radius 
		at.radarShow.transform.localScale = new Vector3(radius,radius,radius);
	}

	void Update()
	{
		if (Input.GetKeyDown (at.radarKey)) {
			//radarOn = !radarOn;

			at.radarShow.gameObject.SetActive (true);


			if (!radarOn) {
				DeActivateRadar ();
			} else {
				ActivateRadar ();
			}
		}


		if (Input.GetKeyUp (at.radarKey)) {
			at.radarShow.gameObject.SetActive (false);
		}

		if (showLines) {
		
			RenderLines ();
		}
	}
	public void ActivateRadar()
	{
		
		radarPlayer.gameObject.SetActive (true);
		if (radarObjects.Count > 0) {
			for (int i = 0; i < radarObjects.Count; i++) {
				if (radarObjects [i]) {
					
					radarObjects [i].SetActive (true);
				}
			}
		}
		active = true;

	}
	public void DeActivateRadar()
	{
		radarPlayer.gameObject.SetActive (false);
		if (radarObjects.Count > 0) {
			for (int i = 0; i < radarObjects.Count; i++) {
				if (radarObjects [i]) {
					radarObjects [i].gameObject.SetActive (false);
				}
			}
		}
		active = false;
	}	
	void LateUpdate()
	{	
		if (active) {
			ModifyPos ();
		}
		if (showLines) {
			RenderLines ();
		}
	}
	void ModifyPos()
	{
		if (radarObjects.Count > 0) {
			for (int i = 0; i < radarObjects.Count; i++) {
				if (radarObjectsOriginals [i]) {
					float distance = Vector3.Distance (this.transform.position, radarObjectsOriginals [i].transform.position);
					float percentage = (radius - (radius - distance)) / radius;
					if (externalDisplayRadar) {
						if (radarObjects [i]) {
							radarObjects [i].SetActive (true);
							float distanceToWorkWith = radarDummyBounds;
							float relativeDistance = distanceToWorkWith * percentage;
							Vector3 relativePos = ball.transform.position - radarObjectsOriginals [i].transform.position;
							Quaternion rotation = Quaternion.LookRotation (relativePos);
							Vector3 negDistance = new Vector3 (0.0f, 0.0f, -relativeDistance);
							Vector3 position = rotation * negDistance + ballDummy.transform.position;

							//position = Quaternion.AngleAxis (at.actualRadar.transform.localEulerAngles.y, at.actualRadar.transform.up)*position;
							//Change according to Radar

							//Vector3 RelativePos2 = at.radarCardinality - radarObjects [i].transform.position;
							//Vector3 position2 = at.radarCardinality.transform.rotation

							//position = at.actualRadar.localRotation * position;

							radarObjects [i].transform.position = position;
							radarObjects [i].transform.rotation = radarObjectsOriginals [i].transform.rotation;
						}
					} 
				}
			}
		}
	}
	//rotation = ballDummy.transform.rotation * rotation;
	void RenderLines()
	{

		for (int i = 0; i < radarObjects.Count; i++) {
			if (radarObjectsOriginals [i]) {


				print ("render lines11111");
				Vector3 directionToTarget = ball.transform.position - radarObjectsOriginals [i].transform.position;
				Color myColor = Color.white;

				float angle = Vector3.Angle (ball.parent.GetComponent<Attributes> ().rotator.forward, directionToTarget);
				if (Mathf.Abs (angle) > 90) {

					myColor.g = 1;
					radarObjects [i].GetComponent<LineRenderer> ().SetColors (Color.yellow, myColor);
				} else {
					radarObjects [i].GetComponent<LineRenderer> ().SetColors (Color.red, Color.red);

				}

				float angle2 = Vector3.Angle (ball.parent.GetComponent<Attributes> ().rotator.right, directionToTarget);
				if (Mathf.Abs (angle2) > 90) {
				
				} else {
				
				}


				radarObjects [i].GetComponent<LineRenderer> ().SetPosition (0, radarObjects [i].transform.localPosition);
				radarObjects [i].GetComponent<LineRenderer> ().SetPosition (1, bd2.transform.position);
			} else {
				DisposeOfRadarObj (i);
				//Destroy (radarObjects [i]);
			}
		}
	}
	public RadarPosition rp;
	void OnTriggerEnter(Collider col)
	{
		if ((col.tag == "Player" && col.transform != ball) || col.tag == "Proyectile") {
			Tmp (col);
		}
		
		//rp.GotCollider(col);
	}
	void Tmp(Collider col)
	{

		//print ("Radar" + col.gameObject);
		if (col.transform != ball && col.isTrigger == false) {

			print ("spawning proyectiles444");

			Vector3 relativePos = col.transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			Vector3 negDistance = new Vector3(0.0f, 0.0f, 2);
			Vector3 position = rotation * negDistance + ball.transform.position;
			Test rdrOCopy = Instantiate<Test> (rdrO);
			//rdrOCopy.transform.parent = at.radarCardinality;

			rdrOCopy.transform.gameObject.layer = LayerMask.NameToLayer ("OnlyOrtho");


			if (showLines) {
				rdrOCopy.GetComponent<LineRenderer> ().enabled = true;
			}




			if (col.tag == "Proyectile") {
				if (showProyectiles) {
					Color a = proyectileMat.color;
					a.a = .4f;
					if (col.GetComponent<ThingAttributes> ().team == transform.root.GetComponent<Attributes> ().team) {
						proyectileMat.color = Color.green;
					} else {
						proyectileMat.color = Color.red;
					}
					rdrOCopy.GetComponent<Renderer> ().material = proyectileMat;
					rdrOCopy.transform.localScale = lclScaleProyectiles;
					//rdrOCopy.transform.GetComponent<LineRenderer> ().enabled = false;
				}

			
			}
			if (col.tag == "Player") {

				if (col.transform.root.GetComponent<CreatureAttributes> ()) {

					if (col.transform.root.GetComponent<CreatureAttributes> ().team == transform.root.GetComponent<Attributes> ().team) {
						rdrOCopy.GetComponent<Renderer> ().material = allyMat;

					} else {
						rdrOCopy.GetComponent<Renderer> ().material = enemyMat;

					}
				}



				rdrOCopy.transform.localScale = lclScale ;
			}
			rdrOCopy.transform.GetComponent<MeshFilter> ().mesh = col.GetComponent<MeshFilter> ().mesh;
			rdrOCopy.transform.position = position;
			rdrOCopy.gameObject.SetActive (false);	//what is this?
			//rdrOCopy.gameObject.AddComponent<RadarObjectBehave> ();
			//rdrOCopy.gameObject.GetComponent<RadarObjectBehave> ().SetUp (ball, col.transform, radarDummyBounds, radius, ballDummy);
			radarObjects.Add (rdrOCopy.gameObject);
			radarObjectsOriginals.Add (col.gameObject);

			if (col.gameObject.GetComponent<ObjectHandle> ()) {
				//Notify Object that when it d it needs to speek tiwh 
				col.gameObject.GetComponent<ObjectHandle> ().IamThisRadarsObject = this;

			} else {
				print ("Did not find  Onject Handle, If the object is destroy within radar it wont be deleated from radar");
			}
		}
	}

	public void RecieveNotificationFromRadarsOriginalObject(GameObject it)
	{
		EqualizeRadarObject (it);
	}

	void  DisposeOfRadarObj(int i)
	{
		if (radarObjects [i] && radarObjects [i].gameObject) {


		}


		//Have to fix how is it that some object are being distroyed without radar consent
	}
	void  EqualizeRadarObject(GameObject wow)
	{
		for (int i = 0; i < radarObjects.Count; i++) {
			if (radarObjectsOriginals [i] == wow) {
				Destroy (radarObjects [i]);
				radarObjects.Remove (radarObjects [i].gameObject);
				radarObjectsOriginals.Remove (radarObjectsOriginals [i].gameObject);		
			}
		}
	}
	void OnTriggerExit(Collider col)
	{
		if ((col.tag == "Player" && col.transform != ball) || col.tag == "Proyectile") {
			if (radarObjects.Count > 0) {
				if (col.gameObject.GetComponent<ObjectHandle> ().RemoveRadarNotificationAfterExtition () == true) {
					EqualizeRadarObject (col.gameObject);

				} else {			
					EqualizeRadarObject (col.gameObject);
				}
			}
		}
	}
}
