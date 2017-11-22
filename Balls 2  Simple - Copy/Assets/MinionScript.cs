using UnityEngine;
using System.Collections;

public class MinionScript : MonoBehaviour {
	public bool p = false;
	public int divisionOfACtions = 3;
	Color activated = Color.green;
	Color searching  = Color.yellow;
	Color destroy = Color.red;
	bool startAttack;
	public Vector3 smallLocal  = new Vector3(.3f, .3f,.3f);
	public int radius = 5;
	public Transform target;
	float intervalOfCheck = 1;
	public float theForce = 3;
	public bool startedSelfDestructionSequence;
	public float explosionDistance = 3;
	public float radiusexplosion = 4;
	public float explosionForce = 50;
	public GameObject explosionPrefab;
	bool haltedMovement;


	void OnEnable () {
		this.transform.localScale = smallLocal;
	}
	void Update()
	{	
		if (target) {
			if (!startedSelfDestructionSequence && !haltedMovement) {
				GetComponent<Rigidbody> ().AddForce ((target.transform.position - transform.position).normalized * theForce);
			}
			if (Vector3.Distance (transform.position, target.position) < explosionDistance) {
				if (!startedSelfDestructionSequence) {
					haltedMovement = true;
					StartCoroutine (SelfDestroy ());
					startedSelfDestructionSequence = true;
				}
			}
		} else {
			CheckIfEnemyClose ();
		}
	}

	void OnCollisionEnter(Collision col)
	{
		ChangeColor (activated);
		StartCoroutine (WakeUp (1));
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}

	IEnumerator WakeUp(float time)
	{
		float elapsedTime = 0;
		while (elapsedTime < time) {
			this.transform.localScale= Vector3.Lerp(this.transform.localScale, new Vector3(1, 1, 1), (elapsedTime/time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		//tartAttack = true;
		CheckIfEnemyClose();
		if (target == null) {
			StartCoroutine (CheckIfPlayerIsArround (intervalOfCheck));
		}
		yield return null;
	}
	IEnumerator CheckIfPlayerIsArround(float time)
	{
		CheckIfEnemyClose ();
		yield return new WaitForSeconds (time);
		if (target = null) {
			StartCoroutine (CheckIfPlayerIsArround (time));
		}
	}
	void CheckIfEnemyClose()
	{
		var cols = Physics.OverlapSphere(transform.position, radius);
		foreach (var col in cols)
		{
			if (col.transform.tag == "Player") {
				if (col.transform.root.GetComponent<Attributes> ()) {
					target = col.transform;
					ChangeColor (searching);
				}
			}
		}
	}

	IEnumerator SelfDestroy()
	{
		ChangeColor (Color.blue);
		yield return new WaitForSeconds (.4f);
		ChangeColor (Color.red);
		yield return new WaitForSeconds (.2f);
		ChangeColor (Color.white);
		yield return new WaitForSeconds (.2f);
		ChangeColor (Color.red);
		yield return new WaitForSeconds (.2f);
		ChangeColor (Color.white);
		yield return new WaitForSeconds (.2f);
		ChangeColor (Color.red);
		yield return new WaitForSeconds (.2f);
		Explode ();
		yield return null;
	}
	void Explode()
	{
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Collider[] objectsInRange = Physics.OverlapSphere (this.transform.position ,radius);
		foreach (Collider col in objectsInRange) {
			if (col.GetComponent<Rigidbody> ()) {
				Rigidbody rb = col.GetComponent<Rigidbody> ();
				rb.AddExplosionForce (explosionForce, transform.position, radius);
				Vector3 orientation = col.gameObject.transform.position - transform.position;	
				rb.AddForce (orientation * 30);
			}
		}
		Destroy (this.gameObject);
	}
	void ChangeColor (Color it)
	{
		this.GetComponent<MeshRenderer> ().material.color = it;
	}
}

