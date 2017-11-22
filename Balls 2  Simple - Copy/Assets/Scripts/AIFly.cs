using UnityEngine;
using System.Collections;

public class AIFly : MonoBehaviour {

	public Transform target;
	Rigidbody rb;

	void Start () {
	
	}


	void OnEnable()
	{
		rb = this.GetComponent<AIBehave>().GetComponent<AIBehave> ().rb;
		rb.useGravity = false;

	}
	// Update is called once per frame
	void Update () {
		if (target) {
		}
	}
}
