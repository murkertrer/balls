using UnityEngine;
using System.Collections;

public class Behave : MonoBehaviour {

	Attributes at;

	void OnEnable () {
	
		at = GetComponent<Attributes> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
