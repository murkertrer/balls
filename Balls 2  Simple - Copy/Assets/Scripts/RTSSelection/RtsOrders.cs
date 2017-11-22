using UnityEngine;
using System.Collections;

public class RtsOrders : MonoBehaviour {
	Attributes at;
    Camera camera;
    UnitSelectionComponent uSC;
    KeyCode shifter;
	bool offsetMovement = false;


	void Start () {
		at.GetComponent<Attributes> ();
        camera = GetComponent<Camera>();
        uSC = GetComponent<UnitSelectionComponent>();
        shifter = KeyCode.LeftShift;
    }

    // Update is called once per frame
    void Update()
    {
		/*
		if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                foreach (var controlledObjects in uSC.selectedObjects)
                {
                    if (controlledObjects.transform.root.GetComponent<RtsMovement>())
                    { 
						if (offsetMovement) {
							if (uSC.selectedObjects.Count > 1) {
								//Find the Average of VEctors, then find the offset of current object from this one, 
								//Pass the hit point, as the hitpoint, plus the offset 
								//Vector3 offsetToTarget = transform.position - uSC.selectedObjects[0].transform.position;
								//transform.position = lockedPlayer.transform.position + offsetToTarget;
								//controlledObjects.transform.
							}							
						} else {
							controlledObjects.transform.root.GetComponent<RtsMovement> ().SetUniqueDestination (hit.point);
						}
                    }
                }
            }
        }
        */


   
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
			// System.Collections.Generic.List<GameObject> AverageVectors = new System.Collections.Generic.List<GameObject> ();
				//AverageVectors = uSC.selectedObjects;
				Vector3 averagepositions = new Vector3(0,0,0);

				for (int i = 0; i < uSC.selectedObjects.Count; i++) {
					averagepositions += uSC.selectedObjects [i].transform.position;
				}
				averagepositions = averagepositions / uSC.selectedObjects.Count;

				//GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				//cube.transform.position = avergaPositions;

                foreach (var controlledObjects in uSC.selectedObjects)
                {

					if (offsetMovement) {

						Vector3 offset = new Vector3 (0,0,0);
						offset = averagepositions - controlledObjects.transform.position;
						controlledObjects.transform.root.GetComponent<RtsMovement>().SetMultipleDestinations(hit.point-(offset/2));
						
					} else {
						controlledObjects.transform.root.GetComponent<RtsMovement>().SetMultipleDestinations(hit.point);
					}
                 
                }
            }
        }
		/*
        if (Input.GetKey(at.shifter));
        {
            //Whatt allways pressed???
            if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
            {
                print("now");

                RaycastHit hit2;
                Ray ray2 = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out hit2, 100.0f))
                {
                    foreach (var controlledObjects in uSC.selectedObjects)
                    {
                        if (controlledObjects.transform.root.GetComponent<RtsAiming>())
                        {
                            controlledObjects.transform.root.GetComponent<RtsAiming>().LookAtPoint(hit2.point);
                        }
                    }
                }
            }
        }
        */
    }
    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(at.shifter))
        {
            RaycastHit hit2;
            Ray ray2 = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray2, out hit2, 100.0f))
            {
                foreach (var controlledObjects in uSC.selectedObjects)
                {
                    if (controlledObjects.transform.root.GetComponent<RtsAiming>())
                    {
                        controlledObjects.transform.root.GetComponent<RtsAiming>().LookAtPoint(hit2.point);
                    }
                }
            }
        }
    }
}
