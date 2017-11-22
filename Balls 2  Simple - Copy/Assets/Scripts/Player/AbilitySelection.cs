using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct AbiltyInfo2{
	public int MaxAmmo;
	public int objCurAmmo;
	public int objType;
	public int objExpel;
	public float objFireRate;
	public string objName;
	GameObject gO;

	public AbiltyInfo2(int max, int cur, int type, int expel, float rate, string name, GameObject gaO)
	{
		MaxAmmo = max;
		objCurAmmo = cur;
		objType = type;
		objExpel= expel;
		objFireRate = rate;
		objName = name;
		gO = gaO;
	}
}


public class AbilitySelection : MonoBehaviour {
	Attributes at;
	KeyCode delete;
	public List <GameObject> abilities  = new List<GameObject>();

	public bool changeToAmmoAsSoonAsAdded;
	public bool notifyPlayerWhenPickedAWeapponWithWeapponName = true;

	public bool key1;
	public bool key2;
	public bool key3;
	public bool key4;
	public bool key5;
	public bool key6;
	public bool key7;
	public bool key8;

	public bool key1Enable;
	public bool key2Enable;
	public bool key3Enable;
	public bool key4Enable;
	public bool key5Enable;
	public bool key6Enable;
	public bool key7Enable;
	public bool key8Enable;

	int obj1MaxAmmo;
	int obj1CurAmmo;
	int obj1Type;
	float obj1NextFire; 
	float obj1FireRate;
	int obj1Expel;
	string obj1Name;

	int obj2MaxAmmo;
	int obj2CurAmmo;
	int obj2Type;
	float obj2NextFire; 
	float obj2FireRate;
	int obj2Expel;
	string obj2Name;

	int obj3MaxAmmo;
	int obj3CurAmmo;
	int obj3Type;
	float obj3NextFire;
	float obj3FireRate;
	int obj3Expel;
	string obj3Name;

	int obj4MaxAmmo;
	int obj4CurAmmo;
	int obj4Type;
	float obj4NextFire;
	float obj4FireRate;
	int obj4Expel;
	string obj4Name;

	int obj5MaxAmmo;
	int obj5CurAmmo;
	int obj5Type;
	float obj5NextFire;
	float obj5FireRate;
	int obj5Expel;
	string obj5Name;

	int obj6MaxAmmo;
	int obj6CurAmmo;
	int obj6Type;
	float obj6NextFire;
	float obj6FireRate;
	int obj6Expel;
	string obj6Name;

	int obj7MaxAmmo;
	int obj7CurAmmo;
	int obj7Type;
	float obj7NextFire;
	float obj7FireRate;
	int obj7Expel;
	string obj7Name;

	int obj8MaxAmmo;
	int obj8CurAmmo;
	int obj8Type;
	float obj8NextFire;
	float obj8FireRate;
	int obj8Expel;
	string obj8Name;

	public bool allowOneOfEachType;
	GenericThrowControl generic;
	//MonoBehaviour it;

	//public List <MonoBehaviour> CurrentAbilities = new List<MonoBehaviour>();
	//public List <AbiltyInfo2> Abilities = new List<AbiltyInfo2>();
	public List <string> myAbilities = new List <string>();



