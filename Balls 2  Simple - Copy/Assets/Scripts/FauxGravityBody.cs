using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttractor attractor;

	private Transform myTransform;

	public Object point;

	public Transform armorFlip;

	void Start () {
		GetComponent<Rigidbody>().useGravity = false;
		//GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		myTransform = transform;
	}

	void FixedUpdate () {
		if (attractor){
			attractor.Attract(myTransform);
			/*
			Vector3 gravityUp = (armorFlip.position - attractor.transform.position).normalized;
			Vector3 localUp = armorFlip.up;
			Quaternion targetRotation = Quaternion.FromToRotation(localUp,gravityUp) * armorFlip.rotation;
			armorFlip.rotation = Quaternion.Slerp (armorFlip.rotation, targetRotation, 50f * Time.deltaTime);
			//armorFlip.rotation = Quaternion.FromToRotation (armorFlip.transform.up, attractor.GetRotation(armorFlip), 50f * Time.deltaTime);
			*/
		}
	}
}
