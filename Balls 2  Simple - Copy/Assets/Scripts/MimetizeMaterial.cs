using UnityEngine;
using System.Collections;

public class MimetizeMaterial : MonoBehaviour {

	// Use this for initialization
	public Transform objectToCopy;
	public bool rotate;
	public bool orientationEulers;
	public bool material;
	Attributes at;
	void OnEnable()
	{
		at = transform.root.GetComponent<Attributes> ();
		objectToCopy = at.myBall;
		GetComponent<MeshRenderer> ().material = objectToCopy.GetComponent<MeshRenderer> ().material;

	}

	void LateUpdate()
	{
		if (rotate) {
			this.transform.rotation = objectToCopy.transform.rotation;
		}
		if (orientationEulers) {
			this.transform.eulerAngles = objectToCopy.transform.eulerAngles;

		}
	}
}
