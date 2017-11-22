using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InformPlanetOfBeings : MonoBehaviour {

	//public List<Collider> objtAllreadyNotifiedAsStayers = new List<Collider> ();
	public bool isARealPlanet;
	public Transform planet;
	ApplyNewtonianForce anf;
	void OnEnable ()
	{
		anf = transform.root.GetComponent<ApplyNewtonianForce> ();
	}
	void OnTriggerEnter (Collider col) {
		if (transform.parent.GetComponent<HandlePlanetBirth> ().haveGrown == true) {
			if (col.GetComponent<Rigidbody> ()) {
				if (anf) {
					anf.AddObject (col.gameObject);
				}

			}
			if (isARealPlanet) {
				anf.OnTriggerEnter2 (col);
			}
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (transform.parent.GetComponent<HandlePlanetBirth> ().haveGrown == true) {
			if (col) {
				if (col.GetComponent<Rigidbody> ()) {
					if (anf) {				
						anf.RemoveObject (col.gameObject);
					}
				}
				anf.OnInnerTriggerExit (col);
			}
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (transform.parent.GetComponent<HandlePlanetBirth> ().haveGrown == true) {	
			if (anf) {
				if (col.GetComponent<Rigidbody> ()) {
					anf.AddObject (col.gameObject);
				}
			}
		}
	}
}
