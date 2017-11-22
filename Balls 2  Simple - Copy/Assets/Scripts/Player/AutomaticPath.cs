using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AutomaticPath : MonoBehaviour {

	// Use this for initialization
	Transform myBall;
	Rigidbody rb;
	bool cancelingElement;
	public List<Vector3> pointsList = new List<Vector3> ();
	public LineRenderer movementPathLineRenderer;
	public float ArrivalTolerance = 1;
	bool haveDestiny;
	public float thrust;
	public KeyCode shifter;
	public Vector3 wayPointHeight = new Vector3 (0, .5f, 0);


	void OnEnable()
	{
		shifter = this.GetComponent<Attributes> ().shifter; 
		myBall = this.GetComponent<Attributes> ().myBall; 
		rb = myBall.GetComponent<Rigidbody> ();
		thrust = this.GetComponent<BallControl> ().thrust;
		movementPathLineRenderer = rb.GetComponent<LineRenderer> ();
	}


	void Update()
	{
		if (Input.GetMouseButtonDown (1)) {
			RaycastHit hitInfo = new RaycastHit ();
			//Debug.DrawRay (this.GetComponent<Attributes> ().myCam.transform.position, hitInfo.point);
			Physics.Raycast (this.GetComponent<Attributes> ().myCam.ScreenPointToRay (Input.mousePosition), out hitInfo);

			if (Input.GetKey (shifter)) {
				if (Input.GetKey (shifter)) {
					RTSPath (hitInfo.point + wayPointHeight);
					EnableLineRenderer ();
				}
			} else {
				RTSPathOne (hitInfo.point + wayPointHeight);
				EnableLineRenderer ();
			}
		}

		if (haveDestiny) {
			Path ();
		}
	}
	public void EnableLineRenderer ()
	{
		movementPathLineRenderer.enabled = true;
	}
	public void RTSPath(Vector3 point){
		if (haveDestiny == true){
			pointsList.Add (point);
			movementPathLineRenderer.SetVertexCount (pointsList.Count);
			movementPathLineRenderer.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);
		}
		if (haveDestiny == false) {
			pointsList.RemoveRange (0, pointsList.Count);
			pointsList.Add (rb.transform.position);
			pointsList.Add (point);
			movementPathLineRenderer.SetPosition(0, rb.transform.position);
			movementPathLineRenderer.SetPosition (1, point);
			movementPathLineRenderer.enabled = true;
			haveDestiny = true;
		}
	}
	public void RTSPathOne (Vector3 InmediatePoint){
		pointsList.RemoveRange (0, pointsList.Count);
		movementPathLineRenderer.SetVertexCount (2);	
		movementPathLineRenderer.SetPosition(0, rb.transform.position);
		movementPathLineRenderer.SetPosition (1, InmediatePoint);
		pointsList.Add (rb.transform.position);
		pointsList.Add (InmediatePoint);

		if (movementPathLineRenderer.enabled == false) {
			movementPathLineRenderer.enabled = true;
		}

		haveDestiny = true;	
		print ("rts path one");
		cancelingElement = false;



	}
	public void CancelRTSPath()
	{
		haveDestiny = false;		
		movementPathLineRenderer.enabled = true;

	
		pointsList.RemoveRange (0, pointsList.Count);
		movementPathLineRenderer.SetVertexCount (2);	
		movementPathLineRenderer.SetPosition(0, rb.transform.position);
		movementPathLineRenderer.SetPosition (1, Vector3.zero);
		pointsList.Add (rb.transform.position);
		pointsList.Add (Vector3.zero);

	}
	void Path()
	{
	
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D) || Input.GetKeyDown (KeyCode.Space)) {
		cancelingElement = true;
			CancelRTSPath ();
		} else {
		cancelingElement = false;
		}

		if (cancelingElement == false) {

			if (!cancelingElement) {
				Vector3 dir = new Vector3 (0, 0, 0);
				dir = pointsList [1] + new Vector3 (0, .5f, 0) - rb.transform.transform.position;
				dir = dir.normalized;
				PathForce (dir);
			}
		}
			
		
		if (cancelingElement == true) 
		{
			movementPathLineRenderer.enabled = false;
			haveDestiny = false;
		}



		float distance = Vector3.Distance (myBall.transform.position, pointsList [1] + new Vector3 (0, .5f, 0));
		if (distance < ArrivalTolerance) {
			if (pointsList.Count == 2) {
				haveDestiny = false;
				movementPathLineRenderer.enabled = false;
			}
			if (pointsList.Count > 1) {
				for (int i = 0; i < pointsList.Count - 1; i++) {
					movementPathLineRenderer.SetPosition (i, (Vector3)pointsList [i + 1]);
				}
				pointsList.RemoveAt (0);
			}
		}
		movementPathLineRenderer.SetPosition (0, myBall.transform.position);
	}
	void PathForce(Vector3 direction)
	{
		//if (myBall.GetComponent<Rigidbody> ().velocity.magnitude > maxRigidBodySpeedPathForce) {}

		//rb.AddForce (direction * thrust);

		//if (rb.velocity.magnitude < maxRigidBodySpeedPathForce)
		//{}
			//rb.AddTorque (direction * thrust);
		direction = Quaternion.AngleAxis (90, Vector3.up) * direction;
			
		rb.AddTorque(direction * thrust);
		//rb.AddRelativeForce(direction*thrust);
		

	}

}
