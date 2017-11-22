using UnityEngine;
using System.Collections;

public class ForwadReferencer : MonoBehaviour {

	Attributes at;
	Vector3 referenceMinusY;
	// Use this for initialization
	void Start () {
		at = transform.root.GetComponent<Attributes> ();	
	}	
	void FixedUpdate()
	{
		referenceMinusY = at.rotator.localEulerAngles;
		referenceMinusY.x = 0;
		referenceMinusY.z = 0;
	}
	void LateUpdate () {
		//transform.position = at.rotator.transform.position;
	
		transform.localEulerAngles = referenceMinusY;
	}
}
