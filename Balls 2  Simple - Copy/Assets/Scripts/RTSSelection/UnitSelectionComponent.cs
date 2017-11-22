using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
   bool isSelecting = false;
   Vector3 mousePosition1;
   Camera camerax;
   KeyCode shifter;

   public List<SelectableUnitComponent> selectedObjects = new System.Collections.Generic.List<SelectableUnitComponent>();
    //List<Transform> myList = new System.Collections.Generic.List<Transform>();
    public GameObject selectionCirclePrefab;
    void OnEnable()
    {
        camerax = GetComponent<Camera>();
    }
    void Start()
    {
       shifter = KeyCode.LeftShift;
    }
    void Update()
    {
		if (!Input.GetKey(shifter) && !Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.CapsLock))
        {
            // If we press the left mouse button, begin selection and remember the location of the mouse
            if( Input.GetMouseButtonDown( 0 ) )
            {
                isSelecting = true;
                mousePosition1 = Input.mousePosition;

                foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
                {
                    if( selectableObject.selectionCircle != null )
                    {  
						selectableObject.transform.root.GetComponent<Attributes> ().DestroySelectionCircle ();

                        //Destroy( selectableObject.selectionCircle.gameObject );
                        selectableObject.selectionCircle = null;              
                    }
                }
            }
            if( Input.GetMouseButtonUp( 0 ))
            {
                selectedObjects.Clear();
                foreach ( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
                {
                    if( IsWithinSelectionBounds( selectableObject.gameObject ) )
                    {
                        if (!selectedObjects.Contains(selectableObject))
                        {
                            selectedObjects.Add( selectableObject );

							//Asigning thi cam To PLayer For their Rts Stuff
							selectableObject.transform.root.GetComponent<Attributes>().AssingRtsCam(transform);
							selectableObject.selectionCircle.transform.root.GetComponent<Attributes> ().Select ();


                        }
                    }
                }
                var sb = new StringBuilder();
                sb.AppendLine( string.Format( "Selecting [{0}] Units", selectedObjects.Count ) );
                foreach( var selectedObject in selectedObjects )
                    sb.AppendLine( "-> " + selectedObject.gameObject.name );
                Debug.Log( sb.ToString() );
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
                            selectableObject.selectionCircle = Instantiate( selectionCirclePrefab );
							//selectableObject.transform.root.GetComponent<Attributes> ().GetSelectionCircle (selectableObject.gameObject);
							//Destroy( selectableObject.selectionCircle.gameObject );
                            selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
                            selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );
							selectableObject.selectionCircle.transform.root.GetComponent<Attributes> ().GetSelectionCircle (selectableObject.selectionCircle);

                        }
                    }
                    else
                    {
                        if( selectableObject.selectionCircle != null )
                        {
                            Destroy( selectableObject.selectionCircle.gameObject );
                            selectableObject.selectionCircle = null;
                        }
						selectableObject.transform.root.GetComponent<Attributes> ().DeSelect  ();

                    }
                }
            }
        }
    }
    public bool IsWithinSelectionBounds( GameObject gameObject )
    {
        if( !isSelecting )
            return false;        
        var viewportBounds = Utils.GetViewportBounds( camerax, mousePosition1, Input.mousePosition );
        return viewportBounds.Contains( camerax.WorldToViewportPoint( gameObject.transform.position ) );
    }
    void OnGUI()
    {
		if (!Input.GetKey (shifter) && !Input.GetKey (KeyCode.LeftControl) ) {
			//&& Input.GetKey(KeyCode.CapsLock)
			if (isSelecting) {
				// Create a rect from both mouse positions
				var rect = Utils.GetScreenRect (mousePosition1, Input.mousePosition);
				Utils.DrawScreenRect (rect, new Color (0.8f, 0.8f, 0.95f, 0.25f));
				Utils.DrawScreenRectBorder (rect, 2, new Color (0.8f, 0.8f, 0.95f));
			}
		}
    }
    public void GetWakeUpUnits(SelectableUnitComponent it)
    {
        //selectedObjects.Add(it);
    }

	public void DeletUnitSelectionComponentsForEachSelectedobject()
	{
	}

}