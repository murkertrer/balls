using UnityEngine;
using System.Collections;

public class AIF : MonoBehaviour {

	public Rigidbody ammo;
	float nextShot = 0 ;
	float fireRate = 3;
	public float vel = 10;
	CreatureAttributes ca;
	public Rigidbody rb;
	public Transform mySelf;
	public Transform target;
	public bool locked;
	public float maxMove = 10;

	public float maxApproach = 15;
	void OnEnable()
	{
		mySelf = this.transform.root;
		ca = this.GetComponent<CreatureAttributes> ();
		rb = this.GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = maxApproach;
	}
	public void GotATriggerEnter(Transform it)
	{
		if (it.transform.tag == "Player" && it.root.GetComponent<Attributes> ()) {
			
			if (!target) {
				target = it;
			}
		}
	}

	public void GotATriggerStay(Transform it)
	{
		if (!target) {

			if (it.transform.tag == "Player") {
				if (it.root.GetComponent<Attributes> ()) {
					target = it;
				}
			}
		}
	}

	void Update()
	{
		if (target) {
			if (target.root.GetComponent<Attributes> ()) {
				if (target.root.GetComponent<Attributes> ().team != this.GetComponent<CreatureAttributes> ().team) {


					transform.LookAt (target);


					if (Vector3.Distance (target.transform.position, this.transform.position) > maxApproach) {
						if (rb.velocity.magnitude < maxMove) {
							rb.AddForce ((transform.forward) * vel);
						}
					}
				} else {
					rb.velocity = Vector3.zero;
				}
			} else {

			}

		}

		Attack ();
	}
	void Attack()
	{

		if (nextShot < Time.time && target) {
	
			Rigidbody flyThing2 = Instantiate (ammo,ca.throwPoint.position, Quaternion.identity) as Rigidbody;
			flyThing2.GetComponent<Rigidbody> ().useGravity = false;
			flyThing2.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = ca.team;
			flyThing2.GetComponent<Rigidbody> ().velocity = this.transform.forward* vel * 20;
			nextShot = Time.time + fireRate;
		}

	}
}
