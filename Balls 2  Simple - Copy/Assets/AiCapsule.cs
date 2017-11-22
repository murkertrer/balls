	using UnityEngine;
using System.Collections;

public class AiCapsule : MonoBehaviour {
	public Rigidbody ammo;
	public Rigidbody minion;
	public float proyectileDuration = 4;
	public float shootingInterval = 8;
	bool isEngaged = false;
	public Transform throwPoint;
	public Transform capsule;
	public bool addRandoomnessToShoot = true;
	public float randoomnessAmountShoot;
	float  maxRandoomFactor;
	float  minRandoomFactor;
	void OnEnable()
	{
		//capsule.GetComponent<MeshRenderer> ().material.color = Color.yellow;
		maxRandoomFactor = randoomnessAmountShoot;
		minRandoomFactor = randoomnessAmountShoot * -1;
	}
	void OnTriggerStay(Collider col)
	{
		if (col.transform.tag == "Player" || col.transform.tag == "PO") {
			if (col.transform.root.GetComponent<Attributes>())
			{
				throwPoint.LookAt (col.transform);
				if (!isEngaged) {
					int randoom = Random.Range (0, 3);
					if (randoom == 0) {
						StartCoroutine(Wait(shootingInterval));
					}
					if (randoom == 1) {
						StartCoroutine (CountUntilNextTurn (shootingInterval, col.transform, minion));
					}
					if (randoom == 2) {
						StartCoroutine (CountUntilNextTurn (shootingInterval, col.transform, minion));
					}
				}
			}
		}
	}
	IEnumerator Wait(float time)
	{
		isEngaged = true;
		capsule.GetComponent<MeshRenderer> ().material.color = Color.red;
		yield return new WaitForSeconds (time);
		isEngaged = false;
		yield return null;
	}
	IEnumerator CountUntilNextTurn(float time, Transform enemy, Rigidbody passAmmo)
	{
		capsule.GetComponent<MeshRenderer> ().material.color = Color.cyan;					
		isEngaged = true;
		float elapsedTime = 0;
		AutoShoot (enemy, passAmmo );
		float modTime = time / 6;
		Vector3 relativePos = enemy.position - capsule.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		while (elapsedTime < modTime) {
			capsule.transform.localRotation= Quaternion.Lerp (capsule.transform.localRotation, rotation, (elapsedTime / modTime));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds (time - modTime);
		isEngaged = false;
		yield return null;
	}
	void AutoShoot(Transform enemy, Rigidbody versatileAmmo)
	{
		Rigidbody flyThing = Instantiate (versatileAmmo, throwPoint.transform.position,throwPoint.transform.rotation) as Rigidbody;
		var y = calculateBestThrowSpeed (throwPoint.transform.position, enemy.transform.position, proyectileDuration);
		flyThing.GetComponent<Rigidbody> ().velocity = y;



		RaycastHit hit;
		Vector3 rayDirection = enemy.transform.position - throwPoint.transform.position;
		Debug.DrawRay (throwPoint.transform.position, rayDirection);
		if (Physics.Raycast (throwPoint.transform.position, rayDirection, out hit)) {
			if (hit.transform.tag == "Player") {

			}
		}
	}
	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) {
		// calculate vectors
		Vector3 toTarget = target - origin;
		if (!addRandoomnessToShoot) {
			toTarget = target - origin;
		}
		if (addRandoomnessToShoot) {
			Vector3 posPlusRandoom = new Vector3 (target.x + Random.Range (minRandoomFactor, maxRandoomFactor), target.y + Random.Range (minRandoomFactor, maxRandoomFactor), target.z + Random.Range (minRandoomFactor, maxRandoomFactor));
			toTarget = posPlusRandoom - origin;
		}

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
}
