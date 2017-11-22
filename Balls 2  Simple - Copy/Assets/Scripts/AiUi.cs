using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AiUi : MonoBehaviour {
	public GameObject eventManger;
	public Vector3 lclSale  = new Vector3 (.1f, .1f, .1f);

	void Start () {
		eventManger = GameObject.Find ("EventManager");
	}
	public void HealthActionNotification(float amount, int it)
	{
		GameObject go = new GameObject ("Cool GO by AI");
		go.transform.localScale	= lclSale;
		go.transform.parent = eventManger.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;
		go.transform.position = this.GetComponent<CreatureAttributes> ().myAiBall.transform.position;
		go.AddComponent<CanvasRenderer>();
		go.AddComponent<Text> ();
		if (it == 1) {
			go.GetComponent<Text> ().color = Color.red;
		}
		if (it == 2) {
			go.GetComponent<Text> ().color = Color.green;
		}
		go.GetComponent<Text> ().fontSize = 20;
		go.GetComponent<Text> ().font = eventManger.GetComponent<KeepTrack> ().thingsFont;
		go.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		amount = Mathf.Round (amount);
		go.GetComponent<Text> ().text = amount.ToString();
		go.AddComponent<TextNotificationFloat> ();
		go.AddComponent<TextNotificationFloat> ().doDouble = true;
		go.GetComponent<TextNotificationFloat> ().playerCam = eventManger.GetComponent<KeepTrack> ().playerCamTransform;
		go.GetComponent<TextNotificationFloat> ().DestroyText ();
		go.GetComponent<TextNotificationFloat> ().rotationReference = this.GetComponent<CreatureAttributes> ().myArmor;	;
	}
}
