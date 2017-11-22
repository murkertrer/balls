using UnityEngine;
using System.Collections;

public class PropsHandle : MonoBehaviour {

	Vector3 initialPos;
	public float minY = -10;
	void Start () {
		initialPos = this.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < minY) {
			PlaceAgain ();
		}
	}

	void PlaceAgain()
	{
		this.transform.position = initialPos;
		this.transform.rotation = Quaternion.identity;
		this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		if (this.GetComponent<Rigidbody> ()) {
		
			this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		}
			

	}
}
