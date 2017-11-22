using UnityEngine;
using System.Collections;

public class xTest : MonoBehaviour {
	public Transform focusDirector;

	void LateUpdate()
	{
		this.transform.eulerAngles = focusDirector.transform.eulerAngles;

	}
}
