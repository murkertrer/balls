using UnityEngine;
using System.Collections;

public class FlightShoot : MonoBehaviour {


	public bool alternator;


	GenericThrowControl gtc;
	Attributes at;
	FlightAim fa;
	public float fixedForceMultiply = 2;

	bool rightArm;
	// Use this for initialization
	void Start () {
		gtc = this.GetComponent<GenericThrowControl> ();
		at = this.GetComponent<Attributes> ();
		fa = this.GetComponent<FlightAim> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gtc.genericExpel == 1) {
			ChargesShotFct ();
			return;
		}
		if (gtc.genericExpel == 2) {
			return;
		}
		if (gtc.genericExpel == 3) {
			FixedShotFct ();
			//MachineGun ();
			return;
		}
	}
	void FixedShotFct()
	{
		if (gtc.genericCurrentAmmo > 0) {
			if (Input.GetMouseButton (0)) {
				if (!fa.onlyLeft && !fa.onlyRight) {
					if (gtc.genericNextFire < Time.time) {
						if (alternator) {
							ThrowIt (gtc.currentShot.GetComponent<Rigidbody> (), gtc.fixedShotStrenght * fixedForceMultiply, 2);
							gtc.ReduceAmmo ();	
						}
						if (!alternator) {
							ThrowIt (gtc.currentShot.GetComponent<Rigidbody> (), gtc.fixedShotStrenght * fixedForceMultiply, 1);
							gtc.ReduceAmmo ();
						}
						//gtc.GenericNextFireforDouble (3);
						alternator = !alternator;
						return;
					}
				}
				if (fa.onlyLeft) {
						ThrowIt (gtc.currentShot.GetComponent<Rigidbody> (), gtc.fixedShotStrenght * fixedForceMultiply, 2);
						gtc.ReduceAmmo ();
				}
				if (fa.onlyRight) {
						ThrowIt (gtc.currentShot.GetComponent<Rigidbody> (), gtc.fixedShotStrenght * fixedForceMultiply, 1);
						gtc.ReduceAmmo ();
				}
			}
		}
	}

	void ChargesShotFct ()
	{
	}
	void MachineGun()
	{
	}
	public void ThrowIt(Rigidbody rb, float vel, int type)
	{
		//if (this.GetComponent<AimingSystem> ().armWithinShooting) {}
		//shootExplosion.Play ();
		//AudioSource.PlayClipAtPoint (shootSound, myBall.transform.position);
		if (type == 1 && gtc.genericNextFireRight < Time.time) {

			Rigidbody flyThing = Instantiate (rb, at.myThrowPoint.transform.position, at.myThrowPoint.transform.rotation) as Rigidbody;
			flyThing.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = at.team;
			flyThing.GetComponent<Rigidbody> ().velocity = this.GetComponent<Attributes> ().myThrowPoint.transform.forward * vel * 20;
			NotifyFireForFireRate (1);
			gtc.MakeSound ();
		}
		if (type == 2 && gtc.genericNextFireLeft < Time.time) {

			Rigidbody flyThing = Instantiate (rb, at.myThrowPoint2.transform.position, at.myThrowPoint2.transform.rotation) as Rigidbody;
			flyThing.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = at.team;
			flyThing.GetComponent<Rigidbody> ().velocity = at.myThrowPoint2.transform.forward * vel * 20;
			NotifyFireForFireRate  (2);
			gtc.MakeSound ();
		}
		if (type == 3 && gtc.genericNextFire < Time.time )
		{

			Rigidbody flyThing = Instantiate (rb, at.myThrowPoint.transform.position, at.myThrowPoint.transform.rotation) as Rigidbody;
			flyThing.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = at.team;
			flyThing.GetComponent<Rigidbody> ().velocity = this.GetComponent<Attributes> ().myThrowPoint.transform.forward * vel * 20;
			Rigidbody flyThing2 = Instantiate (rb, at.myThrowPoint2.transform.position, at.myThrowPoint2.transform.rotation) as Rigidbody;
			flyThing2.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = at.team;
			flyThing2.GetComponent<Rigidbody> ().velocity = at.myThrowPoint2.transform.forward * vel * 20;
			NotifyFireForFireRate (3);	
			//A way to deferenciate sounds when shooting two at a time?
			gtc.MakeSound ();
		}
		gtc.UpdateUI (gtc.fixedShotStrenght, gtc.maxFixedShot);
	}

	void Instantiation(Transform it)
	{
		
	}

	void NotifyFireForFireRate( int caseOfFire)
	{
		if (caseOfFire == 1) {
			//gtc.GenericNextFireforDouble (1);
		}
		if (caseOfFire == 2) {
			//gtc.GenericNextFireforDouble (2);
		}
		if (caseOfFire == 3) {
			//gtc.GenericNextFireforDouble (3);
		}
		
	}
}
