using UnityEngine;
using System.Collections;

public class HomingMissle : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject trackingNotification;
	public GameObject trackingNotificationInstantiated;

	public Transform target;
	public Vector3 targetVector3;

	public float toHoming = 5;
	public float timeToActivateHoming;
	public float timeOfBirth;
	public float homingVelocity = 20;

	public AudioClip startOfHoming;
	public AudioClip homingIt;
	public AudioClip explosion;

	public float sideForce =300;
	public float radius = 8;
	Vector3 trackerOffset = new Vector3(0,1,0);

	public bool targetIsPlayer;
	bool homingDelaySetUp;
	public float lerpSpeed = .8f;

	/*
	public void TargetInfo (bool isPlayer, Vector3 worldPos, Transform targetGet, float delayz)
	{
		targetIsPlayer = isPlayer;
		targetVector3 = worldPos;
		target = targetGet;
		timeOfBirth = Time.time;
		timeToActivateHoming = timeOfBirth + delayz;
		homingDelaySetUp = true;

	}
	*/
	public void TargetIsVector(Vector3 it, float delay)
	{
		timeOfBirth = Time.time;
		targetVector3 = it;
		targetIsPlayer = false;
		timeToActivateHoming = timeOfBirth + delay;
		homingDelaySetUp = true;
	}
	void FixedUpdate () {
		if (targetIsPlayer) {
			this.transform.LookAt (target);
			trackingNotification.transform.position = target.transform.position + trackerOffset;
		}
		if (!targetIsPlayer) {
			Vector3 relativePosz = targetVector3 - transform.position;
			Quaternion desiredRotx = Quaternion.LookRotation (relativePosz);
			transform.rotation = Quaternion.Slerp (transform.rotation, desiredRotx, Time.deltaTime * lerpSpeed);
		}
		if (timeToActivateHoming <= Time.time &&homingDelaySetUp) {
			ActivateHoming ();
		}
	}
	void ActivateHoming ()
	{
		this.GetComponent<Rigidbody> ().useGravity = false;
		this.GetComponent<Rigidbody> ().velocity = this.transform.forward * homingVelocity;
	}
	void OnCollisionEnter(Collision colz)
	{
		if (colz.transform.tag != "Proyectile") {
			Collider[] objectsInRange = Physics.OverlapSphere (this.transform.position, radius);
			foreach (Collider col in objectsInRange) {
				if (col.GetComponent<Rigidbody> () && col.tag != "Proyectile") {
					RaycastHit hit;
					Vector3 rayDirection = col.transform.position - this.transform.position;
					if (Physics.Raycast (this.transform.position, rayDirection, out hit)) {
						float proximity = (this.transform.position - col.transform.position).magnitude;
						float effect = 1 - (proximity / radius);
						Vector3 orientation;
						orientation = this.transform.position - col.gameObject.transform.position;
						col.GetComponent<Rigidbody> ().AddForce (orientation * -1 * effect * sideForce);
					}
				}
			}
			if (trackingNotificationInstantiated != null) {
				Destroy (trackingNotificationInstantiated);
			}
			Instantiate (explosionPrefab, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
