using UnityEngine;
using System.Collections;

public class ChargedShot : GenericThrowControl {


	public enum TypeOfChargedShot{ fireCracker, Nade};
	public TypeOfChargedShot typeOfCS;



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

	public bool gravityAmmo;

	void Start () {
		currentAmmo2 = maxAmmo2;
		at2 = gameObject.transform.root.GetComponent<Attributes> ();
		print ("added charged");

	}
	void Update () {
		ChargesShotFct ();
	}
	//****
	void AdequateAbility()
	{
		/*
		if (typeOfCS == TypeOfChargedShot.fireCracker) {
			thisAbilityName = "firecracker";
		}
		*/
	}

	public void SetAbilityCharacteristics(float fR, int maxA, Rigidbody proyect)
	{
		
	}

	public void AbilitySelected()
	{
		at2.UIC.UpdateAbilityInfo(thisAbilityName,fRate2, currentAmmo2, maxAmmo2 );
	}

	public void SetAmmoCharacteristics()
	{


	}
	//From Send Message
	public void PickedUpAnotherOfTheSameKind(GameObject brotherOne)
	{
		ChargedShot cs;
		cs = brotherOne.GetComponent<ChargedShot> ();

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
		currentAmmo2 = maxAmmo2;
		at2.UIC.UpdateTheAmmoUI (currentAmmo2, maxAmmo2);
	}

	public void ChargesShotFct()
	{
		if (nextFire2 < Time.time && currentAmmo2 > 0)
		{
			print ("Chargin");
			if (Input.GetMouseButtonDown (0)) {
				
				startOfShot2 = Time.time;
				mouseDownOnTime2 = true;
			}
			if (Input.GetMouseButton (0)) {
				endOfShot2 = Time.time;
				totalShot2 = (endOfShot2 - startOfShot2) * strenghtMultiplier;
				if (totalShot2 > maxChargedShot) {
					totalShot2 = maxChargedShot;
				}
				base.UpdateUI(totalShot2, maxChargedShot);
			}
			if (Input.GetMouseButtonUp (0) && mouseDownOnTime2 ) 
			{
				endOfShot2 = Time.time;
				totalShot2 = (endOfShot2 - startOfShot2);
				HandleAlternationAndCoolDowns(totalShot2);
				totalShot2 = 0;
				startOfShot2 = 0;
				ReduceAmmo ();
				UpdateUI (totalShot2, maxChargedShot);
				mouseDownOnTime2 = false;
			}
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
			if (nextFire2 < Time.time) {
				if (alternator2) {
					if (at2.pH.R) {
						if (gravityAmmo) {
							base.ThrowItDouble (proyectile, force * chargedShotSpeed, 1, true);
						} else {
							base.ThrowItDouble (proyectile, force * chargedShotSpeed, 1, false);

						}
					//	genericNextFire = base.GenericNextFireforDouble (genericNextFire);
					}
				}
				if (!alternator2) {
					if (at2.pH.L) {
						if (at2.pH.R) {

							if (gravityAmmo) {
								base.ThrowItDouble (proyectile, force * chargedShotSpeed, 2, true);
							} else {
								base.ThrowItDouble (proyectile, force * chargedShotSpeed, 2, false);
							}
						//genericNextFire = base.GenericNextFireforDouble (genericNextFire);
					}
				}
				alternator2 = !alternator2;

				}
			}
		}
	}
}
