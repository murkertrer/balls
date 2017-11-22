using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetPower : MonoBehaviour {
	public bool capSize;
	public bool hasReachedLimit;
	public float limitOfGrowth = 5;
	public float timeToExplode = 3;
	public bool changingSize;
	public float intervalOfGrowth = .2f;

	public List<GameObject> objectsThatAllreadyExertedInfluence = new List<GameObject> ();

	void Update()
	{
		if (capSize) {
			if (!hasReachedLimit) {
				if (this.transform.localScale.x > limitOfGrowth) {
					//timeOfReaching = Time.time;
					//explosionTime = Time.time + timeToExplode;
					StartCoroutine (Booma (timeToExplode));
					hasReachedLimit = true;
				}
			}
			if (this.transform.localScale.x < .5f) {
				Destroy (this.gameObject);
			}
		}
	}

	void Boom()
	{
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision col)
	{
		if (!objectsThatAllreadyExertedInfluence.Contains (col.gameObject)) {
			objectsThatAllreadyExertedInfluence.Add (col.gameObject);

			if (col.transform.tag == "Proyectile") {

				float currentLclScale = this.transform.localScale.x;
				float newSize = currentLclScale - intervalOfGrowth;
				Vector3 itz = new Vector3 (newSize, newSize, newSize);
				this.transform.localScale = itz;
				GetComponent<AssignRbMassAccordingToSize> ().AssignPlanetMass ();

				if (!changingSize) {
					//StartCoroutine (UnGrow (1f));
				} else {
				}

			} else {
				if (!hasReachedLimit) {}
				//StartCoroutine (Grow(1f));				
			}
		}
	}
	IEnumerator UnGrow(float time)
	{
		changingSize = true;
		float currentLclScale = this.transform.localScale.x;
		float newSize = currentLclScale - intervalOfGrowth;
		Vector3 itz = new Vector3 (newSize, newSize, newSize);

		float elapsedTime = 0;
		while (elapsedTime < time) {
			transform.localScale= Vector3.Slerp (transform.localScale, itz, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		changingSize = false;
	}
	IEnumerator Grow(float time)
	{
		float currentLclScale = this.transform.localScale.x;
		float newSize = currentLclScale + intervalOfGrowth;
		Vector3 itz = new Vector3 (newSize, newSize, newSize);

		float elapsedTime = 0;
		while (elapsedTime < time) {
			transform.localScale= Vector3.Lerp (transform.localScale, itz, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	IEnumerator Booma (float t)
	{
		yield return new WaitForSeconds (t);
		Boom();
	}

}




