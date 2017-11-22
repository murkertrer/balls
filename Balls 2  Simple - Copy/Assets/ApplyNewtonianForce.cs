using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyNewtonianForce : MonoBehaviour {

	public List<GameObject> rbUnderInfluence = new List<GameObject> ();
	public List<Rigidbody> rbUnderPlanetInfluence = new List<Rigidbody> ();

	float g = 9.8f;
	float planetMass;
	public float gravitationalForce = -1;
	public float minimumSize = 1;
	float timeOfReaching;
	float explosionTime;
	public bool outForce;
	public bool temporal;
	public float lifeSpan = 20;
	Rigidbody rb;
	public bool influenceBack;
	bool fistCollision;

	///*** Planet Behave
	public bool planet;
	public bool plane;
	public float gravity = -9.8f;
	bool currentlyRotating = false;
	public float distanceFromPlanetToRotatePlayer = 4;
	public bool shrinking;
	public bool growing;
	public float maxIntensityForPlayer = 20;

	public void AssignMass(float it)
	{
		planetMass = it;
	}
	public void AddObject(GameObject it)
	{
		if (it != transform.root.gameObject) {
			if (!rbUnderInfluence.Contains (it) && !rbUnderPlanetInfluence.Contains(it.GetComponent<Rigidbody>())) {
				rbUnderInfluence.Add (it);
			}
		}
	}
	void FixedUpdate()
	{
		NewtonianAttraction ();
		PlanetaryAttraction ();
	}
	void NewtonianAttraction()
	{
		if (GetComponent<HandlePlanetBirth>().haveGrown== true) {
			for (int i = 0; i < rbUnderInfluence.Count; i++) {
				if (rbUnderInfluence [i].gameObject == null) {
					rbUnderInfluence.RemoveAt (i);
				} else {
					if (rbUnderInfluence [i].GetComponent<Rigidbody> ()) {


						Vector3 direction = rbUnderInfluence [i].transform.position - transform.position;
						Rigidbody rb = rbUnderInfluence [i].GetComponent<Rigidbody> ();
						float radiuses = rb.GetComponent<Renderer> ().bounds.extents.x + this.GetComponent<Renderer> ().bounds.extents.x;
						float distance = (Vector3.Distance (transform.position, rbUnderInfluence [i].transform.position))-radiuses;
						distance = distance * distance;

						float intensity = 0;
						if (!outForce) {
							if (rb) {
								//float sqrD = Mathf.Sqrt (distance);
								//print (distance + "   sqr is" + sqrD);
								//print ("pm " + planetMass+"  d " + distance);
								intensity =  gravitationalForce* (rb.mass* planetMass /distance );
								//print (intensity);

							}
						}
						/*
						if (outForce) {
							if (rb) {
								intensity = gravitationalForce  * (rb.mass * planetMass / distance * distance);
							}
						}
						*/
						//Proper GravitationalForce Calculation
						direction = direction.normalized;
						if (rb) {
							if (rb.velocity != Vector3.zero) {
								if (rb.transform.root.GetComponent<Attributes> ()) {
									if (intensity > maxIntensityForPlayer) {
										intensity = maxIntensityForPlayer;
									}
									print (intensity);
								}
								rb.AddForce (direction.normalized * intensity*-1);
							}
						}

						if (influenceBack) {
							Vector3 directionBack = transform.position - rbUnderInfluence [i].transform.position;
							float distanceBack = Vector3.Distance (transform.position, rbUnderInfluence [i].transform.position);
							float intensityBack = 0;
							distanceBack = distanceBack * distanceBack;
							float stipulatedMassOfRb = GetThingMass (rbUnderInfluence [i].gameObject);
							float intensity2 = 0;

							if (outForce) {
								if (rb) {
									intensity2 = gravitationalForce * -1 * (stipulatedMassOfRb * planetMass / distanceBack);
								}
							}
							if (this.GetComponent<Rigidbody> ()) {
								GetComponent<Rigidbody> ().AddForce (directionBack.normalized * intensity);
							}
						}

						//Check For Planets
						if (rbUnderInfluence [i].GetComponent<PlanetPower> () && rbUnderInfluence [i].GetComponent<HandlePlanetBirth> ().haveGrown == true)
						{
							transform.position = Vector3.MoveTowards (transform.position, rbUnderInfluence [i].transform.position, 
								10 * Time.deltaTime);

							float distace = Vector3.Distance (transform.position, rbUnderInfluence [i].transform.position);
							float myBounds = GetComponent<Renderer> ().bounds.extents.x;
							float hisBounds = rbUnderInfluence [i].GetComponent<Renderer> ().bounds.extents.x;
							if ((distace - myBounds - hisBounds) < 2) {	
								HandlePlanetCollision (rbUnderInfluence [i].gameObject);
							}
						}
					}
				}
			}
		}
	}
	void PlanetaryAttraction()
	{	
		for (int i = 0; i < rbUnderPlanetInfluence.Count; i++) {
			if (rbUnderPlanetInfluence [i].gameObject == null) {
				rbUnderPlanetInfluence.RemoveAt (i);
			} else {
				Attributes at;
				at = rbUnderPlanetInfluence [i].transform.root.GetComponent<Attributes> ();
				Vector3 gravityUp4body = (rbUnderPlanetInfluence [i].transform.position - transform.position).normalized;
				Vector3 localUp = at.myArmor.up;
				Quaternion targetRotation = Quaternion.FromToRotation (localUp, gravityUp4body) * at.myArmor.rotation;
				at.myArmor.rotation = Quaternion.Slerp (at.myArmor.rotation, targetRotation, 500 * Time.deltaTime);
				Rigidbody rb;
				rb = at.myBall.GetComponent<Rigidbody> ();
				rb.AddForce (gravityUp4body * gravity);

			}
		}
	}
	public float GetThingMass(GameObject it)
	{
		Renderer rend;
		rend = GetComponent<Renderer>();
		float radius;
		radius = rend.bounds.extents.y;
		float mass = (4 / 3) * Mathf.PI * (radius);
		planetMass = mass;
		return mass;
	}
	public void RemoveObject(GameObject it)
	{
		/*
		for (int i = 0; i < rbUnderInfluence.Count; i++) {
			if (it == rbUnderInfluence[i]) {
				rbUnderInfluence.RemoveAt (i);

				print ("Removing object" + it.name);
			}
		}
		*/
		if (rbUnderInfluence.Contains (it)) {
			rbUnderInfluence.Remove (it);
		}
		if(GetComponent<PlanetPower>().objectsThatAllreadyExertedInfluence.Contains(it))
		{
			GetComponent<PlanetPower>().objectsThatAllreadyExertedInfluence.Remove (it);
		}

	}


	//Planet


	public void OnTriggerEnter2(Collider col)
	{
		/*
		if (col.tag != "Player" || col.tag == "PO")
		{
			


			if (!col.GetComponent<GenericSpaceManipulation> () ) {
				col.transform.gameObject.AddComponent<GenericSpaceManipulation> ();
			}
			if (plane) {
				col.transform.gameObject.GetComponent<GenericSpaceManipulation> ().EstablishPlane (this.transform);
			}
			if (planet) {
				col.transform.gameObject.GetComponent<GenericSpaceManipulation> ().EstablishPlanet (this.transform, this);
			}

		}
		*/

		//Its player
		/*
		if (col.tag == "Player"){
			if (col.transform.root.GetComponent<SpaceManipulation> ()) {
				if (plane) {
					col.transform.root.GetComponent<SpaceManipulation> ().ActivateArtificialGravity (this.transform,"planet" );
				}
				if (planet) {

					//Check if ready to become objects vasallos
					if (GetComponent<ApplyNewtonianForce> ().rbUnderInfluence.Contains (col.gameObject)) {
						GetComponent<ApplyNewtonianForce> ().rbUnderInfluence.Remove (col.gameObject);
					}

					StartCoroutine(RotToPlanet(col, 2));
				}
			}
			if (col.transform.parent.GetComponent<CreatureAttributes> ()) {
				if (!col.transform.parent.GetComponent<CreatureSpaceManipulation> ()) {
					col.transform.parent.gameObject.AddComponent<CreatureSpaceManipulation> ();
				}
				col.transform.parent.gameObject.GetComponent<CreatureSpaceManipulation> ().ActivatePlanetaryAttraction (this.transform.transform, this);	
			}
		}

		*/


	}
	public void OnTriggerExit2(Collider col)
	{
		if (col.tag == "Player") {
			if (col.transform.parent.GetComponent<SpaceManipulation> ()) {
				col.transform.parent.GetComponent<SpaceManipulation> ().DeActivateArtificialGravity ();

			}
		}
		RemoveObject (col.gameObject);
		//rbUnderInfluence.Add (col.transform.GetComponent<Rigidbody> ());

	}
	public void OnInnerTriggerExit(Collider col)
	{
		if (rbUnderPlanetInfluence.Contains(col.gameObject.GetComponent<Rigidbody>())) {
			rbUnderPlanetInfluence.Remove (col.gameObject.GetComponent<Rigidbody>());
			rbUnderInfluence.Add (col.gameObject);

			if (rotatingToplanet) {
				print ("stoping");
				StopCoroutine (inst);
			}

			col.transform.root.GetComponent<SpaceManipulation> ().RotateToWorld ();
			//col.GetComponent<Rigidbody>().useGravity = true;
		}
	}
	public void OnInnerTriggerEnter(Collider col)
	{
		if (GetComponent<HandlePlanetBirth> ().haveGrown == true) {
			inst = RotToPlanet (col, 2);
			StartCoroutine (inst);
			col.GetComponent<Rigidbody> ().useGravity = false;
			if (rbUnderInfluence.Contains (col.gameObject)) {
				rbUnderInfluence.Remove (col.gameObject);
			}
			/*
			if (objectsThatAllreadyExertedInfluence.Contains (col.gameObject)) {
				objectsThatAllreadyExertedInfluence.Remove (col.gameObject);
			}
			*/
			if (!rbUnderPlanetInfluence.Contains (col.gameObject.GetComponent<Rigidbody>())) {
				//rbUnderPlanetInfluence.Add (col.gameObject.GetComponent<Rigidbody>());
			}
		}
	}
	bool rotatingToplanet;
	IEnumerator inst = null;

	IEnumerator RotToPlanet(Collider colz, float time)
	{
		rbUnderPlanetInfluence.Add (colz.transform.GetComponent<Rigidbody> ());

		print ("rotating");
		currentlyRotating = true;
		rotatingToplanet = true;
		float elapsedTime = 0;
		while (elapsedTime < time) {
			Attributes at;
			at = colz.transform.root.GetComponent<Attributes> ();
			Vector3 gravityUp4body = (colz.transform.position - transform.position).normalized;
			Vector3 localUp = at.myArmor.up;
			Quaternion targetRotation = Quaternion.FromToRotation (localUp, gravityUp4body) * at.myArmor.rotation;
			at.myArmor.rotation = Quaternion.Slerp (at.myArmor.rotation, targetRotation, (elapsedTime / time));
			Rigidbody rb;
			rb = at.myBall.GetComponent<Rigidbody> ();
			rb.AddForce (gravityUp4body * gravity);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		currentlyRotating = false;
		rotatingToplanet = false;
		//colz.transform.root.GetComponent<SpaceManipulation> ().ActivatePlanetaryGravity(this.transform, this);
	}

	public void SetBodyNewRotation(Transform it)
	{
		StartCoroutine (RotationToBeInPlanet (it, 2));
	}
	IEnumerator RotationToBeInPlanet(Transform body, float time)
	{
		currentlyRotating = true;
		float elapsedTime = 0;
		while (elapsedTime < time) {
			Vector3 gravityUp = (body.position - transform.position).normalized;
			Vector3 localUp = body.parent.GetComponent<Attributes>().myArmor.up;
			Quaternion targetRotation = Quaternion.FromToRotation(localUp,gravityUp) * body.parent.GetComponent<Attributes>().myArmor.rotation;

			while (elapsedTime < time) {
				body.root.GetComponent<Attributes>().myArmor.rotation = Quaternion.Slerp(body.rotation,targetRotation,(elapsedTime / time) );
				yield return null;
			}
		}
		currentlyRotating = false;
	}
	public void Attract(Transform body) {
		if (!currentlyRotating) {
			Vector3 gravityUp = (body.position - transform.position).normalized;
			Vector3 localUp = body.parent.GetComponent<Attributes> ().myArmor.up;
			body.GetComponent<Rigidbody> ().AddForce (gravityUp * gravity);
			Quaternion targetRotation = Quaternion.FromToRotation (localUp, gravityUp) * body.parent.GetComponent<Attributes> ().myArmor.rotation;
			body.root.GetComponent<Attributes> ().myArmor.rotation = Quaternion.Slerp (body.rotation, targetRotation, 500 * Time.deltaTime);
		}
	}  

	public void HandlePlanetCollision(GameObject otherPlanet)
	{
		if (!shrinking)
		{
			if (this.transform.lossyScale.magnitude > otherPlanet.transform.lossyScale.magnitude) {
				StartCoroutine(Merge (this.transform, otherPlanet.transform, 3));
				otherPlanet.GetComponent<ApplyNewtonianForce> ().shrinking = true;
			} else {
				if (this.transform.lossyScale.magnitude == otherPlanet.transform.lossyScale.magnitude) {
					StartCoroutine(Merge (this.transform, otherPlanet.transform, 3));
					otherPlanet.GetComponent<ApplyNewtonianForce> ().shrinking = true;
				}
			}
		}
	}

	void DestroySmallestPlanet(GameObject otherPlanet)
	{

		if (!shrinking)
		{
			if (this.transform.lossyScale.magnitude > otherPlanet.transform.lossyScale.magnitude) {
				Destroy (otherPlanet);
			} else {
				if (this.transform.lossyScale.magnitude == otherPlanet.transform.lossyScale.magnitude) {
					Destroy (otherPlanet);

				}
			}
		}
	}
	IEnumerator Merge(Transform merger, Transform mergee, float time)
	{
		Vector3 newSizeForMerger = merger.transform.localScale + mergee.transform.localScale;
		float elapsedTime = 0;
		while (elapsedTime < time) {
			merger.transform.localScale = Vector3.Lerp (merger.transform.localScale, newSizeForMerger, (elapsedTime / time));
			if (mergee) {
				mergee.transform.localScale = Vector3.Lerp (mergee.transform.localScale, Vector3.zero, (elapsedTime / time));
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		if (mergee) {
			Destroy (mergee.gameObject);
		}

	}

	public void AttractBastard(Transform body) {
		if (body.GetComponent<Rigidbody>())
		{
			Vector3 gravityUp = (body.position - transform.position).normalized;
			body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity * body.GetComponent<Rigidbody>().mass );
		}
	}   




}
