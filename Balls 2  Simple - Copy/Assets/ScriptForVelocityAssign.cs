using UnityEngine;
using System.Collections;

public class ScriptForVelocityAssign : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void GetVelocity(Vector3 it)
	{
		GetComponent<Rigidbody> ().velocity = it;
		print ("xxx");
	}
}
