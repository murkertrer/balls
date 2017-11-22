using UnityEngine;
using System.Collections;

public class DamageIfCollided : MonoBehaviour {
	public float damage = 20;
	public AudioClip soundWhenCollision;
	public int team;
	void OnEnable()
	{
	}


	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag== "Player") {
			team = GetComponent<ThingAttributes> ().team;

			if (soundWhenCollision) {
				AudioSource.PlayClipAtPoint (soundWhenCollision, this.transform.position);
			}

			/*
			if (col.transform.root.GetComponent<Attributes> ()) {
				col.transform.root.GetComponent<Attributes> ().TakeDamage (damage);
			}

			if (col.transform.root.GetComponent<CreatureAttributes> ()) {
				col.transform.root.GetComponent<CreatureAttributes> ().TakeDamage (damage);
			}
			*/

			if (col.transform.root.GetComponent<ThingAttributes> ()) {
				col.transform.root.GetComponent<ThingAttributes> ().TakeDamage (damage, team);
			}

			Destroy (this.gameObject);
		}
	}
}
