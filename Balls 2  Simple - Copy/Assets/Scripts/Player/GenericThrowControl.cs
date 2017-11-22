using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
[System.Serializable]
public class Item
{
	public int expelMethood;
	public int maxAmmo;
	public int curAmmo;
	public int key;
	public int type;

	public Item (int expel,int max, int cur, int key,int type)
	{
		this.expelMethood = expel;
		this.maxAmmo = max;
		this.curAmmo = cur;
		this.key = key;
	}
}
*/
public class GenericThrowControl : MonoBehaviour {


	public bool automaticArm = true;
	public bool absoluteVelocity;
	public float relativeVelocity = 3000;
	public float fixedShotSUltra = 8;
	public float fixedShotStrenght = 15;
	public float maxFixedShot = 60;	
	public float minFixedShot = 10;	
	public float chargedShotSpeed = 20;
	public float initialFixedShotStrenght;
	public float shotForce;
	public float strenghtMultiplier = 10;

	public int genericCurrentAmmo;
	public int genericMaxAmmo;
	public float genericNextFire;
	public float genericCurrentShotType;
	public int genericExpel;
	public float genericFireRate;
	//public float totalShot;
	public float maxChargedShot = 50;
	public float genericNextFireLeft;
	public float genericNextFireRight;



	/*
	float startOfShot = 0;
	float endOfShot;
	bool mouseDownOnTime;
	*/

	bool AimingSystemDouble;
	bool RTSAimingSystem;
	bool alternator3;

	public Attributes attri;
	AimingSystemSimpleDouble assd;
	//
	public Transform currentShot;


	public void TEST()
	{
		print ("called from inheritance");
	}


	public 	virtual void  GetAmmoCharacteristics(int max, int cur, int type, string name)
	{
	//print ("called get ammo on base");

	}


	void Start()
	{
		attri = transform.root.GetComponent<Attributes> ();
		assd = GetComponent<AimingSystemSimpleDouble> ();
		initialFixedShotStrenght = fixedShotStrenght;
	}

