using UnityEngine;
using System.Collections;

public class LookAtPlayerCam : MonoBehaviour {

	public Transform playerCam;
	public Transform healthBar;
	public bool foundPlayerCam;
	public GameObject actualHealthBar;

	void OnEnable()
	{
		GameObject go = GameObject.Find("EventManager");
		if (go) {
			playerCam = go.GetComponent<KeepTrack> ().playerCamTransform;
			foundPlayerCam = true;
		}


	}

	void Update()
	{
		if (!foundPlayerCam) {
		}
		GameObject go = GameObject.Find("EventManager");
		if (go) {
			//playerCam = go.GetComponent<KeepTrack> ().playerCamTransform;
			foundPlayerCam = true;
		}

		if (!playerCam) {
		}
	}
	void LateUpdate()
	{
		if (playerCam) {
			//healthBar.
			transform.eulerAngles = playerCam.transform.eulerAngles;
			transform.LookAt (playerCam);
		} else {
		}
	}

	public void EstablishPlayerCam(Transform it)
	{
		playerCam = it;
	}
}
