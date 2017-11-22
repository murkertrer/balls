using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionWhenCollision : MonoBehaviour {
	public bool exploteAfterDelay = true;

	public GameObject explosion;
	public GameObject explosionRadiusShow;
	public GameObject explosionRadiusShowInst;
	Color it ;
	List<GameObject> thingsToTakeCare = new List<GameObject>();

	public bool ExplodeOut;
	public bool ExplodeUp;

	int TeamOrigin;
	bool friendlyFire = false;
	bool friendlyFireForce = false;


	public float sideForce =300;
	public float upForce = 500;
	public float radius = 8;
	public float force = 300;
	public float damage =50;

	public Transform childForParticleExplosion;
	public Transform childForParticleTrail;

	public AudioClip explosionSound;


	void OnCollisionEnter()
	{
		AreaDamageEnemies (this.transform.position);
		AudioSource.PlayClipAtPoint (explosionSound, this.transform.position);
	}

	void OnEnable ()
	{
		friendlyFire = this.GetComponent<ProyectileAttributes> ().friendlyFire;
		TeamOrigin = this.GetComponent<ProyectileAttributes> ().TeamOrigin;
		if (childForParticleTrail) {
			var trail = childForParticleTrail.GetComponent<ParticleSystem> ();
	
		trail.Play ();
		}

	}
	void AreaDamageEnemies (Vector3 origin)
	{
		Collider[] objectsInRange = Physics.OverlapSphere (origin ,radius);
		foreach (Collider col in objectsInRange)
		{
			if (col.GetComponent<Rigidbody> () && col.tag != "proyectile") 
			{
				RaycastHit hit;
				Vector3 rayDirection =  col.transform.position -this.transform.position;
				if (Physics.Raycast (this.transform.position, rayDirection, out hit)) {


					float proximity = (origin - col.transform.position).magnitude;
					float effect = 1 - (proximity / radius);
					Vector3 orientation;
					orientation = origin - col.gameObject.transform.position;	


					if (hit.transform.tag == "Player") {

						if (friendlyFire) {

							if (col.transform.root.GetComponent<Attributes> ()) {
								col.transform.root.GetComponent<Attributes> ().TakeDamage (damage * effect);
							}
							if (col.gameObject.transform.root.GetComponent<CreatureAttributes> ()) {
								col.gameObject.transform.root.GetComponent<CreatureAttributes> ().TakeDamage (damage * effect);
							}
						}
						if (!friendlyFire) {
							if (col.transform.root.GetComponent<Attributes> ()) {
								if (col.transform.root.GetComponent<Attributes> ().team != this.GetComponent<ProyectileAttributes> ().TeamOrigin) {
									col.transform.root.GetComponent<Attributes> ().TakeDamage (damage * effect);
								}
							}
							if (col.gameObject.transform.root.GetComponent<CreatureAttributes> ()) {
								if (col.gameObject.transform.root.GetComponent<CreatureAttributes> ().team != this.GetComponent<ProyectileAttributes> ().TeamOrigin) {
									
									col.gameObject.transform.root.GetComponent<CreatureAttributes> ().TakeDamage (damage * effect);
								}
							}

						}


						if (friendlyFireForce) {
							col.GetComponent<Rigidbody> ().AddForce (orientation * -1 * effect * sideForce);
							col.GetComponent<Rigidbody> ().AddForce (Vector3.up * upForce * 3 * effect);
						} 
						if (!friendlyFireForce) {

							if (col.transform.root.GetComponent<Attributes> ()) {
								if (col.transform.root.GetComponent<Attributes> ().team != this.GetComponent<ProyectileAttributes> ().TeamOrigin) {
									col.GetComponent<Rigidbody> ().AddForce (orientation * -1 * effect * sideForce/5);
									col.GetComponent<Rigidbody> ().AddForce (Vector3.up * upForce * 3 * effect/5);
								}
							}
							if (col.gameObject.transform.root.GetComponent<CreatureAttributes> ()) {
								if (col.gameObject.transform.root.GetComponent<CreatureAttributes> ().team != this.GetComponent<ProyectileAttributes> ().TeamOrigin) {
									col.GetComponent<Rigidbody> ().AddForce (orientation * -1 * effect * sideForce/5);
									col.GetComponent<Rigidbody> ().AddForce (Vector3.up * upForce * 3 * effect/5);
								}
							}
			
						} 


					} else 
					{

						if (col.gameObject.name == this.gameObject.gameObject.name) {
						} else {

							if (col.tag == "Crumble") {

								print ("Clumbly");
								if (col.GetComponent<CheckForForceToCrumble> ()) {
									col.GetComponent<CheckForForceToCrumble> ().WTF (orientation * -1 * effect * sideForce);
								}
							}

							if (ExplodeOut) {
								col.GetComponent<Rigidbody> ().AddForce (orientation * -1 * effect * sideForce);
							} else {
								col.GetComponent<Rigidbody> ().AddForce (orientation * effect * sideForce);
							}

							if (ExplodeUp) {
								col.GetComponent<Rigidbody> ().AddForce (Vector3.up * upForce * 3 * effect);
							}

							Debug.DrawRay (this.transform.position, rayDirection, Color.red);
						}

					}
				}
			}
		}
		if (childForParticleExplosion) {
			var exp	= childForParticleExplosion.GetComponent<ParticleSystem> ();
			exp.Play ();
		}
		GetComponent<Renderer> ().enabled = false;
		GetComponent<Collider> ().enabled = false;


		GameObject needToCleanup = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;






		float u = Random.Range (.7f, 7.2f);

		needToCleanup.AddComponent<DestroyTimer> ();
		needToCleanup.AddComponent<DestroyTimer> ().TickingClockLifeSpan (4);

		//thingsToTakeCare.Add (needToCleanup);

		//Destroy (this.gameObject);


		if (childForParticleTrail) {
			//childForParticleTrail.GetComponent<ParticleSystem> ().duration + 4
		}

		if (explosionRadiusShow) {
			/*
			GameObject go= Instantiate (explosionRadiusShow, transform.position, Quaternion.identity) as GameObject;
			Vector3 radiusSize = new Vector3 (radius, radius, radius);
			go.transform.localScale = radiusSize;
			explosionRadiusShowInst = go;
		
			//it = explosionRadiusShowInst.GetComponent<Renderer> ().material.color;
			//StartCoroutine(FadeTo (1.0f, .5f));
			//StartCoroutine (FadeSphere (2));
			*/

			//StartCoroutine(FadeTo(0.3f, .5f, go));
			//StartCoroutine (WaitToDestroy (.1f));
			//go.AddComponent<FadeOverTime>();
			//go.GetComponent<FadeOverTime> ().StartFade (.4f);
		}


		if (exploteAfterDelay) {
			StartCoroutine (WaitToDestroy (3));
		} else {
			EliminateObject ();
		}
	}

	void EliminateObject()
	{

		GetComponent<ObjectHandle> ().DisposeObject ();

		/*
		if (GetComponent<ObjectHandle> ()) {
			

			print ("eliminating object via radar RADAR");

		}
		print ("eliminating object via radarless");

		Destroy (gameObject);
		Destroy (this.GetComponent<Rigidbody> ());

		*/
	}


	IEnumerator WaitToDestroy(float it)
	{
		yield return new WaitForSeconds (it);
		if (explosionRadiusShowInst) {
			Destroy (explosionRadiusShowInst);
		}
		for (int i = 0; i < thingsToTakeCare.Count; i++) {
			Destroy (thingsToTakeCare [i].gameObject);
		
		}
		EliminateObject ();
		yield return null;
	}
		
	IEnumerator FadeTo(float aValue, float aTime, GameObject it)
	{
		print ("t");
		explosionRadiusShowInst.GetComponent<Renderer> ().material.color = Color.cyan;
		float alpha = explosionRadiusShowInst.GetComponent<Renderer> ().material.color.a;
		for (float t = 0.0f; t < 1; t += Time.deltaTime/aTime )
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			explosionRadiusShowInst.GetComponent<Renderer> ().material.color = newColor;
			yield return null;

		}
		EliminateObject ();
	}
}
