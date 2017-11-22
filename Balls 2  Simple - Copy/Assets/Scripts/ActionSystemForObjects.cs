using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSystemForObjects : MonoBehaviour {

	public List<GameObject> destructionObjects = new List<GameObject>();
	KeepTrack kt;
	Transform playeraz;
	public bool colliderCondition;
	public bool displacementCondition;
	public bool sendMessageWhenCondition;
	public bool destroySelfWhenCondition;
	public float durationOfMessage;

	public float minY = -10;
	public string messageWhenConditionSatisfied;


	void Start () {
		GameObject go = GameObject.Find("EventManager");
		kt = go.GetComponent<KeepTrack> ();
		playeraz = kt.player;
	
	}
	// Update is called once per frame
	void Update () {
		if (displacementCondition) {
			if (this.transform.position.y < minY) {
				ConditionSatisfied ();
			}
		}
	}
	void ConditionSatisfied()
	{
		foreach (GameObject obj in destructionObjects) {
			Destroy (obj);
		}
		if (sendMessageWhenCondition) {	
			SendMessage (messageWhenConditionSatisfied, durationOfMessage);
		}
		if (destroySelfWhenCondition) {
			Destroy (this.gameObject);
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if (colliderCondition) {
			ConditionSatisfied ();
		
		}
	}
	void SendMessage(string it)
	{
		playeraz.GetComponent<UiCoordination> ().NotifyPlayer (it);

	}
}
