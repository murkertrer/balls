using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiCoordination : MonoBehaviour {
	Attributes at;

	public Font HealthRelated;
	public Image MouseAim;
	public Image yAim;
	public Image xAndYAim;
	public Image camaraTiltAsArm;

	bool prevMouse;
	int prevMouseInt;

	public AudioClip abilitySelectionSound;
	public AudioClip cantSelect;

	public Transform myWorldPower;
	public Transform screenCanvas;

	public Image myPowerBar;
	public Image myHealthBar;
	public Image myAngleBar;
	public Image myManaBar;

	public Text myTextHealth;
	public Text myTextPower;
	public Text myTextAngle;
	public Text myTextProyectile;
	public Text myHealthInText;
	public Text myManaInText;
	public Text notificationText;
	//public Text myUnitSelection;


	float curH;
	float maxH;
	float curM;
	float maxM;


	public GameObject healthBar;
	public GameObject WorldCanvas;
	public Camera m_Camera;
	GameObject h;



	public Image myCurrentShotImage;
	public Text myCurrentShotText;
	public Text myCurrentShotAmmo;
	public Text myCurrentFireRate;

	public Image  straightShot;
	public Image regularShot;
	public Image regularShotExpolison;
	public Image shieldManipulation;
	float xx;

	public bool ScreenInfo;
	public bool WorldInfor = true;


	public Image ability1Cooldown;
	public Image ability2Cooldown;
	public Image ability3Cooldown;
	public Image ability4Cooldown;
	public Image ability5Cooldown;
	public Image ability6Cooldown;
	public Image ability7Cooldown;
	public Image ability8Cooldown;

	public float ability1LocalY;

	public string type1  = "Gravity with explosion";
	public string type2  = "No Gravity with explosion";
	public string type3  = "Rotating placeble shield";
	public string type4  = "Gravity with implosion";
	public string type5  = "Machine Gun Gravity Bounce";
	public string type6  = "Machine Gravity No Bounce";
	public string type7  = "Grenade";
	public string type8  = "Homing Device";



	public Image slot1;
	public Image slot2;
	public Image slot3;
	public Image slot4;
	public Image slot5;
	public Image slot6;
	public Image slot7;
	public Image slot8;

	public Sprite blankSprite;
	public Sprite gravShotExplosion1;
	public Sprite noGravShotExplosion1;
	public Sprite shieldRotate1;
	public Sprite gravShotImplosion1;
	public Sprite bouncyNoGrav;
	public Sprite bouncyNoGravNo;
	public Sprite grenade;
	public Sprite homing;
	public Sprite boomerang;

	public GameObject eventManger;
	Color selectionColor = Color.green;
	Color normalColor;
	Color enabledColor = Color.gray;
	Image previousSelection;
	int prevAbility;

	public Image cd1;
	public Image cd2;
	public Image cd3;
	public Image cd4;
	public Image cd5;
	public Image cd6;
	public Image cd7;
	public Image cd8;
	public Image cdb1;
	public Image cdb2;
	public Image cdb3;
	public Image cdb4;
	public Image cdb5;
	public Image cdb6;
	public Image cdb7;
	public Image cdb8;

	Color cdC =Color.red;
	Color cdbC =Color.green;
	Color notificationColor = Color.cyan;
	KeepTrack kT;

	public Image boomerangInfo;
	bool dimNotifyText;


	public Vector3 lclSale  = new Vector3 (.05f, .05f, .05f);


	float w;

	/* 1 - Grav Explosion
	 * 2-Straight Explosion
	 * 3-Shield rotate
	 * 4-Gravi Implosion
	 *  * 5=Gravity Bouncy MachinGun;
	 * 6 = No Garvity bouncy;
	 * 7 = Homing Missle
	 * 8 = Gravity
	 * 
	 * Expel Methood
	 * 1 - chargedShot
	 * 2 FixedShot
	 * 3 Machine Gun
	 * 4 Place
	 * 5 place And Follow;
	*/


	void Update()
	{
		if (at.isSelected) {

			//Check Here To Update Both Arms
			at.kT.generealUIC.UpdateAngleUi (Mathf.Round(at.myArm.transform.localEulerAngles.x));
			curH = this.transform.GetComponent<Attributes> ().currentHealth;
			curH = Mathf.Round(curH);
			maxH = this.transform.GetComponent<Attributes> ().maxHealth;
			maxH = Mathf.Round (maxH);
			at.kT.generealUIC.UpdateHealthUI  (
				Mathf.Round(at.currentHealth), 
				Mathf.Round(at.maxHealth) 
			);
		} 


		/*
			UpdateHealthUI ();
			UpdateManaUI ();
			AngleCalculation ();
			UpdateAngleUi (xx); 
		*/
	}
	/*
				slot1 = gravShotExplosion;
				Color it = slot1.color;
				it.a = .5f;
				slot1.color = it;
				slot1.GetComponent<Image> ().enabled = false;
				print ("called");
				print ("slot  " + slot + "type  " + type);
				slot1.GetComponent<Image> ().sprite = gravShotExplosion1;
				slot1.transform.gameObject.SetActive (false);
				*/
	public void NotifyPlayer(string it)
	{
		at.kT.generealUIC.Notification (it);

	}
	public IEnumerator FadeNotificationText(float time)
	{
		yield return new WaitForSeconds(3);
		dimNotifyText = true;
		yield return new WaitForSeconds(time-3);
		dimNotifyText = false;
		yield return null;
	}
	void DimText ()
	{
		if (dimNotifyText) {
			Color it = notificationText.color;
			it.a -= .05f;
			notificationText.color = it;
		}
	}
	void OnEnable()
	{
		at = GetComponent<Attributes> ();
	}
	void Start()
	{
		eventManger = GameObject.Find ("EventManager");
		kT = eventManger.GetComponent<KeepTrack> ();
		boomerangInfo = kT.BoomerangInfo;
		myCurrentShotAmmo = kT.curAmmo;
		myCurrentShotText = kT.currentShot;
		myCurrentFireRate = kT.fireRate;
		myAngleBar = kT.angleBar;
		myHealthBar = kT.healthBar;
		myHealthInText = kT.healthTxt;
		myTextAngle = kT.angleTxt;
		myManaBar = kT.manaBar;
		myManaInText = kT.manaText;
		notificationText = kT.notificationCenter;
		//notificationColor = notificationText.color;

		MouseAim = kT.MouseAim;
		yAim = kT.yAim;
		xAndYAim = kT.MouseAim;
		camaraTiltAsArm = kT.CamaraTiltAsArm;

		//ability1LocalY = myWorldPower.transform.localScale.y;
		UpdatePowerUI (0, 0);

		blankSprite = kT.blankSprite;
		normalColor = kT.normalColor;;
		//selectionColor.a = .5f;
		cdC.a = .5f;
		cdbC.a = .5f;
		cd1 = kT.cd1;
		cd2 = kT.cd2;
		cd3 = kT.cd3;
		cd4 = kT.cd4;
		cd5 = kT.cd5;
		cd6 = kT.cd6;
		cd7 = kT.cd7;
		cd8 = kT.cd8;

		cdb1 = kT.cdb1;
		cdb2 = kT.cdb2;
		cdb3 = kT.cdb3;
		cdb4 = kT.cdb4;
		cdb5 = kT.cdb5;
		cdb6 = kT.cdb6;
		cdb7 = kT.cdb7;
		cdb8 = kT.cdb8;

		slot1 = kT.slot1;
		slot2 = kT.slot2;
		slot3 = kT.slot3;
		slot4 = kT.slot4;
		slot5 = kT.slot5;
		slot6 = kT.slot6;
		slot7 = kT.slot7;
		slot8 = kT.slot8;

		myPowerBar = kT.myPowerBar;
		myManaBar = kT.myManaBar;
		myHealthBar = kT.myHealthBar;
		myAngleBar = kT.myAngleBar;


		myTextHealth= kT.myTextHealth;
		myTextPower = kT.myTextPower;
		myTextAngle = kT.myTextAngle;
		myTextProyectile = kT.myTextProyectile;
		myHealthInText =myHealthInText;
		myManaInText = kT.myManaInText;

		/*
		cd1.color = cdC;
		cd2.color = cdC;
		cd3.color = cdC;
		cd4.color = cdC;
		cd5.color = cdC;
		cd6.color = cdC;
		cd7.color = cdC;
		cd8.color = cdC;
		cdb1.color = cdbC;
		cdb2.color = cdbC;
		cdb3.color = cdbC;
		cdb4.color = cdbC;
		cdb5.color = cdbC;
		cdb6.color = cdbC;
		cdb7.color = cdbC;
		cdb8.color = cdbC;
		*/
	}


	public void HandleBoomerangInfo (float with, float rotation)
	{
		Vector3 rot = new Vector3 (0, 0, rotation);
		boomerangInfo.transform.eulerAngles = rot;
		w = with*20;
		Vector2 Wide = new Vector2 (w, boomerangInfo.rectTransform.sizeDelta.y);
		boomerangInfo.rectTransform.sizeDelta = Wide;
	}

	public void UpdateImageUI(int slot, int type)
	{
		switch (slot) {
		case 1:
			slot1.color = enabledColor;
			break;
		case 2:
			slot2.color = enabledColor;
			break;
		case 3:
			slot3.color = enabledColor;
			break;
		case 4:
			slot4.color = enabledColor;
			break;
		case 5:
			slot5.color = enabledColor;
			break;
		case 6:
			slot6.color = enabledColor;
			break;
		case 7:
			slot7.color = enabledColor;
			break;
		case 8:
			slot8.color = enabledColor;
			break;

		}
		if (slot == 1) {
			if (type == 1) {
				slot1.sprite = gravShotExplosion1; 
			}
			if (type == 2) {
				slot1.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot1.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot1.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot1.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot1.sprite = bouncyNoGrav;
			}
			if (type == 7) {
				slot1.sprite = homing;
			}
			if (type == 8) {
				slot1.sprite = grenade;
			}

			if (type == 10) {
				slot1.sprite = boomerang;
			}

		}
		if (slot == 2) {
			if (type == 1) {
				slot2.sprite = gravShotExplosion1;
			}
			if (type == 2) {
				slot2.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot2.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot2.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot2.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot2.sprite = bouncyNoGravNo;
			}
			if (type == 7) {
				slot2.sprite = homing;
			}
			if (type == 8) {
				slot2.sprite = grenade;
			}

			if (type == 10) {
				slot2.sprite = boomerang;
			}
		}
		if (slot == 3) {
			if (type == 1) {
				slot3.sprite = gravShotExplosion1;
			}
			if (type == 2) {
				slot3.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot3.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot3.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot3.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot3.sprite = bouncyNoGravNo;
			}
			if (type == 7) {
				slot3.sprite = homing;
			}
			if (type == 8) {
				slot3.sprite = grenade;
			}


			if (type == 10) {
				slot3.sprite = boomerang;
			}
		}
		if (slot == 4) {
			if (type == 1) {
				slot4.sprite = gravShotExplosion1;
			}
			if (type == 2) {
				slot4.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot4.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot4.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot4.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot4.sprite = bouncyNoGravNo;
			}
			if (type == 7) {
				slot4.sprite = homing;
			}
			if (type == 8) {
				slot4.sprite = grenade;
			}


			if (type == 10) {
				slot4.sprite = boomerang;
			}
		}
		if (slot == 5) {
			if (type == 1) {
				slot5.sprite = gravShotExplosion1;
			}
			if (type == 2) {
				slot5.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot5.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot5.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot5.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot5.sprite = bouncyNoGravNo;
			}
			if (type == 7) {
				slot5.sprite = homing;
			}
			if (type == 8) {
				slot5.sprite = grenade;
			}


			if (type == 10) {
				slot5.sprite = boomerang;
			}
		}
		if (slot == 6) {
			if (type == 1) {
				slot6.sprite = gravShotExplosion1;
			}
			if (type == 2) {
				slot6.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot6.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot6.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot6.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot6.sprite = bouncyNoGravNo;
			}
			if (type == 7) {
				slot6.sprite = homing;
			}
			if (type == 8) {
				slot6.sprite = grenade;
			}


			if (type == 10) {
				slot6.sprite = boomerang;
			}
		}
		if (slot == 7) {
			if (type == 1) {
				slot7.sprite = gravShotExplosion1;
			}
			if (type == 2) {
				slot7.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot7.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot7.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot7.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot7.sprite = bouncyNoGravNo;
			}
			if (type == 7) {
				slot7.sprite = homing;
			}
			if (type == 8) {
				slot7.sprite = grenade;
			}
			if (type == 10) {
				slot7.sprite = boomerang;
			}
		}
		if (slot == 8) {
			if (type == 1) {
				slot8.sprite = gravShotExplosion1;
			}
			if (type == 2) {
				slot8.sprite = noGravShotExplosion1;
			}
			if (type == 3) {
				slot8.sprite = shieldRotate1;
			}
			if (type == 4) {
				slot8.sprite = gravShotImplosion1;
			}
			if (type == 5) {
				slot8.sprite = bouncyNoGrav;
			}
			if (type == 6) {
				slot8.sprite = bouncyNoGravNo;
			}
			if (type == 7) {
				slot8.sprite = homing;
			}
			if (type == 8) {
				slot8.sprite = grenade;
			}
			if (type == 10) {
				slot8.sprite = boomerang;
			}
		}
	}
	public void UpdateMouseAimUI(int slot)
	{
		if (slot == 1) {
			MouseAim.color = Color.green;
			yAim.color = Color.blue;;
			xAndYAim.color = Color.blue;
			camaraTiltAsArm.color = Color.blue;

		}
		if (slot == 2) {
			MouseAim.color = Color.blue;
			yAim.color = Color.green;
			xAndYAim.color = Color.blue;
			camaraTiltAsArm.color = Color.blue;

		}
		if (slot == 3) {
			MouseAim.color = Color.blue;
			yAim.color = Color.blue;;
			xAndYAim.color = Color.green;
			camaraTiltAsArm.color = Color.blue;

		}
		if (slot == 4) {
			MouseAim.color = Color.blue;
			yAim.color = Color.blue;;
			xAndYAim.color = Color.blue;
			camaraTiltAsArm.color = Color.green;

		}
	}
	public void DestroyAbilityImage(int slot)
	{
		switch (slot) {
		case 1:
			slot1.sprite = blankSprite;
			slot1.color = normalColor;
			break;
		case 2:
			slot2.sprite = blankSprite;
			slot2.color = normalColor;

			break;
		case 3:
			slot3.sprite = blankSprite;
			slot3.color = normalColor;
			break;
		case 4:
			slot4.sprite = blankSprite;
			slot4.color = normalColor;

			break;
		case 5:
			slot5.sprite = blankSprite;
			slot5.color = normalColor;

			break;
		case 6:
			slot6.sprite = blankSprite;
			slot6.color = normalColor;

			break;
		case 7:
			slot7.sprite = blankSprite;
			slot7.color = normalColor;

			break;
		case 8:
			slot8.sprite = blankSprite;
			slot8.color = normalColor;

			break;
		}
	}
	public void SelectAbilityUI(int slot)
	{
		AbilitySelection generic = this.GetComponent<AbilitySelection>();

			if (prevAbility != null) {
			switch (prevAbility) {
			case 1:
				//slot1.color = normalColor;
	
					if (slot1.sprite != blankSprite) {
						slot1.color = enabledColor;
					}
				
				break;
			case 2:
				//slot2.color = normalColor;
					if (slot2.sprite != blankSprite) {
					slot2.color = enabledColor;
				}
				break;
			case 3:
				//slot3.color = normalColor;
				if (slot3.sprite != blankSprite) {
					slot3.color = enabledColor;
				}
				break;
			case 4:
				//slot4.color = normalColor;
				if (slot4.sprite != blankSprite) {
					slot4.color = enabledColor;
				}
				break;
			case 5:
				//slot5.color = normalColor;
				if (slot5.sprite != blankSprite) {
					slot5.color = enabledColor;
				}
				break;
			case 6:
				//slot6.color = normalColor;
				if (slot6.sprite != blankSprite) {
					slot6.color = enabledColor;
				}
				break;
			case 7:
				//slot7.color = normalColor;
				if (slot7.sprite != blankSprite) {
					slot7.color = enabledColor;
				}
				break;
			case 8:
				//slot8.color = normalColor;
				if (slot8.sprite != blankSprite) {
					slot8.color = enabledColor;
				}
				break;

			}
		}
		switch (slot) {
		case 1:
			if (generic.key1) {
				slot1.color = selectionColor;
				prevAbility = 1;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();

			}
			break;
		case 2:
			if (generic.key2) {
				slot2.color = selectionColor;
				prevAbility = 2;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();
			}
			break;
		case 3:
			if (generic.key3) {
				slot3.color = selectionColor;
				prevAbility = 3;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();
			}
			break;
		case 4:
			if (generic.key4) {
				slot4.color = selectionColor;
				prevAbility = 4;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();
			}
				break;
		case 5:
			if (generic.key5) {
				slot5.color = selectionColor;
				prevAbility = 5;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();
			}
			break;
		case 6:
			if (generic.key6) {
				slot6.color = selectionColor;
				prevAbility = 6;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();
			}
			break;
		case 7:
			if (generic.key7) {
				slot7.color = selectionColor;
				prevAbility = 7;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();
			}
			break;
		case 8:
			if (generic.key8) {
				slot8.color = selectionColor;
				prevAbility = 8;
				PlaySoundAbilitySelected ();
			} else {
				PlaySoundCantSelect ();
			}
			break;
		}
		
	}
	void PlaySoundAbilitySelected()
	{
		AudioSource.PlayClipAtPoint (this.abilitySelectionSound, this.GetComponent<Attributes> ().myBall.transform.position);
	}
	void PlaySoundCantSelect()
	{
		AudioSource.PlayClipAtPoint (cantSelect, this.GetComponent<Attributes> ().myBall.transform.position);
		UpdateTheAmmoUIName ("nullo",0);

	}
	public void UpdateCoolDowns(int key, float remainingTime)
	{
		if (!System.Single.IsNaN (remainingTime)) 
		{
			if (key == 1) {
				cd1.transform.localScale = new Vector3 (remainingTime, cd1.transform.localScale.y, cd1.transform.localScale.z);
			}
			if (key == 2) {
				cd2.transform.localScale = new Vector3 (remainingTime, cd2.transform.localScale.y, cd2.transform.localScale.z);
			}
			if (key == 3) {
				cd3.transform.localScale = new Vector3 (remainingTime, cd3.transform.localScale.y, cd3.transform.localScale.z);
			}
			if (key == 4) {
				cd4.transform.localScale = new Vector3 (remainingTime, cd4.transform.localScale.y, cd4.transform.localScale.z);
			}
			if (key == 5) {
				cd5.transform.localScale = new Vector3 (remainingTime, cd5.transform.localScale.y, cd5.transform.localScale.z);
			}
			if (key == 6) {
				cd6.transform.localScale = new Vector3 (remainingTime, cd6.transform.localScale.y, cd6.transform.localScale.z);
			}
			if (key == 7) {
				cd7.transform.localScale = new Vector3 (remainingTime, cd7.transform.localScale.y, cd7.transform.localScale.z);
			}
			if (key == 8) {
				cd8.transform.localScale = new Vector3 (remainingTime, cd8.transform.localScale.y, cd8.transform.localScale.z);
			}
		}
	}
	public void UpdateCoolDowns(float one, float two, float three)
	{
		ability1Cooldown.transform.localScale= new Vector3 (one, ability1Cooldown.transform.localScale.y, ability1Cooldown.transform.localScale.z);

	}
	void AngleCalculation()
	{
		xx = this.GetComponent<Attributes> ().myArm.transform.localEulerAngles.x;
		xx = xx * -1;
		if (xx < 26) {
			xx = xx * -1;
		}
		if (xx > 26) {
			xx = (xx -360) *-1 ;
		}
		xx = Mathf.Round (xx);
	}
	public void UpdateAngleUi (float anglez)
	{
		if (anglez < 90) {
			myTextAngle.text = anglez.ToString ();
			if (!System.Single.IsNaN (anglez)) {
				float equalized = anglez / 90;
				if (equalized < 91) {
					//this.GetComponent<AimingSystem>().yMaxLimit *-1
					myAngleBar.transform.localScale = new Vector3 (equalized, myAngleBar.transform.localScale.y, myAngleBar.transform.localScale.x);
				}
			}
		}
		//myPowerBar.transform.localScale = new Vector3 (anglez, myPowerBar.transform.localScale.y, myPowerBar.transform.localScale.z);

	}
	public void UpdatePowerUI(float currentPower, float maxPower)
	{
		float calculatePower = currentPower / maxPower;
		if (!System.Single.IsNaN (calculatePower) && !System.Single.IsNaN (maxPower)) {
			
			myPowerBar.transform.localScale = new Vector3 (calculatePower , myPowerBar.transform.localScale.y, myPowerBar.transform.localScale.z);
			currentPower = Mathf.Round (currentPower);
			myTextPower.text = currentPower.ToString() + "  /  " + maxPower.ToString();
		}
	}
	public void UpdateHealthUI()
	{
		curH = this.transform.GetComponent<Attributes> ().currentHealth;
		curH = Mathf.Round(curH);
		maxH = this.transform.GetComponent<Attributes> ().maxHealth;
		maxH = Mathf.Round (maxH);
		float calculateHealth = curH / maxH;
		myHealthInText.text =  curH+ " / " + maxH;
		myHealthBar.transform.localScale = new Vector3 (calculateHealth, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
	}
	void UpdateManaUI ()
	{
		curM = Mathf.Round (this.transform.GetComponent<Attributes> ().currentMana);
		maxM = Mathf.Round (this.transform.GetComponent<Attributes> ().maxMana);
		float calculataMana = curM / maxM;
		myManaInText.text = curM +" / " + maxM;
		if (!System.Single.IsNaN (calculataMana)) {
			myManaBar.transform.localScale = new Vector3 (calculataMana, myManaBar.transform.localScale.y, myManaBar.transform.localScale.z);
		}
	}
	public void UpdateTheAmmoUI(int curAmmo, int maxAmmo)
	{
		if (myCurrentShotAmmo) {
			myCurrentShotAmmo.text = curAmmo.ToString () + " / " + maxAmmo.ToString ();
		}
	}
	public void UpdateTheAmmoUIName(string name, float fireR)
	{
		myCurrentFireRate.text = "fire rate: " + fireR.ToString ();
		myCurrentShotText.text = name;
		/*
		if (myCurrentShotText) {

			switch (it) {
			case 0:
				myCurrentShotText.text = "";
				break;
			case 1:
				myCurrentShotText.text = type1;
				break;
			case 2:
				myCurrentShotText.text = type2;

				break;
			case 3:
				myCurrentShotText.text = type3;

				break;
			case 4:
				myCurrentShotText.text = type4;
				break;
			case 5:
				myCurrentShotText.text = type5;
				break;
			case 6:
				myCurrentShotText.text = type6;
				break;
			case 7:
				myCurrentShotText.text = type7;
				break;
			case 8:
				myCurrentShotText.text = type8;
				break;
			}
		}
		*/
	}

	public void UpdateAbilityInfo(string name, float fireR, int curAmmo, int maxAmmo)
	{
		myCurrentFireRate.text = "fire rate: " + fireR.ToString ();
		myCurrentShotText.text = name;
		myCurrentShotAmmo.text = curAmmo.ToString () + " / " + maxAmmo.ToString ();		
	}




	public void HealthActionNotification(float amount, int it)
	{
		GameObject go = new GameObject ("Cool Game Object made from Code");
		go.transform.parent = eventManger.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;
		go.transform.position = this.GetComponent<Attributes> ().myBall.position;
		go.AddComponent<CanvasRenderer>();
		go.AddComponent<Text> ();
		go.GetComponent<Text> ().fontSize = 20;

		if (it == 1) {
			go.GetComponent<Text> ().color = Color.red;
		}
		if (it == 2) {
			go.GetComponent<Text> ().color = Color.green;
		}

		go.GetComponent<Text> ().font = HealthRelated;
		go.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		amount = Mathf.Round (amount);
		go.GetComponent<Text> ().text = amount.ToString();
		go.transform.localScale	= lclSale;
		go.AddComponent<TextNotificationFloat> ();
		go.GetComponent<TextNotificationFloat> ().playerT = this.GetComponent<Attributes> ().myBall;
		go.GetComponent<TextNotificationFloat> ().playerCam = this.GetComponent<Attributes> ().myCam.transform;
		go.GetComponent<TextNotificationFloat> ().DestroyText ();

	}


}

