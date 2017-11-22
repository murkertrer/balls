using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RtsMovement : MonoBehaviour
{
	public Transform tempForTest;

	public bool curvePath = true;
	public float marginCollision = 3f;
	Vector3 blockDirection,escapeDirection;
	bool blocked = false;
	RaycastHit hit;
	//public float arriveTolarenace = 1;
	public float depthCheck = 2;
	public float frontCheck = 1;
	public float threshHoldForJumpAbovePlayer = .5f;
	public Transform checkPositionOfHeight; 
	public GameObject it;

	public bool findingAlternative;
	public bool jumpingToGetOnTop;
	public bool jumpingOverVoid;



    Attributes at;
    Camera camera;
    Vector3 destination;
    public bool haveDestination;
    public  bool haveMultipleDestination;
    Rigidbody rb;
    float thrustox = 0f;
    public float toleranceForWayPoints = 2;
	//KeyCode[] wasds = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space };
    public List<Vector3> waypointsToGo = new List<Vector3>();

    void Start()
    {
        camera = GetComponent<Camera>();
        at = GetComponent<Attributes>();
        thrustox = at.bC.thrust;
        rb = at.myBall.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (haveDestination)
        {
           //HeadToDestination();
        }
        if (haveMultipleDestination)
        {
            WayPoints();
        }
    }
    void HeadToDestination()
    {
        Vector3 direction = (  destination- rb.transform.position).normalized;
        Debug.DrawRay(at.myBall.position, direction * 5, Color.blue);
        at.movementRenderer.SetPosition(0, rb.transform.position);
        at.movementRenderer.SetPosition(1, destination);

		//**
		//If you encounter an obstacle
		float distanceToPoint = Vector3.Distance(rb.transform.position, destination);
		//-margin perhaps
		//***pATHFINDING hERE
		/*
		if (Physics.Raycast (rb.transform.position, destination,out hit, distanceToPoint)) {
			//If the obstacle is target
			//if(hit.transform == target) return forward;
			//If obstacle is within marginCollision, blocked path
			if (hit.distance < marginCollision) {
				//this.blocked = true;
				//this.blockDirection = -hit.normal;
				//Perpendicular to normal obstacle

			}
			print (" blocked");


			Vector3 temp = Vector3.Cross(hit.normal,Vector3.up);
			Debug.DrawRay (rb.position, temp, Color.cyan);
			//Choose direction with lower angle, nearest
			//if(Vector3.Angle(temp,at.rotator.forward)<90f) this.escapeDirection = temp;
			//else this.escapeDirection = -temp;
			//return this.escapeDirection;

			//at.movementRenderer.SetPosition(1, temp);

			at.movementRenderer.SetColors (Color.red, Color.yellow);
		} else {
			at.movementRenderer.SetColors (Color.green, Color.blue);
			print ("not   blocked");
		}

*/



        direction = Quaternion.Euler(0, 90,0) * direction;
        Debug.DrawRay(at.myBall.position, direction * 5, Color.yellow);
        rb.AddTorque(direction * thrustox);
        float stillToGo = Vector3.Distance(rb.transform.position, destination);
        if (stillToGo <toleranceForWayPoints)
        {
            EndDestination();
        }

    }
    void WayPoints()
    {
        if (waypointsToGo.Count > 0)
        {
			if (curvePath) {

				Vector3 direction = (waypointsToGo [0]);
				direction = RayPath (direction);

				Debug.DrawRay(at.myBall.transform.position + (at.myArmor.up * threshHoldForJumpAbovePlayer), at.myArmor.up*5, Color.red);

				Vector3 forward = (waypointsToGo [0] - at.myBall.transform.position).normalized;
				if (direction == forward) {

					Vector3 targetDir = waypointsToGo [0] - at.myBall.transform.position;

					//Go Forward, in the adjacent of the angle;
					//Rotate Forward, not in direction of point
					//Perhaps Find Angle"
					//float angle = Vector3.Angle( targetDir, at.forwardReference.forward );
					//rb.velocity.normalized


					//Compensate For Drift in the playerMovement

					float test = Vector3.Angle (rb.velocity.normalized, forward.normalized);

					Vector3 thetaForce = Quaternion.AngleAxis (test, at.forwardReference.up) * rb.velocity.normalized;
					Debug.DrawRay (rb.transform.position, thetaForce * 5);

			

					//Vector3 temp = Vector3.Cross(hit.normal,at.myArmor.up);
					//Choose direction with lower angle, nearest
					//if(Vector3.Angle(temp,forward)<90f) this.escapeDirection = temp;

					Quaternion addedRotation = Quaternion.AngleAxis (90, at.rotator.up);
					direction =   addedRotation*direction;
					//direction = Quaternion.Euler (0, 90, 0) * direction;
					rb.AddTorque (direction * at.bC.thrust/2);


					//AddThetaCompensaions
					thetaForce = addedRotation * thetaForce;
					rb.AddTorque (thetaForce * at.bC.thrust/2);
					//rb.AddTorque (thetaForce * Mathf.Sin(test) *  at.bC.thrust/2);


				} else {

					print ("flag");
					JumpIfObstacleIsNotSoHigh ();
					//Do Complex PathFinding here Instead of just Direction
					direction = Quaternion.Euler (0, 90, 0) * direction;
					rb.AddTorque (direction * at.bC.thrust);
					CheckForVoidBellow ();
				}
				CheckForVoidBellow ();

			} else {
				Vector3 direction = (waypointsToGo [0] - rb.transform.position).normalized;
				direction = Quaternion.Euler (0, 90, 0) * direction;
				rb.AddTorque (direction * at.bC.thrust);
			}
            float stillToGo = Vector3.Distance(rb.transform.position, waypointsToGo[0]);
            if (stillToGo < toleranceForWayPoints)
            {
                waypointsToGo.RemoveAt(0);
                if (waypointsToGo.Count ==0)
                {
                    haveMultipleDestination = false;
                }
            }            
            at.movementRenderer.SetVertexCount(waypointsToGo.Count+1);
            at.movementRenderer.SetPosition(0, at.myBall.position);
            for (int i = 0; i < waypointsToGo.Count; i++)
            {  
                at.movementRenderer.SetPosition(i+1, waypointsToGo[i]);                         
            }             
        }
    }
	void CheckForVoidBellow()
	{
		Vector3 inFront = at.myBall.transform.position + rb.velocity.normalized;
		if (Physics.Raycast (inFront, at.myArmor.up * -1, out hit, depthCheck,~2)) {
			
			jumpingOverVoid = false;
			
		} else {
			print ("jump1");
			at.bC.MoveBall(KeyCode.Space);
			jumpingOverVoid = true;
		}
	}
	void JumpIfObstacleIsNotSoHigh()
	{
		if (Physics.Raycast (at.myBall.transform.position, rb.velocity.normalized, out hit, marginCollision,~2)) {

			//If Object is in front
			Vector3 tPoint = at.myBall.transform.position + (at.myArmor.up * threshHoldForJumpAbovePlayer);

			 
			if (Physics.Raycast (tPoint, rb.velocity.normalized, out hit, marginCollision,~2)) {
				//If Object is Too High
			} else {
				//If Object is NOT Too High

				print ("jump2");
				at.bC.MoveBall (KeyCode.Space);
			}
		} else {		
			//print ("nothing in front");
		}
	}

	Vector3 RayPath(Vector3 target) {

		Vector3 position = at.myBall.transform.position;
		Vector3 forward = (target - at.myBall.transform.position).normalized;

		//Vector3 movementDirection = at.myBall.GetComponent<Rigidbody> ().velocity.normalized;

		//Go to target position
		if(!this.blocked){
			//If you encounter an obstacle
			if(Physics.Raycast(position, forward, out hit, marginCollision,~2)){
				//If the obstacle is target

				if (Vector3.Distance(hit.point, target) < toleranceForWayPoints) return forward;

				//If obstacle is within marginCollision, blocked path
				if(hit.distance < marginCollision){

					blocked = true;
					blockDirection = -hit.normal;
					//Perpendicular to normal obstacle
					Vector3 temp = Vector3.Cross(hit.normal,at.myArmor.up);
					//Choose direction with lower angle, nearest

					if(Vector3.Angle(temp,forward)<90f) this.escapeDirection = temp;
					else escapeDirection = -temp;
					return escapeDirection;
				}
			}
			return forward;
		}

		//PATH BLOCKED
		//If target is visible
		if(Physics.Linecast(position, target, out hit)){
			if (Vector3.Distance(hit.point, target) < toleranceForWayPoints)
			{
				blocked = false; return at.rotator.forward;
			}
		}
		//If you continue blocked
		if(Physics.Raycast(position, blockDirection, marginCollision,~2 )){
			//If direction of escape is blocked
			if(Physics.Raycast(position, escapeDirection, marginCollision,~2)){
				//Reverse direction of escape
				escapeDirection = -escapeDirection;
			}
			//Follow escape direction
			return escapeDirection;

		}

		//Ridding corner
		if(Physics.Raycast(position, forward, marginCollision,~2)) return this.escapeDirection;
		//No longer is blocked
		blocked = false;
		return forward;

	}

    public void SetMultipleDestinations(Vector3 it)
    {
        if (haveDestination)
        {
            waypointsToGo.Add(destination);
            waypointsToGo.Add(it);
        }
        else
        {
            waypointsToGo.Add(it);
        }

        haveDestination = false;
        haveMultipleDestination = true;
        at.movementRenderer.enabled = true;       
    }
    void EndDestination()
    {
        haveDestination = false;
        at.movementRenderer.enabled = false;
    }
    public void EndAllDestinations()
    {	
        haveDestination = false;
        haveMultipleDestination = false;
        destination = Vector3.zero;
        waypointsToGo.Clear();
		at.movementRenderer.SetVertexCount(0);	
        at.movementRenderer.enabled = false;
    }
    public void SetUniqueDestination(Vector3 it)
    {
        waypointsToGo.Clear();
        haveMultipleDestination = false;
        at.movementRenderer.SetVertexCount(2);
        destination = it;
        haveDestination = true;
        at.movementRenderer.enabled = true;
    }
    void LateUpdate()
    {
        at.myArmor.transform.position = at.myBall.position;
    }
	public void ChangeWayPointTolerance(int numberOfAgents)
	{
		toleranceForWayPoints = numberOfAgents + 2;
	}
}
