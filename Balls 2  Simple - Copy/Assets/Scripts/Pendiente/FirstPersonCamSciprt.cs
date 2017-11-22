using UnityEngine;
using System.Collections;

public class FirstPersonCamSciprt : MonoBehaviour {

	public Transform armToCopy;
	
	// Update is called once per frame
	void LateUpdate()
	{
		this.transform.localEulerAngles = armToCopy.transform.localEulerAngles;
	}
}
