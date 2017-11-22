using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotifyPlayerTextTrigger : MonoBehaviour {

	public string message;
	public bool oneShot;
	bool switchi;

	public bool multipleMessages;
	public List<string> theMessages = new List<string> ();
	int messageTrack = 0;
	float clockTime;
	float messagesTimeAppart = 8;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" && col.transform.parent.GetComponent<Attributes>()) {
			if (oneShot && !switchi) {
				col.transform.parent.GetComponent<UiCoordination> ().NotifyPlayer (message);
				Destroy (this.gameObject);
				switchi = true;
			}
			if (!oneShot && multipleMessages) {
				StartCoroutine (SendMultipleMessages (col.transform.parent.GetComponent<UiCoordination> (), messagesTimeAppart));
			}
		}
	}
	IEnumerator SendMultipleMessages(UiCoordination ui, float time)
	{
		if (messageTrack < theMessages.Count) {
			ui.NotifyPlayer (theMessages [messageTrack]);
			yield return new WaitForSeconds (messagesTimeAppart);
			messageTrack += 1;
			StartCoroutine (SendMultipleMessages(ui ,messagesTimeAppart));
			yield return null;

		} else {
			Destroy (this.gameObject);
		}
	}
}
