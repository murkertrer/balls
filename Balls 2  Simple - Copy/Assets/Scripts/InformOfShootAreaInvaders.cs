using UnityEngine;
using System.Collections;

public class InformOfShootAreaInvaders : MonoBehaviour {

	public bool isRight;
	public bool isLeft;
	public bool haveStilOnTrigger;

	public Transform aim1;
	public Transform aim2;

	void OnTriggerEnter(Collider col)
	{
		if (col.transform.tag == "PO") {
			haveStilOnTrigger = true;
			aim1.gameObject.SetActive (false);
			aim2.gameObject.SetActive (false);
		}
	}
	void OnTriggerStay(Collider col)
	{

		//print (col.transform.name);
		if (col.transform.tag == "PO") {
			haveStilOnTrigger = true;

		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.transform.tag == "PO") {
			haveStilOnTrigger = false;

			aim1.gameObject.SetActive (true);
			aim2.gameObject.SetActive (true);
		}
	}
}