	void OnEnable()
	{
		generic = this.GetComponent<GenericThrowControl> ();
		delete = this.GetComponent<Attributes> ().abilityDelete;
		at = GetComponent<Attributes> ();
	}
	void Update()
	{
		if (at.isSelected) {
			//Change Here Update And Selection Type, depending if just selected or if FPS
			UpdateAbilityCooldowns ();
			if (!Input.GetKey (at.shifter) || Input.GetKey (delete)) {
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					AbilitySelection2 (1);
				}
				if (Input.GetKeyDown (KeyCode.Alpha2)) {
					AbilitySelection2 (2);
				}		
				if (Input.GetKeyDown (KeyCode.Alpha3)) {
					AbilitySelection2 (3);
				}
				if (Input.GetKeyDown (KeyCode.Alpha4)) {
					AbilitySelection2 (4);
				}
				if (Input.GetKeyDown (KeyCode.Alpha5)) {
					AbilitySelection2 (5);
				}
				if (Input.GetKeyDown (KeyCode.Alpha6)) {
					AbilitySelection2 (6);
				}
				if (Input.GetKeyDown (KeyCode.Alpha7)) {
					AbilitySelection2 (7);
				}
				if (Input.GetKeyDown (KeyCode.Alpha8)) {
					AbilitySelection2 (8);
				}
			}

			if (Input.GetKey (delete)) {
				DiscardAbility ();
			}
		}
	}
  
		/*
	public void CheckIfWeAllreadyHaveThisAbility(int type, int maxAmmo, int curAmmo, int expel, string abilityName, GameObject abilityObj)
	{
		if (obj1Type == type || obj2Type == type || obj3Type == type || obj4Type == type || obj5Type == type || obj6Type == type || obj7Type == type || obj8Type == type) {

			//JustAddAmmo (type, maxAmmo, curAmmo);
		} else {
			AbilityImplementation (type, maxAmmo, curAmmo, expel, abilityName, abilityObj);
		}

		if (notifyPlayerWhenPickedAWeapponWithWeapponName) {
			at.UIC.NotifyPlayer ("Just Picked: " + abilityName);
		}

	}
	*/

	public bool CheckIfHave(string it)
	{

		//did this cuz i could not compare the enums
		bool haveit = false;
		if (myAbilities.Count != 0) {
			for (int i = 0; i < abilities.Count ; i++) {
				if (myAbilities [i] == it )
				{
					haveit = true;
					return true;


				} 
			}
			if (!haveit) {
				//Implement (ability);
				return false;
			}
		} else {
			return false;
			//Implement (ability);				
		}
		return false;



	}

	public void CheckIfWeHaveIt(GameObject ability, string nameOfAbility, float fR, int maxAmmo)
	{
		bool haveit = false;
		if (abilities.Count != 0) {
			for (int i = 0; i < abilities.Count ; i++) {

				if (abilities [i].GetComponent<AbilityIdentifier> ().nameOfAbility == nameOfAbility) {
					print ("two of the same");
					haveit = true;
					abilities [i].SendMessage ("PickedUpAnotherOfTheSameKind", abilities [i].gameObject);
				} 
			}
			if (!haveit) {
				Implement ();		
				DoCorresponding (ability,nameOfAbility, fR, maxAmmo);

			}
		} else {
			Implement ();
			DoCorresponding (ability,nameOfAbility, fR, maxAmmo);

		}
	}

	public void Implement()
	{
		if (!key1) {
			key1 = true;
			return;
		}
		if (!key2) {
			key2 = true;
			return;
		}
		if (!key3) {
			key3 = true;
			return;
		}
		if (!key4) {
			key2 = true;
			return;
		}		
	}
	/*
	public void AbilityImplementation( int type, int max, int cur, int expel, string name, GameObject abObj)
	{
		if (abObj == null) {		
			print ("tried adding a null");
		}			
		if (!key1) {
			DoCorresponding (abObj);
			key1 = true;
			return;
		}
		if (!key2) {
			DoCorresponding (abObj);
			key2 = true;
			return;
		}
		if (!key3) {
			GameObject ability = Instantiate (abObj, at.abilitiesCollection.position, at.abilitiesCollection.rotation) as GameObject;
			ability.transform.SetParent (at.abilitiesCollection);
			key3 = true;
			return;
		}
		if (!key4) {
			GameObject ability = Instantiate (abObj, at.abilitiesCollection.position, at.abilitiesCollection.rotation) as GameObject;
			ability.transform.SetParent (at.abilitiesCollection);
			key4 = true;
			return;
		}
	}
	*/
	void DoCorresponding(GameObject abObj2, string nameOfAbility, float fireR, int maxA)
	{
		GameObject ability = Instantiate (abObj2, at.abilitiesCollection.position, at.abilitiesCollection.rotation) as GameObject;
		ability.transform.SetParent (at.abilitiesCollection);
		ability.transform.name = "test";
		ability.GetComponent<AbilityIdentifier> ().nameOfAbility = nameOfAbility;
		abilities.Add (ability);

	}




		//ability.SendMessage("SetAmmoCharacteristics", fireR, 


			/*****
	}


	/*
	void JustAddAmmo(int it, int maxAmmo, int curAmmo)
	{
		if (obj1Type == it) {
			obj1CurAmmo = curAmmo;
			obj1MaxAmmo = maxAmmo;
			if (key1Enable) {
				ChangeGenericAmmo (obj1CurAmmo, obj1MaxAmmo);
			}
			return;
		}
		if (obj2Type == it) {
			obj2CurAmmo = curAmmo;
			obj2MaxAmmo = maxAmmo;
			if (key2Enable) {
				ChangeGenericAmmo (obj2CurAmmo, obj2MaxAmmo);
			}
			return;
		}
		if (obj3Type == it) {
			obj3CurAmmo = curAmmo;
			obj3MaxAmmo = maxAmmo;
			if (key3Enable) {
				ChangeGenericAmmo (obj3CurAmmo, obj3MaxAmmo);
			}
			return;
		}
		if (obj4Type == it) {
			obj4CurAmmo = curAmmo;
			obj4MaxAmmo = maxAmmo;
			if (key4Enable) {
				ChangeGenericAmmo (obj4CurAmmo, obj4MaxAmmo);
			}
			return;
		}
		if (obj5Type == it) {
			obj5CurAmmo = curAmmo;
			obj5MaxAmmo = maxAmmo;
			if (key5Enable) {
				ChangeGenericAmmo (obj5CurAmmo, obj5MaxAmmo);
			}
			return;
		}
		if (obj6Type == it) {
			obj6CurAmmo = curAmmo;
			obj6MaxAmmo = maxAmmo;
			if (key6Enable) {
				ChangeGenericAmmo (obj6CurAmmo, obj6MaxAmmo);
			}
			return;
		}
		if (obj7Type == it) {
			obj7CurAmmo = curAmmo;
			obj7MaxAmmo = maxAmmo;
			if (key7Enable) {
				ChangeGenericAmmo (obj7CurAmmo, obj7MaxAmmo);
			}
			return;
		}
		if (obj8Type == it) {
			obj8CurAmmo = curAmmo;
			obj8MaxAmmo = maxAmmo;

			if (key8Enable) {
				ChangeGenericAmmo (obj8CurAmmo, obj8MaxAmmo);
			}
			return;
		}
	}
	*/

	void ChangeGenericAmmo(int curA, int maxA)
	{
		this.GetComponent<GenericThrowControl> ().assignGenericAmmo (curA, maxA);
	}
	void UpdateAbilityCooldowns()
	{
			if (key1) {
			if (obj1NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (1, (obj1NextFire - Time.time)/obj1FireRate);
			}
		}
		if (key2) {
			if (obj2NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (2, (obj2NextFire - Time.time)/obj2FireRate);
			}
		}
		if (key3) {
			if (obj3NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (3, (obj3NextFire - Time.time)/obj3FireRate);
			}
		}
		if (key4) {
			if (obj4NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (4, (obj4NextFire - Time.time)/obj4FireRate);
			}
		}
		if (key5) {
			if (obj5NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (5, (obj5NextFire - Time.time)/obj4FireRate);
			}
		}
		if (key6) {
			if (obj6NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (6, (obj6NextFire - Time.time)/obj6FireRate);
			}
		}
		if (key7) {
			if (obj7NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (7, (obj7NextFire - Time.time)/obj7FireRate);
			}
		}
		if (key8) {
			if (obj8NextFire > Time.time) {
				this.GetComponent<UiCoordination> ().UpdateCoolDowns (8, (obj8NextFire - Time.time)/obj8FireRate);
			}
		}

	}
	void DiscardAbility()
	{		
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			key1 = false;
			obj1Type = 0;
			obj1CurAmmo = 0;
			obj1MaxAmmo = 0;
			obj1NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (1);
			if (key1Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			key2 = false;
			obj2Type = 0;
			obj2CurAmmo = 0;
			obj2MaxAmmo = 0;
			obj2NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (2);
			if (key2Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}		
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			key3 = false;
			obj3Type = 0;
			obj3CurAmmo = 0;
			obj3MaxAmmo = 0;
			obj3NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (3);
			if (key3Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			key4 = false;
			obj4Type = 0;
			obj4CurAmmo = 0;
			obj4MaxAmmo = 0;
			obj4NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (4);
			if (key4Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			key5 = false;
			obj5Type = 0;
			obj5CurAmmo = 0;
			obj5MaxAmmo = 0;
			obj5NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (5);
			if (key5Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			key6 = false;
			obj6Type = 0;
			obj6CurAmmo = 0;
			obj6MaxAmmo = 0;
			obj6NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (6);
			if (key6Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			key7 = false;
			obj7Type = 0;
			obj7CurAmmo = 0;
			obj7MaxAmmo = 0;
			obj7NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (7);
			if (key7Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			key8 = false;
			obj8Type = 0;
			obj8CurAmmo = 0;
			obj8MaxAmmo = 0;
			obj8NextFire = 0;
			this.GetComponent<UiCoordination> ().DestroyAbilityImage (8);
			if (key8Enable) {
				DiscardItAtGenericIfSelected ();
			}
		}
	}
	void DiscardItAtGenericIfSelected()
	{
		this.GetComponent<GenericThrowControl> ().EmptyCurrentGenericAmmo ();

	}


	void AbilitySelection2(int it)
	{

	
		if (abilities.Count != 0) {
			print ("wut");
			if (it <= abilities.Count) {				
				EnableKeys (it);
				AbilitySelected (it);
				for (int i = 0; i < abilities.Count ; i++) {
					abilities [i].gameObject.SetActive (false);	
				}
				abilities [it-1].gameObject.SetActive (true);
				abilities [it-1].gameObject.SendMessage ("AbilitySelected");

				print (abilities [it - 1].gameObject);


				//abilities [it - 1].gameObject.GetComponent<AbilityIdentifier> ().ThisAbilityWasSelected ();
			}
		}

		/*
		switch (it) {
		case 1:
			abilities [it - 1].gameObject.SetActive = true;
			EnableKeys (it);
			AbilitySelected (it);
			break;

		case 2:
			EnableKeys (2);
			AbilitySelected (2);
			break;

		case 3:
			EnableKeys (3);
			InformGenericThrowOfSelection (obj3Type, obj1CurAmmo, obj3MaxAmmo, obj3NextFire,obj3Expel,obj3FireRate, obj3Name);
			AbilitySelected (3);
			break;

		case 4:
			EnableKeys (4);
			InformGenericThrowOfSelection (obj4Type, obj4CurAmmo, obj4MaxAmmo, obj4NextFire,obj4Expel,obj4FireRate, obj4Name);
			AbilitySelected (4);
			break;
		case 5:
			EnableKeys (5);
			InformGenericThrowOfSelection (obj5Type, obj5CurAmmo, obj5MaxAmmo, obj5NextFire, obj5Expel,obj5FireRate, obj5Name);
			AbilitySelected (5);
			break;
		case 6:
			EnableKeys (6);
			InformGenericThrowOfSelection (obj6Type, obj6CurAmmo, obj6MaxAmmo, obj6NextFire,obj6Expel,obj6FireRate, obj6Name);
			AbilitySelected (6);
			break;
		case 7:
			EnableKeys (7);
			InformGenericThrowOfSelection (obj7Type, obj7CurAmmo, obj7MaxAmmo, obj7NextFire, obj7Expel,obj7FireRate, obj7Name);
			AbilitySelected (7);
			break;
		case 8:
			EnableKeys (8);
			InformGenericThrowOfSelection (obj8Type, obj8CurAmmo, obj8MaxAmmo, obj8NextFire, obj8Expel,obj8FireRate, obj8Name);
			AbilitySelected (8);
			break;
		}	
		*/
	}
	void EnableKeys(int it)
	{
		key1Enable = false;
		key2Enable = false;
		key3Enable = false;
		key4Enable = false;
		key5Enable = false;
		key5Enable = false;
		key6Enable = false;
		key7Enable = false;
		key8Enable = false;

		switch(it)
		{
		case 1:
			key1Enable = true;
			break;
		case 2:
			key2Enable = true;
			break;
		case 3:
			key3Enable = true;
			break;
		case 4:
			key4Enable = true;
			break;
		case 5:
			key5Enable = true;
			break;
		case 6:
			key6Enable = true;
			break;
		case 7:
			key7Enable = true;
			break;
		case 8:
			key8Enable = true;
			break;
		}
	}
	void InformGenericThrowOfSelection(int type,int curA,int maxA, float nxtF, int exp, float fireR, string name)
	{
		at.gTC.GetCurrentAmmoDetails (type, curA, maxA, nxtF, exp, fireR, name);

	}
	void AbilitySelected(int slot)
	{
		this.GetComponent<UiCoordination> ().SelectAbilityUI (slot);
		//this.GetComponent<AimingSystem> ().endAimForHomming();

	}
	public void UpdateAmmoSpecificCooldowns()
	{
		
		if (key1Enable) {	
			obj1CurAmmo -= 1;
			obj1NextFire = Time.time + obj1FireRate;
		}
		if (key2Enable) {
			obj2CurAmmo -= 1;
			obj2NextFire = Time.time + obj2FireRate;
		}
		if (key3Enable) {
			obj3CurAmmo -= 1;
			obj3NextFire = Time.time + obj3FireRate;
		}
		if (key4Enable) {
			obj4CurAmmo -= 1;
			obj4NextFire = Time.time + obj4FireRate;
		}
		if (key5Enable) {
			obj5CurAmmo -= 1;
			obj5NextFire = Time.time + obj5FireRate;
		}
		if (key6Enable) {
			obj6CurAmmo -= 1;
			obj6NextFire = Time.time + obj6FireRate;
		}
		if (key7Enable) {
			obj7CurAmmo -= 1;
			obj7NextFire = Time.time + obj7FireRate;
		}
		if (key8Enable) {
			obj8CurAmmo -= 1;
			obj8NextFire = Time.time + obj8FireRate;
		}
	}
		

}
