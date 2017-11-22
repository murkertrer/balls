using UnityEngine;
using System.Collections;

public class LookAtTransform : MonoBehaviour {

	public Transform thingToLook;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		this.transform.LookAt (thingToLook);
	}
}
