using UnityEngine;
using System.Collections;

public class ObjectHandle : MonoBehaviour {


	//Add Dont go through if we go faster than
	public bool variableDontGoThrough = true;
	public float addDontGoThroughThingsThresholdVelocity = 15;
	public bool destroyOnColiision;
	public bool ActivateGravWhenCollisionIfItHasNoGrav = true;
	public bool destroyIfReachedMinVel;
	public float minVel = 2;
	public float shrinkTime = 1;
	public float shrinkDelay = 1;
	public float shrinkDelayCheckInterval = .5f;
	public bool destroyAfterTime;
	public bool respawn;
	public float maxLifeTime = 15;
	public int minY = -10;
	public bool SoundWhenCollision;
	public AudioClip soundWhenCollided;
	Vector3 initialPos;
	Rigidbody rb;
	bool haveRb;
	bool adjustedDontGoThroguh;
	float timeOfCreation;
	public RadarOne IamThisRadarsObject;


	void OnEnable()
	{
		timeOfCreation = Time.time;
		if (GetComponent<Rigidbody> ()) {
		
			rb = GetComponent<Rigidbody> ();
			haveRb = true;
		}
		if (variableDontGoThrough) {
			if (!GetComponent<DontGoThroughThings> ()) {
				gameObject.AddComponent<DontGoThroughThings> ();
			}		
		}
	}

	public bool RemoveRadarNotificationAfterExtition()
	{
		if (IamThisRadarsObject) {
			IamThisRadarsObject = null;
			return true;

		} else {
			return false;
		}
	}
			
	public void DisposeObject()
	{
		if (IamThisRadarsObject) {		
		
			if (IamThisRadarsObject.GetComponent<RadarOne> ()) {
				IamThisRadarsObject.GetComponent<RadarOne> ().RecieveNotificationFromRadarsOriginalObject (this.gameObject);
			}
			print ("can locate radar Obj");
		} else {
			print ("canwhaaat ths should be allways radar");
		
			Destroy (gameObject);
		}

		Destroy (gameObject);
			
	}
	// Update is called once per frame
	void Update () {
		if (transform.position.y < minY ) {
			if (respawn) {
				PlaceAgain ();
			} else {
				DisposeObject ();
			}
		}
		if (destroyAfterTime) {
			if ((Time.time - timeOfCreation) > maxLifeTime) {
				DisposeObject ();
			}
		}
		if (destroyIfReachedMinVel) {
			if (rb.velocity.magnitude < minVel) {
				StartCoroutine (WaitALilAndCheck ());
				//StartCoroutine(ShrinkAndDestroy();
				//gameObject.AddComponent<ShrinkAndDestroy>();
				//GetComponent<ShrinkAndDestroy> ().SetTimes (0,shrinkTime);
			}
		}


		if (variableDontGoThrough) {
			if (!adjustedDontGoThroguh) {
				if (haveRb) {
					if (rb.velocity.magnitude < addDontGoThroughThingsThresholdVelocity) {
						GetComponent<DontGoThroughThings> ().enabled = false;
						adjustedDontGoThroguh = true;
					} else {
						GetComponent<DontGoThroughThings> ().enabled = true;

						print ("too fast");
					}
				}
			}
		}

	}

	public void TickingClockLifeSpan(int it)
	{
		destroyAfterTime = true;
		maxLifeTime = it;
	}
	void PlaceAgain()
	{
		transform.position = initialPos;
		transform.rotation = Quaternion.identity;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		if (GetComponent<Rigidbody> ()) {

			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		}
	}
	void OnCollisionEnter()
	{	
		if (SoundWhenCollision) {
			AudioSource.PlayClipAtPoint (soundWhenCollided, transform.position);
		}
		if (ActivateGravWhenCollisionIfItHasNoGrav) {
		
			if (rb.useGravity == false) {
				rb.useGravity = true;
			}
		}
		if (destroyOnColiision) {
			DisposeObject ();
		}
	}


	IEnumerator ShrinkAndDestroy()
	{
		float elapsedTime = 0;

		while (elapsedTime < shrinkTime) {

			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, (elapsedTime	/ shrinkTime));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return null;
		DisposeObject ();
	}
	IEnumerator WaitALilAndCheck()
	{
		yield return new WaitForSeconds(shrinkDelay);
		StartCoroutine(ShrinkAndDestroy());
		yield return null;
	}


}
