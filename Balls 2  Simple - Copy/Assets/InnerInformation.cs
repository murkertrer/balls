using UnityEngine;
using System.Collections;

public class InnerInformation : MonoBehaviour {

	ApplyNewtonianForce anf;
	void OnEnable ()
	{
		anf = transform.root.GetComponent<ApplyNewtonianForce> ();
	}
	void OnTriggerExit (Collider col) {
		if (transform.parent.GetComponent<HandlePlanetBirth> ().haveGrown == true) {
			if (anf)
			{
				if (col.GetComponent<Rigidbody> ()) {
					anf.OnInnerTriggerExit (col);
				}
			}				
		}
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<Rigidbody> () && col.transform.root.GetComponent<Attributes> ()) {
			anf.OnInnerTriggerEnter (col);
		}
	}
}
