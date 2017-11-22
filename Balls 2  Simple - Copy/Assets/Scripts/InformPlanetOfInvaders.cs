using UnityEngine;
using System.Collections;

public class InformPlanetOfInvaders : MonoBehaviour {
	ApplyNewtonianForce anf;
	void OnEnable()
	{
		anf = transform.parent.GetComponent<ApplyNewtonianForce> ();
	}
	void OnTriggerEnter(Collider col)
	{
		anf.OnTriggerEnter2 (col);
	}
	void OnTriggerStay(Collider col)
	{
	//	pp.OnTriggerStay2 (col);
	}
	void OnTriggerExit(Collider col)
	{
		anf.OnTriggerExit2 (col);
	}
}
