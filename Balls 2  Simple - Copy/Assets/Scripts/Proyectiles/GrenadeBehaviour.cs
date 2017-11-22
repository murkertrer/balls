using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeBehaviour : MonoBehaviour {

	public AudioClip boom;
	public AudioClip beep;

	bool ticking;
	float fuse = 5;
	float timeToExplode;
	float timeToNextBeep;

	public bool ExplodeOut;
	public bool ExplodeUp;

	int TeamOrigin;
	bool friendlyFire;

	public float sideForce =300;
	public float upForce = 500;
	public float radius = 8;
	public float force = 300;
	public float damage =50;
	// Use this for initialization
	void Start () {
	
	}
	void Update()
	{
		if (ticking) {
			
			//print ((timeToExplode - Time.time) / timeToExplode );
			if (timeToExplode < Time.time) {
				Explode ();

			}

			if (timeToNextBeep < Time.time)
			{

				/*
				if (((timeToExplode - Time.time) / timeToExplode) > .2f) {
					print ("called 3");
					timeToNextBeep = +.5f;

					PlayBeep ();
					return;
				}
				*/
			}
		}
	}

	IEnumerator BeepIt(float it)
	{
		PlayBeep ();
		List<float> beepSliced = new List<float> ();
		int numbOfBeeps = 5;

		for(int i = 0; i < numbOfBeeps; i++)
		{
			yield return new WaitForSeconds(it/numbOfBeeps);
			PlayBeep();
			//print ("beep");
		}

		///yield return new WaitForSeconds(
	}
	void PlayBeep()
	{
		AudioSource.PlayClipAtPoint (beep, this.transform.position);
	}
	void OnCollisionEnter()
	{
		StartCoroutine (BeepIt(fuse));
		ticking = true;
		timeToExplode = Time.time + fuse;
	}
	void Explode()
	{
		Collider[] objectsInRange = Physics.OverlapSphere (this.transform.position,radius);
		foreach (Collider col in objectsInRange)
		{
			if (col.GetComponent<Rigidbody> () && col.tag != "proyectile") 
			{
				RaycastHit hit;
				Vector3 rayDirection =  col.transform.position -this.transform.position;
				if (Physics.Raycast (this.transform.position, rayDirection, out hit)) {


					float proximity = (this.transform.position - col.transform.position).magnitude;
					float effect = 1 - (proximity / radius);
					Vector3 orientation;
					orientation = this.transform.position - col.gameObject.transform.position;	

					if (ExplodeOut) {
						col.GetComponent<Rigidbody> ().AddForce (orientation * -1 * effect * sideForce);
					} else {
						col.GetComponent<Rigidbody> ().AddForce (orientation * effect * sideForce);
					}

					if (ExplodeUp) {
						col.GetComponent<Rigidbody> ().AddForce (Vector3.up * upForce * 3 * effect);
					}
					if (hit.transform.tag == "Player") {
						Debug.DrawRay (this.transform.position, rayDirection, Color.green);


						if (col.gameObject.transform.parent.GetComponent<Attributes> ()) {
							col.gameObject.transform.parent.GetComponent<Attributes> ().TakeDamage (damage * effect);
						}
						/*
						if (col.gameObject.transform.parent.GetComponent<CreatureAttributes> ()) {
							col.gameObject.transform.parent.GetComponent<CreatureAttributes> ().TakeDamage (damage * effect);
						}
						*/

					} else 
					{
						Debug.DrawRay (this.transform.position, rayDirection, Color.red);

					}
				}
			}
		}

		AudioSource.PlayClipAtPoint (boom, this.transform.position);
		Destroy (this.gameObject);
	}

}
