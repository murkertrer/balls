using UnityEngine;
using System.Collections;

public class GenericSpaceManipulation : MonoBehaviour {

	Transform planeAlign;
	float gravity = 9.8f;
	Rigidbody rb;
	Vector3 rBVel;
	Vector3 newRBVel;
	bool gravityUser;
	Transform direct;
	bool gotAPlane;
	PlanetPower pp;
	Transform forceThatAttracts;
	bool planetaryGravity;


	public void EstablishPlane (Transform it)
	{
		planeAlign = it;
		if (this.GetComponent<Rigidbody> ()) {
			rb = this.GetComponent<Rigidbody> ();

			if (rb.useGravity) {
				gotAPlane = true;
				gravityUser = true;
				rb.useGravity = false;
			}
			if (gotAPlane) {
			}
		}
	}

	public void EstablishPlanet(Transform planet, PlanetPower it)
	{
		if (this.GetComponent<Rigidbody>())
		{
		pp = it;
		forceThatAttracts = planet;
		this.GetComponent<Rigidbody>().useGravity = false;
		//StartCoroutine (RotationNewPlane (attractor.transform.rotation, 2));
		planetaryGravity = true;
		}
	}
	void FixedUpdate()
	{
		PlanetaryAttracion ();
	}
	void PlanetaryAttracion()
	{
		if (pp && planetaryGravity) {
			//pp.AttractBastard (this.transform);
			/*
			Vector3 gravityUp = (this.transform.position - pp.transform.position).normalized;
			Vector3 localUp = this.transform.up;
			Quaternion targetRotation = Quaternion.FromToRotation (localUp, gravityUp) * armorFlip.rotation;
			armorFlip.rotation = Quaternion.Slerp (armorFlip.rotation, targetRotation, 50f * Time.deltaTime);
			*/
		}
	}

	// Update is called once per frame
	void Update () {
		if (gotAPlane) {
			Vector3 gravityUp = planeAlign.transform.up;
			if (this.GetComponent<Rigidbody> ()) {

				if (gravityUser) {
					this.GetComponent<Rigidbody> ().AddForce (planeAlign.transform.up* gravity * rb.mass);
				}

				//this.GetComponent<Rigidbody> ().AddForce (planeAlign.transform.up * gravity);
				//this.GetComponent<Rigidbody> ().AddForce (newRBVel);
			}
			//this.transform.transform.rotation = attractor.transform.rotation * Quaternion.AngleAxis (180, Vector3.forward);
		}
	}

	public void EndGenericEnterRealWorld()
	{
		if (gravityUser) {
			rb.useGravity = true;
			planeAlign = null;
			Destroy (this);
		}
	}
}
