using UnityEngine;
using System.Collections;

public class ChangeAimsRot : MonoBehaviour {

	Attributes at;
	public Transform t1;
	public Transform t2;
	public Transform t3;
	public bool right;
	public bool left;
	void Start () {
		at = transform.root.GetComponent<Attributes> ();
	}
	
	void LateUpdate () {
		if (right)
		{
			t2.transform.localRotation = at.myArm.transform.localRotation;
			t3.transform.localRotation = at.myArm.transform.localRotation;
		}
		if (left)
		{
			t2.transform.localRotation = at.myArm2.transform.localRotation;
			t3.transform.localRotation = at.myArm2.transform.localRotation;
		}
	}
}
