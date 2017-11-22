using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityIdentifier : MonoBehaviour {

	public string nameOfAbility;

	/*

	public float fireRate;
	public int maxAmmo;
	bool adequatio;
	Attributes at;
	public Rigidbody testo;

	public List <string> nameOf = new List<string> ();
	public enum TypeOf{ chargeShotSmallExposion,
		StraightShotBounce,
		FireCracker

		}
	public TypeOf typeOf;

	public void PickedUpAnotherOfTheSameKind(GameObject it)
	{
		if (typeOf == TypeOf.chargeShotSmallExposion) {
			GetComponent<ChargedShot> ().PickedUpAnotherOfTheSameKind (it);;
		}
	}



	public void ThisAbilityWasSelected()
	{
		
		if (typeOf == TypeOf.FireCracker) {
			if (adequatio) {
				GetComponent<ChargedShot> ().AbilitySelected ();			
			} else {
				gameObject.AddComponent<ChargedShot> ();
				gameObject.GetComponent<ChargedShot> ().proyectile = gameObject.transform.root.GetComponent<Attributes> ().firecracker as Rigidbody;
				adequatio = true;

			}

		}
	}
	void OnEnable()
	{
		
		at = gameObject.transform.root.GetComponent<Attributes> ();
		if (at.firecracker) {
			print ("f");
		}

	}
	void Start()
	{


		if (typeOf == TypeOf.FireCracker) {
			gameObject.AddComponent<ChargedShot> ();
			//gameObject.GetComponent<ChargedShot> ().proyectile = testo;
			adequatio = true;
		}
		
		
	}
	*/
}
