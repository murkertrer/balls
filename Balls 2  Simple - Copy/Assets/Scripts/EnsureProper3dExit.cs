using UnityEngine;
using System.Collections;

public class EnsureProper3dExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			if (col.transform.parent.GetComponent<SpaceManipulation>())
			{
				col.transform.parent.GetComponent<SpaceManipulation> ().DeActivateArtificialGravity ();
			}
		}
		else
		{
			if (col.transform.GetComponent<GenericSpaceManipulation> ()) {
				col.GetComponent<GenericSpaceManipulation> ().EndGenericEnterRealWorld ();
			}
		}
	}
}
