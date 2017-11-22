using UnityEngine;
using System.Collections;

public class AIBehave : MonoBehaviour {
	public bool fly;

	public Transform shield;
	public LineRenderer lr;
	public bool targetLocked;
	public Transform target;

	public bool planetaryMovement;
	public bool approach;
	public bool shoot;
	public AudioClip throwClip;
	public Transform armor;
	public Transform arm;
	public Rigidbody rb;
	public Rigidbody rbStraight;
	public Rigidbody rbGrav;

	public Transform throwPoint;
	public float proyectileDuration = .5f;
	public float straightShotForce = 15;
	public Transform ball;
	public float torqueSpeed = 5;

	public float fireRate = 50f;
	public float nextFire;
	public bool touchingGround = false;
	public float distToGround ;
	public float groundTolerance =  0.1f;
	public float maxApproach = 6;
	public int myTeam;
	AIFly aif;

	bool sequenceInitialized;
	public bool sequentialBehave = true;
	public float loopInterval = 3;

	public bool b1;
	public bool b2;
	public bool b3;
	public bool b4;
	public bool b5;

	KeepTrack kt;


	public LineRenderer aimLR;
	Vector3 initialPos;
	CreatureAttributes cA;
	Transform tempForShield;

	void Start () {
		initialPos = ball.transform.position;
		distToGround = rb.transform.GetComponent<Collider> ().bounds.extents.y;
		myTeam = this.transform.GetComponent<CreatureAttributes> ().team;
		cA = this.GetComponent<CreatureAttributes> ();

		if (this.GetComponent<AIFly>())
		{
			aif = this.GetComponent<AIFly> ();
		}

		GameObject go = GameObject.Find("EventManager");
		kt = go.GetComponent<KeepTrack> ();

		if (kt.playerTeam != myTeam) {
			cA.myAiBall.GetComponent<MeshRenderer> ().material.color = Color.red;
		}




	}
	/*
		if (Physics.Raycast (rb.transform.position, -Vector3.up, distToGround + groundTolerance+5)) {
			touchingGround = true;

		} else {
			touchingGround = false;
		}
*/
	void LateUpdate()
	{
		armor.transform.position = ball.transform.position;
		if (ball.transform.position.y < -5) {
			Respawn ();
		}
	}
	public void Respawn()
	{
		ball.transform.position = initialPos;
		rb.velocity = Vector3.zero;
	}
	void ApproachTarget(Vector3 pos)
	{
		/*
		aimLR.SetPosition (0, arm.transform.position);
		aimLR.SetPosition (1, pos);
		*/

		Vector3 dir = new Vector3 (0, 0, 0);
		dir = pos - ball.transform.transform.position;
		dir = Quaternion.AngleAxis (270, armor.up) * dir;
		ball.GetComponent<Rigidbody> ().AddTorque (dir* torqueSpeed*-1);
	}
	void AimTarget()
	{
		aimLR.enabled = true;
	}

