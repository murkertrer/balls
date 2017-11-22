using UnityEngine;
using System.Collections;

public class HandlePlanetBirth : MonoBehaviour {
	public bool haveGrown;
	public Vector3 growScale = new Vector3 (3, 3, 3);
	public float littleTimeOfActivation = 1;
	public bool isAmmo = true;
	float planetMass;
	void Start()
	{
		if (isAmmo) {
			StartCoroutine (LittleWait ());

		}
	}
	void OnCollisionEnter () {
	
		if (!haveGrown) {
			float radius;
			Renderer rend;
			rend = GetComponent<Renderer> ();
			radius = rend.bounds.extents.y;

			///*****
			float mass = (4 / 3) * (Mathf.PI) * (radius) * (radius) * (radius);
			AssignPlanetMass ();
			GetComponent<ApplyNewtonianForce> ().AssignMass (planetMass);


			GetComponent<Rigidbody> ().isKinematic = true;
			GetComponent<ApplyNewtonianForce> ().enabled = true;
			StartCoroutine (GrowALittle (2, growScale));

		}
	}

	public void AssignPlanetMass ()
	{
		planetMass = GetComponent<AssignRbMassAccordingToSize> ().GetPlanetMass();
		if (GetComponent<Rigidbody> ()) {
			Rigidbody rb = GetComponent<Rigidbody> ();
			rb = GetComponent<Rigidbody> ();
			rb.mass = planetMass;
		} 
	}
	IEnumerator LittleWait()
	{
		yield return new WaitForSeconds (littleTimeOfActivation);
		yield return null;
	}
	IEnumerator GrowALittle(float time, Vector3 it)
	{
		float elapsedTime = 0;
		while (elapsedTime < time) {
			transform.localScale= Vector3.Lerp (transform.localScale, it, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		//triggerNotify.GetComponentInChildren<InformPlanetOfBeings> ().isARealPlanet = true;;
		haveGrown = true;
	}

	public Transform triggerNotify;
	bool isARealPlanet;
	public float magnitudeWhenItBecomesAPlanet = 1;

	void Update () {
		/*
		if(!isARealPlanet)
		{
			if (this.transform.localScale.magnitude > magnitudeWhenItBecomesAPlanet) {
				isARealPlanet = true;
			}
		}
		*/
	}
}

