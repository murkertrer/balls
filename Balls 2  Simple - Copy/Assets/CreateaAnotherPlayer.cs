using UnityEngine;
using System.Collections;

public class CreateaAnotherPlayer : MonoBehaviour {

	Attributes at;

	public GameObject player;

	void OnTriggerEnter(Collider col)
	{
		if (col.transform.root.GetComponent<Attributes>())
		{
			//GameObject baby = Instantiate (player, transform.position, Quaternion.identity) as GameObject;
			GameObject baby = Instantiate (col.transform.root.gameObject, transform.position, Quaternion.identity) as GameObject;
			at = baby.transform.root.GetComponent<Attributes> ();
			at.myCam.enabled = false;
			at.myCam.transform.GetComponent<AudioListener>().enabled = false;
			at.DeSelect ();
			if (!at.myBall.gameObject.GetComponent<SelectableUnitComponent> ()) {
				at.myBall.gameObject.AddComponent<SelectableUnitComponent> ();
			}
		

		/*
		col.transform.gameObject.GetComponent<ComanderScript> ().AddPlayerToCommandedControll (baby.GetComponent<Attributes>().myBall
			.GetComponent<SelectableUnitComponent>());
			*/


		//at.AssignHealthBarRtsCam (at.kT.kingCamera.transform);
		at.CreateRTS();
		//at.pH.TurnOffFPSStuff();
		Destroy (gameObject);
		}
	}
}
