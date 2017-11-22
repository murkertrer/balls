using UnityEngine;
using System.Collections;

public class OrientationChangerCube : MonoBehaviour {
	public Transform playerAttract;
	public bool stopAttractionIfExitedCollision;

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.tag == "Player") {
			if (col.transform.root.GetComponent<SpaceManipulation> ()) {
				SpaceManipulation sm = col.transform.root.GetComponent<SpaceManipulation> ();
				if (sm.planeReference != transform) {
					col.transform.root.GetComponent<SpaceManipulation> ().ActivatePlaneGravity (transform);
				}
			}
		}			
	}
	void OnCollisionExit(Collision col)
	{
		if (stopAttractionIfExitedCollision) {
			CancelPlayerAttraction (col);
		}
	}
	void CancelPlayerAttraction(Collision col)
	{
		if (col.transform.tag == "Player") {

			print ("exited Collision");
			if (col.transform.root.GetComponent<SpaceManipulation> ()) {
				col.transform.root.GetComponent<SpaceManipulation> ().RotateToWorld ();
				col.transform.root.GetComponent<SpaceManipulation> ().DeActivateArtificialGravity ();
			}
		}
	}
}