	IEnumerator LoopIt()
	{
		sequenceInitialized = true;
		int behaveType = Random.Range (1, 6);
		Do (behaveType);
		yield return new WaitForSeconds (loopInterval);
		Renew ();
		sequenceInitialized = false;
	}
	void Do(int it)
	{
		switch (it) {
		case 1:
			b1 = true;
			break;
		case 2:
			b2 = true;
			break;
		case 3:
			b3 = true;
			break;
		case 4:
			b4 = true;
			break;
		case 5:
			b5 = true;
			break;
		}
	}
	void Renew()
	{
		b1 = false;
		b2 = false;
		b3 = false;
		b4 = false;
		b5 = false;
		lr.enabled = false;
	}
	void Update()
	{
		if (b1) {
			if (shoot &&  target) {
				RaycastHit hit;
				Vector3 rayDirection = target.transform.position - throwPoint.transform.position;
				Debug.DrawRay (throwPoint.transform.position, rayDirection);
				if (Physics.Raycast (throwPoint.transform.position, rayDirection, out hit)) {
					if (hit.transform.tag == "Player") {
						//or shield;
						arm.transform.LookAt (target.transform.position);
						Throw ();
						lr.enabled = true;
						lr.SetPosition (0, cA.myArm.transform.position);
						lr.SetPosition (1, target.transform.position);

					}
				}
			}
		}
		if (b2) {
			if (approach && target) {
				float it = Vector3.Distance (ball.transform.position, target.transform.position);
				if (it > maxApproach) {
					if (it < armor.GetComponent<SphereCollider> ().radius) {
						ApproachTarget (target.transform.position);

					}
				}
			}
		}
		if (b3) {
			if (shoot && target) {
				ThrowParabole (Random.Range(.5f, 5));
			}	
		}
		if (b4) {
			if (Time.time > nextFire) {
				Defend ();
				print ("aa");
				nextFire = Time.time + 2;

			}
		}
		if (b5) {
			if (Time.time > nextFire) {
				Defend ();
				print ("aa");
				nextFire = Time.time + 2;
			}
		}



	}
	public void Defend()
	{
		float nearestDistance = 50;
		//= float.MaxValue
		float distance = 0;
		Transform closestObj=null;
		Collider[] hitCollider = Physics.OverlapSphere (ball.transform.position, cA.myArmor.GetComponent<SphereCollider> ().radius);
		foreach (Collider col in hitCollider) {
			if (col.transform.tag == "Proyectile" ) {
				//|| col.tag == "Player"

				bool dox = false;
				if (col.transform.root.GetComponent<CreatureAttributes> ()) {
					if (col.transform.root.GetComponent<CreatureAttributes> ().team != cA.team) {
						dox = true;
					}
				}

				if (col.transform.root.GetComponent<Attributes> ()) {

					if (col.transform.root.GetComponent<Attributes> ().team != cA.team) {
						dox = true;
					}
				}
				if (col.transform.root.GetComponent<ProyectileAttributes> ()) {


					if (col.transform.root.GetComponent<ProyectileAttributes>().TeamOrigin != cA.team) {
						dox = true;
					}
				}
				if (dox )
				{
					print (col.gameObject.name);
					distance = Vector3.Distance (ball.transform.position, col.transform.position);
					if (distance < nearestDistance) {
						nearestDistance = distance;
						closestObj = col.transform;
					}
				}
			}
		}


		if (closestObj) {
				Vector3 relativePos = ball.transform.position - closestObj.transform.position;
				Quaternion rotation = Quaternion.LookRotation (relativePos);
				Vector3 negDistance = new Vector3 (0.0f, 0.0f, (ball.transform.GetComponent<MeshRenderer>().bounds.extents.y +2) *-1);
				Vector3 position = rotation * negDistance + ball.transform.position;
			tempForShield = Instantiate (shield, position, rotation) as Transform;
			tempForShield.parent = armor.transform;
			tempForShield.transform.gameObject.AddComponent<ShrinkAndDestroy> ();
			tempForShield.transform.gameObject.GetComponent<ShrinkAndDestroy> ().SetTimes (1, 3); ;


			//gop.transform.parent = this.transform;
				//go.transform.parent = armor.transform;
			//gop.transform.gameObject.AddComponent<DestroyAfterTime> ();
				//go.GetComponent<DestroyAfterTime> ().lifeSpan = 10;
			//gop.transform.gameObject.GetComponent<DestroyAfterTime> ().lifeSpan=1;

			}



	}
	public void ThrowParabole(float time2Impact)
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate+2;
			Rigidbody flyThing = Instantiate (rbGrav, throwPoint.transform.position, throwPoint.transform.rotation) as Rigidbody;
			flyThing.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = cA.team;
			Vector3 it = calculateBestThrowSpeed (arm.transform.position + arm.transform.forward, target.transform.position,  time2Impact);

			flyThing.GetComponent<Rigidbody> ().velocity = it;

		}
	}
	public void Throw()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Rigidbody flyThing = Instantiate (rbStraight, throwPoint.transform.position, throwPoint.transform.rotation) as Rigidbody;
			flyThing.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = cA.team;

			flyThing.GetComponent<Rigidbody> ().velocity = arm.transform.forward * straightShotForce;
			AudioSource.PlayClipAtPoint (throwClip, throwPoint.transform.position);
		}
	}
	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) {
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;

		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;

		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;

		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)

		return result;
	}

	public void StillWithinTrigger(Collider other)
	{	
		if (!fly) {
			if (target) {
				if (Vector3.Distance (target.transform.position, ball.transform.position) > cA.influenceRadius) {
					target = null;
				}
			}
			//if (other.tag == "Player" && touchingGround ) 
			if (target) {

				if (target.GetComponentInParent<CreatureAttributes> ()) {
					if (myTeam != target.transform.root.GetComponentInParent<CreatureAttributes> ().team) {
						if (!sequenceInitialized) {
							StartCoroutine (LoopIt ());
						}
					}
				}

				if (target.GetComponentInParent<Attributes> ()) {
					if (myTeam != target.transform.root.GetComponentInParent<Attributes> ().team) {
						if (!sequenceInitialized) {
							StartCoroutine (LoopIt ());
						}
					} else {
						target = null;
					}
				}
			} else {
				target = other.transform;
			}
		}


	}
	public void GotATriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			if (!targetLocked) {
				target = other.transform;
			}
		}
	}
	public void ExitedTrigger()
	{	
		aimLR.enabled = false;
	}
}
