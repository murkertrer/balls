using UnityEngine;
using System.Collections;

public class InformAboutTrigger : MonoBehaviour {

	public Transform info;
	void Start () {
		info = this.transform.root;
	}
	



	void OnTriggerEnter(Collider other)
	{
		if (info.GetComponent<AIBehave> ()) {
			if (other.transform != info.GetComponent<AIBehave> ().ball) {
				info.GetComponent<AIBehave> ().GotATriggerEnter (other);
				if (other.gameObject.tag == "PO") {

				}
			}
		}



		if (info.GetComponent<AIF> ()) {
			if (other.transform != info.GetComponent<AIF> ()) {
				if (other.transform != info.GetComponent<AIF> ().mySelf) {
					info.GetComponent<AIF> ().GotATriggerEnter (other.transform);
				}
			}
		}



			
	}

	void OnTriggerStay(Collider other)
	{
		if (info.GetComponent<AIBehave> ()) {	
			if (other.transform != info.GetComponent<AIBehave> ().ball) {
				info.GetComponent<AIBehave> ().StillWithinTrigger (other);
				if (other.gameObject.tag == "PO") {
				
				}
			}
		}




		if (info.GetComponent<AIF> ()) {
			if (other.transform != info.GetComponent<AIF> ()) {
				if (other.transform != info.GetComponent<AIF> ().mySelf) {
					info.GetComponent<AIF> ().GotATriggerStay (other.transform);
				}
			}
		}
	}
}