	void ForceChanger()
	{
		if (attri.iAmFPSPlayer) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				fixedShotStrenght += 1;
				UpdateUI (fixedShotStrenght, maxFixedShot);
				if (fixedShotStrenght >= maxFixedShot) {
					fixedShotStrenght = maxFixedShot;
					fixedShotStrenght -= 1;
				}
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				fixedShotStrenght -= 1;
				UpdateUI (fixedShotStrenght, maxFixedShot);
				if (fixedShotStrenght <= minFixedShot) {
					fixedShotStrenght = minFixedShot;
					fixedShotStrenght += 1;
				}
			}
		}
	}
	void Update()
	{
		if (attri.isSelected) {
			//Divide REsponsability From Selected to, Commanded
			ForceChanger ();
			//ActivateExpelMethood ();
		}
	}
	public void UpdateUI(float it, float max)
	{
		//at.UIC.UpdatePowerUI(it, max);
	}

	public List <AmmoInfo> CurrentAbilities = new List<AmmoInfo>();
	public struct AmmoInfo
	{
		int type;
		int curA;
		int maxA;
		float nxtF;
		float fireR;
		string name;
	}



	public void GetCurrentAmmoDetails(int type,int curA,int maxA, float nxtF, int expel, float fireR, string name)
	{
		print ("getting" + type + expel);

		EstablishCurrentShot (type);
		genericCurrentAmmo = curA;
		genericMaxAmmo = maxA;
		genericNextFire = nxtF;
		genericExpel = expel;
		genericFireRate = fireR;

		/*
		UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
		CancelOtherAbilitiesInProgress ();
		UpdateAmmoNameInUI (name, fireR);

		if (expel == 3) {
			fixedShotStrenght = (maxFixedShot + minFixedShot) / 2;
			UpdateUI (fixedShotStrenght, maxFixedShot);
		}
		*/


	}


	void EstablishCurrentShot(int it)
	{
		genericCurrentShotType = it;
		if (it== 1)
		{

			currentShot = attri.ballGravityExplosionShot;	
			//gameObject.AddComponent<ChargedShot> ();
			return;
		
		;}

		if (it == 2)
		{
			currentShot = attri.ballShotStraight;
			UpdateUI (fixedShotStrenght, maxFixedShot);
		
		;}


		/*
		if (it == 3)
		{	currentShot = shieldRotate;
			UpdateUI (0, maxFixedShot);
			;
		}
		if (it == 4)
		{	currentShot = BallGravityImplosion;
			;
		}
		if (it == 5) {
			currentShot = SmallBouncyBallGravityNoExplosion;
		}
		if (it == 6) {
			currentShot = orangeNoGrav;;
			;
		}
		if (it == 7) {
			currentShot = homingMissle;
			genericExpel = 7;
			at.kT.BoomerangInfo.color = Color.cyan;
			;
		}
		if (it == 8) {
			currentShot = Grenade;
			;
		}
		if (it == 10) {
			currentShot = boomerang;
			at.kT.BoomerangInfo.color = Color.green;
			;
		}
		if (it == 11) {
			currentShot = at.ballTrazer;
			;
		}
		if (it == 12) {
			currentShot = at.ballRegular;
			;
		}
		if (it == 13) {
			currentShot = at.likeBullet;
			//fixedShotStrenght = fixedShotSUltra;
			;
		}
		if (it == 14) {
			currentShot = at.impactBall;
			//fixedShotStrenght = fixedShotSUltra;
			;
		}

		if (it == 16) {
			currentShot = at.planetMaker;
		}

		//Bulllets
		if (it == 18) {
			currentShot = at.kT.LikeBulletNoGravDestroyImpact;
		}
		if (it == 19) {
			currentShot = at.kT.LikeBulletNoGravConstant;
		}
		if (it == 20) {
			currentShot = at.kT.LikeBulletNoGrav;
		}
		if (it == 21) {
			currentShot = at.kT.LikeBullet;
		}
		if (it == 22) {
			currentShot = at.kT.LikeBulletGravDestroyImpact;
		}
		*/


		//Modifiers
		if (it == 30) {
			currentShot = attri.kinematicBomb;
		}

		//EstablishDelegateExpelMethood ();
		print ("this number is not assignable");
	}

	/*
	void HandleAlternationAndCoolDowns(float force)
	{
		if (!assd.onlyLeft && !assd.onlyRight) {
			if (genericNextFire < Time.time) {
				if (alternator3) {
					if (attri.pH.R) {
						ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 1);
					}
				}
				if (!alternator3) {
					if (attri.pH.L) {
						ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 2);
					}
				}
				alternator3 = !alternator3;
				//For when boh arms are active
				GenericNextFireforDouble (3);
			}
		}
		if (assd.onlyRight) {
			ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 1);
		}
		if (assd.onlyLeft) {
			if (attri.pH.L) {
				ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 2);
			}
		}
	}
	*/


	public void ThrowItDouble(Rigidbody rb, float vel, int typeOfThrow, bool gravity)
	{

		attri = transform.root.GetComponent<Attributes> ();

		if (attri.pH.R) {
			if (typeOfThrow == 1) {
				if (!attri.zoneOfDetectR.GetComponent<InformOfShootAreaInvaders> ().haveStilOnTrigger) {
					if (genericNextFireRight < Time.time) {
						//R
						Rigidbody flyThing2 = Instantiate (rb, attri.myThrowPoint.transform.position, attri.myThrowPoint.transform.rotation) as Rigidbody;

						flyThing2.GetComponent<Rigidbody> ().velocity = attri.myThrowPoint.transform.forward * vel;


						/*
						if (absoluteVelocity) {
							flyThing2.GetComponent<Rigidbody> ().velocity = attri.myThrowPoint.transform.forward * vel * forceMultiplier;
						}
						if (!absoluteVelocity) {
							flyThing2.GetComponent<Rigidbody> ().AddForce(attri.myThrowPoint.transform.forward * vel * relativeVelocity);
						}
						//GenericNextFireforDouble (1);
						//ReduceAmmo ();
						*/
						if (gravity) {
							flyThing2.useGravity = true;
						}

						AssignProyectileThingAttributes (flyThing2);

					}
				}
			}
		}
		if (attri.pH.L) {
			if (typeOfThrow == 2) {
				if (!attri.zoneOfDetectL.GetComponent<InformOfShootAreaInvaders> ().haveStilOnTrigger) {
					if (genericNextFireLeft < Time.time) {				

						Rigidbody flyThing2 = Instantiate (rb, attri.myThrowPoint2.transform.position, attri.myThrowPoint2.transform.rotation) as Rigidbody;
						flyThing2.GetComponent<Rigidbody> ().velocity = attri.myThrowPoint.transform.forward * vel;

						if (gravity) {
							flyThing2.useGravity = true;
						}
						AssignProyectileThingAttributes (flyThing2);
						//GenericNextFireforDouble (2);
					}
				}	
			}
		}
	}

	void AssignProyectileThingAttributes(Rigidbody rb)
	{
		ThingAttributes ta;
		ta = rb.transform.GetComponent<ThingAttributes> ();
		ta.team = attri.team;
		ta.fromBallPlayer = attri.myBall;

		if (genericExpel == 7) {
			//GiveHommingInfo (rb);
		}

	}

	/*
	public float GenericNextFireforDouble(float rate)
	{
		float calculation = Time.time + rate;
		return calculation;
	}
	*/

	public void ReduceAmmo()
	{
		//genericCurrentAmmo -= 1;
		attri.aS.UpdateAmmoSpecificCooldowns ();
		//UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
		//MakeSound ();
	}
	public void UpdateTheAmmoUIInUiControll(int curAmmo, int maxAmmo)
	{
		attri.UIC.UpdateTheAmmoUI (curAmmo, maxAmmo);
	}

	public void MakeSound()
	{
		AudioSource.PlayClipAtPoint (attri.kT.expelSound, attri.myBall.transform.position);
	}
	//***
	public void assignGenericAmmo(int curA, int maxA)
	{
		genericCurrentAmmo = curA;
		genericMaxAmmo = maxA;
		UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
	}

	public void EmptyCurrentGenericAmmo()
	{
		genericCurrentAmmo = 0;
		genericMaxAmmo = 0;
		UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
		fixedShotStrenght = initialFixedShotStrenght;
	}
}