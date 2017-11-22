using UnityEngine;
using System.Collections;

public class ContinuousShot : GenericThrowControl {
	Attributes at2;
	Transform currentShot2;
	public Rigidbody proyectile;
	public string thisAbilityName;


	bool selected;
	public int currentAmmo2;
	public int maxAmmo2;
	public float nextFire2;
	public string abName2;

	public float fRate2;
	public float totalShot2;
	public float nextFireLeft2;
	public float nextFireRight2;


	float startOfShot2 = 0;
	float endOfShot2;
	bool mouseDownOnTime2;
	bool alternator2;

	public float nextFireR;
	public float nextFireL;
	public float fireR;
	public float genNextFire;

	public bool gravityAmmo;
	public float velMultiplier = 1;

	void Start () {

		base.TEST ();
		at2 = gameObject.transform.root.GetComponent<Attributes> ();
		currentAmmo2 = maxAmmo2;
	}
	public void EstablishAmmoType(Transform it)
	{
		currentShot2 = it;
	}		
	void Update () {
		if (Input.GetMouseButton (0)) {
			HandleAlternationAndCoolDowns (fixedShotStrenght);
		}
	}		
	void ReduceAmmo ()
	{
		currentAmmo2 -= 1;
		at2.aS.UpdateAmmoSpecificCooldowns ();
		base.UpdateTheAmmoUIInUiControll (currentAmmo2, maxAmmo2);		
	}
	void HandleAlternationAndCoolDowns(float force)
	{
		if (!at2.aSSD.onlyLeft && !at2.aSSD.onlyRight) {
			if (genNextFire < Time.time) {
				if (alternator2) {
					if (at2.pH.R) {
						if (gravityAmmo) {
							ThrowItDouble (proyectile.GetComponent<Rigidbody> (), force * velMultiplier, 1, true);
						} else {
							ThrowItDouble (proyectile.GetComponent<Rigidbody> (), force * velMultiplier, 1, false);
						}

						//alternator2 = !alternator2;
						//GenericNextFireforDouble (1);
					}	
				} else {
					if (at2.pH.L) {						
						
						if (gravityAmmo) {
							ThrowItDouble (proyectile.GetComponent<Rigidbody> (), force * velMultiplier, 2, true);
						} else {
							ThrowItDouble (proyectile.GetComponent<Rigidbody> (), force * velMultiplier, 2, false);
						}					
					}
				
				}
				alternator2 = !alternator2;
				GenericNextFireforDouble (3);
			}
				
		}

		/*
		if (!at2.aSSD.onlyRight) {
			ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 1);
		}
		if (!at2.aSSD.onlyLeft) {
			if (!at2.pH.L) {
				ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 2);
			}
		}
		*/
	}
	public void PickedUpAnotherOfTheSameKind(GameObject brotherOne)
	{
		ContinuousShot cs;
		cs = brotherOne.GetComponent<ContinuousShot> ();

		if (cs.fRate2 < fRate2) {
			fRate2 = cs.fRate2;
		}
		if (maxAmmo2 < cs.maxAmmo2) {
			maxAmmo2 = cs.maxAmmo2;
		}
		RefillAmmo ();
		//Notify Player If Upgrade Happened
	}
	public void RefillAmmo()
	{

		////!!!!!!!!!!!
		currentAmmo2 = maxAmmo2;
		//at2.UIC.UpdateTheAmmoUI (currentAmmo2, maxAmmo2);
	}
	public void GenericNextFireforDouble(int shootCorrespondance)
	{
		if (shootCorrespondance == 1) {
			nextFireR = Time.time + (fireR*2);

		}
		if (shootCorrespondance == 2) {
			nextFireL = Time.time + (fireR * 2);

		}

		//for both arms
		if (shootCorrespondance == 3) {
			genNextFire = Time.time + fireR;
		}
	}
	public void AbilitySelected()
	{
		at2.UIC.UpdateAbilityInfo(thisAbilityName,fRate2, currentAmmo2, maxAmmo2 );
	}
}




	/*
	public void ThrowItDouble(Rigidbody rb, float vel, int typeOfThrow)
	{
		print ("ZZZ" + typeOfThrow);
		attri = transform.root.GetComponent<Attributes> ();

		if (attri.pH.R) {
			if (typeOfThrow == 1) {
				if (!attri.zoneOfDetectR.GetComponent<InformOfShootAreaInvaders> ().haveStilOnTrigger) {
					if (nextFireR < Time.time) {
						Rigidbody flyThing2 = Instantiate (rb, attri.myThrowPoint.transform.position, attri.myThrowPoint.transform.rotation) as Rigidbody;
						flyThing2.GetComponent<Rigidbody> ().velocity = attri.myThrowPoint.transform.forward * vel;
						GenericNextFireforDouble (1);
						ReduceAmmo ();

					}
				}
			}
		}
		if (attri.pH.L) {
			if (typeOfThrow == 2) {
				if (!attri.zoneOfDetectL.GetComponent<InformOfShootAreaInvaders> ().haveStilOnTrigger) {
					if (nextFireL< Time.time) {				

						Rigidbody flyThing2 = Instantiate (rb, attri.myThrowPoint2.transform.position, attri.myThrowPoint2.transform.rotation) as Rigidbody;

						if (absoluteVelocity) {
							flyThing2.GetComponent<Rigidbody> ().velocity = attri	.myThrowPoint2.transform.forward * vel;
						}
						GenericNextFireforDouble (2);
						ReduceAmmo ();
					}
				}	
			}
		}
	}
	*/
