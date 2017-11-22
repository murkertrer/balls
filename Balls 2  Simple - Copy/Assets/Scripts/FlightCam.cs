using UnityEngine;
using System.Collections;

public class FlightCam : MonoBehaviour {
	public Transform target;
	Attributes at;
	Rigidbody rb;
	Vector3 targetPos;
	Transform cam;
	public float distance;
	public float height;
	public bool LookAt;


	void Start()
	{
		at = this.GetComponent<Attributes> ();
		target = at.myBall;
		rb = target.GetComponent<Rigidbody> ();
		cam = at.myCam.transform;

		distance = Vector3.Distance (cam.transform.position, target.transform.position);
		height = cam.transform.position.y - target.transform.position.y;
	}


	void LateUpdate()
	{
		// Get the inverse of the players velocity
		Vector3 direction = -(rb.velocity.normalized);
		//  Set the position of the camera relative to the player, with some distance and height
		targetPos = target.transform.position + (direction * distance) + (Vector3.up * height);
		// Set camera position                
		cam.transform.position = targetPos;
		// Let the camera look at the player    
		if (LookAt) {
			cam.transform.LookAt (target.transform);
		}
		//Quaternion it = Quaternion.LookRotation (rb.velocity.normalized);
		//cam.transform.rotation = it;

		cam.transform.rotation = at.rotator.transform.rotation;

		//cam.transform.LookAt (-(rb.velocity.normalized*100));
		//GameObject go = Instantiate(GameObject.CreatePrimitive.

	}
}
