using UnityEngine;
using System.Collections;

public class PathfindingBeta : MonoBehaviour {



	public Transform FollowBall;
	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () {
		
		Vector3 direction = RayPath (FollowBall);
		Rigidbody rb;
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (direction * 20);
		GetComponent<LineRenderer> ().SetPosition (1, transform.position - direction);
		GetComponent<LineRenderer> ().SetPosition (0, transform.position);

	}

	public float marginCollision = 3f;
	Vector3 blockDirection,escapeDirection;
	bool blocked = false;
	RaycastHit hit;

	Vector3 RayPath(Transform target) {

		Vector3 position = this.transform.position;
		Vector3 forward = (target.position - position).normalized;

		//Go to target position
		if(!this.blocked){
			//If you encounter an obstacle
			if(Physics.Raycast(position, forward, out hit, this.marginCollision)){
				//If the obstacle is target
				if(hit.transform == target) return forward;
				//If obstacle is within marginCollision, blocked path
				if(hit.distance < this.marginCollision){

					this.blocked = true;
					this.blockDirection = -hit.normal;
					//Perpendicular to normal obstacle
					Vector3 temp = Vector3.Cross(hit.normal,Vector3.up);
					//Choose direction with lower angle, nearest
					if(Vector3.Angle(temp,forward)<90f) this.escapeDirection = temp;
					else this.escapeDirection = -temp;
					return this.escapeDirection;

				}
			}
			return forward;
		}

		//PATH BLOCKED
		//If target is visible
		if(Physics.Linecast(position, target.position, out hit)){
			if(hit.transform == target) {this.blocked = false; return forward;}
		}
		//If you continue blocked
		if(Physics.Raycast(position, this.blockDirection, this.marginCollision)){
			//If direction of escape is blocked
			if(Physics.Raycast(position, this.escapeDirection, this.marginCollision)){
				//Reverse direction of escape
				this.escapeDirection = -this.escapeDirection;

			}
			//Follow escape direction
			return this.escapeDirection;

		}

		//Ridding corner
		if(Physics.Raycast(position, forward, this.marginCollision)) return this.escapeDirection;

		//No longer is blocked
		this.blocked = false;
		return forward;

	}

}
