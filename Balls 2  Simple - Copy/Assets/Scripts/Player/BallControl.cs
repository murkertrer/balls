using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	bool cameraCardinality;
	//Physics
	public float maxAnglVelocity = 15;
	//public float maxRigidBodySpeed =5;
	//Movement
	public float thrust = 10;
	public float hooveringMovementStrenght =30;
	public float jumpStrenght = 5f;
	public bool canDoSingleJump;
	public bool doubleJump = true;
	bool doubleJumpExpended;
	bool canDoDoubleJump;

	KeyCode[] wasds = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space };
	public KeyCode shifter;
	public KeyCode powerKey;
	public KeyCode breaks;
	//Reference
	public Transform myBall;
	public Rigidbody rb;
	public Transform myDirector;
	public Transform myArmor;
	//Control
	public float mouseDragX;
	public float mouseDragY;
	public float rotationSpeed = 5f;
	public bool canHoover;
	public bool touchingGround = false;
	private float distToGround =0f;
	public float groundTolerance =  0.1f;
	Attributes at;
	PlayerHandle ph;
	bool startedSpaceBarOnGround;
	public float minManaEngageInFloat =10;
	public float CostOfThrustMana = 1;
	public bool switchOfSafetey = true;
	public bool pivot;
	public bool particles = true;
	public float initialAngularDarg;
	public bool canFlip;
	public int amountOfParticles=1;
	public float somersaultForce = 250;
	public bool currentlyRotating;
	public float rotationT = .6f;
	public float displacementForce =100;
	public bool lookAtAimPointPivot = true;
	public bool returnToRotation = true;
	public bool returnToAim;
	public bool rotating;
	Vector3 lastLookedPoint;


	void OnEnable()
	{
		at = this.GetComponent<Attributes> ();
		shifter = at.shifter;
		myBall = at.myBall;
		myDirector = at.rotator;
		rb = myBall.GetComponent<Rigidbody> ();
		myArmor = at.myArmor;	
		breaks = KeyCode.F;
		initialAngularDarg = rb.angularDrag;
	}
	void Start()
	{
		distToGround = myBall.GetComponent<Collider> ().bounds.extents.y;
		myBall.GetComponent<Rigidbody> ().maxAngularVelocity = maxAnglVelocity;

		ph = this.GetComponent<PlayerHandle> ();
		powerKey = this.GetComponent<Attributes> ().usePowers;
	}
	void Update()
	{
		if (at.isSelected) {
			if (ph.isSelected && ph.fpsControl) {			
				if (at.kT.currentFPSPlayer == transform.root.gameObject) { 
					MovementWASD ();
				}
			
			
			}
			CheckWasdParticles ();

			if (Input.GetKeyDown (breaks)) {
				rb.angularDrag = initialAngularDarg * 4;
			}
			if (Input.GetKeyUp (breaks)) {
				rb.angularDrag = initialAngularDarg;
			}
			CheckIftouchingGround ();




			if (Input.GetKeyDown (at.shifter)) {
				cameraCardinality  = true;
			}

			if (Input.GetKeyUp (at.shifter)) {
				cameraCardinality  = false;

			}

		}

		/*

		if (at.cc.armorChild) {

		} else {
		
		}
		*/


	}
	public void Colliding()
	{
		touchingGround = true;
	}
	public void NotColliding()
	{
		touchingGround = false;
	}

	void CheckIftouchingGround ()
	{
		//Vector3 r = new Vector3 (myBall.transform.position.x + 1, myBall.transform.position.y, myBall.transform.position.z+1);
		if (Physics.Raycast (myBall.transform.position, -at.rotator.up*-1, distToGround + groundTolerance)) {



			touchingGround = true;
		} else {
			touchingGround = false;
		}
		if (touchingGround && (Input.GetKeyUp (KeyCode.Space))) {
			// = true;
			switchOfSafetey = true;
		}
		if (!touchingGround)
		{
			if (!Input.GetKey (KeyCode.W) &&!Input.GetKey (KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey (KeyCode.D)) {
				switchOfSafetey = true;
			}
		}
	}
	IEnumerator ReturnRotatorToOriginalRotation(float t)
	{
		float elapsedTime = 0;
		while (elapsedTime < t) {
			at.rotator.transform.rotation = Quaternion.Lerp (at.rotator.transform.rotation, at.rotatorDummy.transform.rotation, (elapsedTime / t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	void LateUpdate()
	{
		if (at.kT.currentFPSPlayer == transform.root.gameObject) { 
			if (at.isSelected) {
				myArmor.transform.position = myBall.position;
				if (!lookAtAimPointPivot) {

					
					if (!Input.GetKey (powerKey)) {
						if (!Input.GetKey (at.controller) && Input.GetMouseButton (1)) {
							if (Input.GetKey (shifter) && Input.GetMouseButton (1)) {
								RotatePivot ();

								//if (!at.cc.armorChild && !Input.GetMouseButton(1) )}
							}
						}
						if (!Input.GetKey (shifter) && !Input.GetKey (at.controller)) {
							if (!at.cc.armorChild && !Input.GetMouseButton (1)) {
								RotatePivot ();

							}
						}
					}
				}

				if (lookAtAimPointPivot) {
					if (!Input.GetKey (shifter)) {
						if (at.commandingUnits) {
							if (!Input.GetMouseButton (1)) {
								RotatePivot ();
							}
						
						} else {
							RotatePivot ();
						}
					}
					if (Input.GetKey (shifter) && Input.GetMouseButton (1)) {
						if (!at.comandoMode) {
							//print ("case4");
							RotatePivot ();
						}
					}
				}
				if (at.cc.armorChild) {
					
					if (!lookAtAimPointPivot) {
					}
					if (!Input.GetMouseButton (1)) {
						if (!Input.GetKey (shifter))
							RotatePivot ();


					}
					if (Input.GetMouseButton (1)) {
						//RotatePivotCam ();
						RotatePivot ();
					} 
				} else {
					if (Input.GetMouseButton (1)) {
						if (at.commandingUnits) {
							//print ("case17");
							//RotatePivot ();
						}
					}
				}
				if (Input.GetKeyUp (shifter)) {
					//StartCoroutine (ReturnRotatorToOriginalRotation (.6f));
				}

				if (!Input.GetKey (at.shifter)) {
					Gimballer ();
				}

			}
		} else {
		//Im not in FPS
			if (at.subordination && Input.GetMouseButton (1)) {
				RotatePivot ();
			}

		}

		if (cameraCardinality) {

			//Only when Shifter?

			/*
			at.cameraCardianlity.transform.position = at.myCam.transform.position;
			//at.cameraCardianlity.transform.SetParent (at.myCam.transform);
			Vector3 cardinalityRestrictingX = new Vector3 (0, at.myCam.transform.localRotation.y, at.myCam.transform.localRotation.z);
			//at.cameraCardianlity.transform.eulerAngles = cardinalityRestrictingX;
			at.cameraCardianlity.transform.localRotation = at.myCam.transform.localRotation;
			*/
			
		}

	}
	public void RotatePivotCam()
	{
			float mouseDragX3 = Input.GetAxis ("Mouse X") * rotationSpeed;
			at.myCam.transform.RotateAround (myBall.transform.position, at.myArmor.up, mouseDragX3);
	}
	public void RotatePivot()
	{
		if (GetComponent<Powers> ().midAirRot == false && !at.cc.rotatingPlayerToCamFromAssdLookPlayerToCam) {
			if (!Input.GetKey (powerKey)) {

				DoRot ();
			}
		} else {
		
			//print ("now active");
		}
	}

	Coroutine returningAim;
	public void Gimballer()
	{
		if (at.gimBall && !Input.GetKey(at.commander) && !at.cc.armorChild) {
			/*
			float read = Vector3.Dot (at.myArmor.up, at.myCam.transform.up);

			//Make a better Clamping Methodology

			Vector3 lastKnownCamPos = at.myCam.transform.position;
			Quaternion lastKnownCamRot = at.myCam.transform.rotation;

			if (read > 0) {	}
			*/
				//Rotation
				//at.rotatorDummy.RotateAround (at.myBall.transform.position, at.myArmor.up, at.bC.mouseDragX);
				mouseDragY = Input.GetAxis ("Mouse Y") * at.bC.rotationSpeed * -1;
				//at.rotator.RotateAround (at.myBall.transform.position, at.rotator.right, mouseDragY);
				//at.myCam.transform.RotateAround (at.myBall.transform.position, at.cameraCardianlity.transform.right, mouseDragY);
				at.myCam.transform.RotateAround (at.myBall.transform.position, at.myCam.transform.right, mouseDragY);

				//Indication Of Observation
				at.aimObj.SetActive (true);
				Ray ray = new Ray (at.myCam.transform.position, (at.aimObj.transform.position - at.myCam.transform.position));
				RaycastHit hit;	
				if (Physics.Raycast (ray, out hit, 10000, ~2)) {
					if (hit.transform != at.myBall) {
					
						at.aSSD.LookAtPointInQuestion (hit.point);
					}
				}
				if (Input.GetKeyUp (at.shifter)) {			
					StartCoroutine (at.aSSD.ReturnAimToCenter ());
				}




		
			/*
			Vector3 angleDifference = Vector3.Angle (at.myCam.transform.eulerAngles.x, at.myArmor.eulerAngles);
			print (angleDifference);
			*/




			Aldos ();
		}

	
	}
	void Aldos()
	{
		//Vector3 dir = at.rotator.right;
		//Vector3 toOther = other.position - transform.position; 
	}



	public void RotateArmor()
	{
		float mouseDragX3 = Input.GetAxis ("Mouse X") * rotationSpeed;
		at.myArmor.RotateAround (myBall.transform.position, at.myArmor.up, mouseDragX);
	}
	public void DoRot()
	{		
		at.rotatorDummy.RotateAround (myBall.transform.position, at.myArmor.up, mouseDragX);
		mouseDragX = Input.GetAxis ("Mouse X") * rotationSpeed;
		at.rotator.RotateAround (myBall.transform.position, at.myArmor.up, mouseDragX);
	}
	public void DoRot2()
	{
		mouseDragX = Input.GetAxis ("Mouse X") * rotationSpeed;
		at.anotherAxis.RotateAround (myBall.transform.position, at.myArmor.up, mouseDragX);
	}
	public void RotOtherAxis()
	{
		mouseDragX = Input.GetAxis ("Mouse X") * rotationSpeed;
		at.anotherAxis.RotateAround (myBall.transform.position, at.myArmor.up, mouseDragX);
	}

	void MovementWASD ()
	{
		foreach (KeyCode wasd in wasds) {	
			if (!Input.GetKey (wasd))
				continue;
			MoveBall (wasd);
			if (GetComponent<RtsMovement> () && at.kT.currentFPSPlayer == transform.root.gameObject) { 
				if (GetComponent<RtsMovement> ().haveDestination || GetComponent<RtsMovement> ().haveMultipleDestination) {
					GetComponent<RtsMovement> ().EndAllDestinations ();
				}
			} else {
			
				print (at.kT.currentFPSPlayer);
				print (transform.root);
				print (GetComponent<RtsMovement> ());
				//print ();
				//
			}

            ;
		}
	}
	void CreateEntityForCamCardinalityAndMantainUntilCardianlityisOff()
	{
		/*
		GameObject go = new GameObject;
		go.name = "Entity For CameraCardinality, Dont Bend";
		go.transform.position
		//..........
		*/

	}

	void CheckWasdParticles()
	{
		
		if (Input.GetKeyUp (KeyCode.Space)) {
			at.ThrustUp.Stop ();
		}
		if (Input.GetKeyUp (KeyCode.W)) {
			at.ThrustBack.Stop ();
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			at.ThrustLeft.Stop ();
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			at.ThrustFront.Stop ();
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			at.ThrustRight.Stop ();
		}
	}
	void CheckWasdParticlesInput()
	{
		if (Input.GetKeyDown (KeyCode.W)) {
			at.ThrustBack.Play();
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			at.ThrustLeft.Play ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			at.ThrustFront.Play ();
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			at.ThrustRight.Play ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			at.ThrustUp.Play ();
		}
	}
	IEnumerator TinyDelay()
	{
		yield return new WaitForSeconds (.05f);
		canDoSingleJump = true;

		//canDoDoubleJump = true;
	}
	public void MoveBall(KeyCode wazadia){

		if (touchingGround) {
			if (!cameraCardinality) {

				switch (wazadia) {
				case KeyCode.W:				
					rb.AddTorque (at.forwardReference.right * thrust);
					break;
				case KeyCode.A:
					rb.AddTorque (at.forwardReference.forward * thrust);
					break;
				case KeyCode.S:
					rb.AddTorque (at.forwardReference.right * thrust * -1);
					break;
				case KeyCode.D:
					rb.AddTorque (at.forwardReference.forward * thrust * -1);
					break;
				case KeyCode.Space:
					if (GetComponent<SpaceManipulation> ().planeOrientation) {
						//rb.AddForce(myDirector.up * (jumpStrenght)/2);
					} else {
					}

					if (canDoSingleJump) {
						rb.AddForce (at.myArmor.up * (jumpStrenght));
						canDoSingleJump = false;
						StartCoroutine (TinyDelay ());
					}
					break;
				}
			} else {


					switch (wazadia) {
						case KeyCode.W:				
						rb.AddTorque (at.myCam.transform.right * thrust);
						break;
						case KeyCode.A:
						rb.AddTorque (at.myCam.transform.forward * thrust);
						break;
						case KeyCode.S:
						rb.AddTorque (at.myCam.transform.right * thrust * -1);
						break;
						case KeyCode.D:
						rb.AddTorque (at.myCam.transform.forward * thrust * -1);
						break;
						case KeyCode.Space:

					rb.AddForce (at.myArmor.up * (jumpStrenght));
						break;
					}
						}
		}
		else {
			/*
			if (canFlip) {
			}
			*/

			if (doubleJump) {

				/*
				switch (wazadia) {
				case KeyCode.Space:

					//&& at.pW.midAirRot == false
					if (!doubleJumpExpended&& at.pW.midAirRot == false ) {
						rb.AddForce (myDirector.up * (jumpStrenght/2));
						//doubleJumpExpended = true;
						at.pW.DoubleSomersault ();
					//Double Flip
					}

					break;
				}
				*/
			}
		}
	}
	IEnumerator RotatePlayerSpace(float inTime)
	{		
		currentlyRotating = true;
		rb.AddForce (at.rotator.up * somersaultForce / 4);
		RaycastHit hit;
		float offsetDistance = 0;
		float highestPoint = 0;
		float angle = 72;
		float time = 0;
		time = inTime;
		float thrust2 = 2;
		float elapsedTime = Time.time + (time);
		bool oneShotForceDown = false;
		float passedTime = 0;
		Quaternion localDesiredRot = Quaternion.identity;
		GameObject go = new GameObject ();
		go.transform.parent = at.myCam.transform.parent;
		go.transform.position = at.myCam.transform.position;
		go.transform.rotation = at.myCam.transform.rotation;
		at.myCam.transform.parent = at.myArmor;
		GameObject shieldDummy = new GameObject ();
		Vector3 originalForceDirector = at.rotator.right;
		while (elapsedTime > Time.time) {
			while (elapsedTime > Time.time) {
				passedTime += Time.deltaTime;
				at.rotator.RotateAround (at.myArmor.position, at.rotator.right, angle * (Time.deltaTime / time));
				yield return null;
			}
			if (!at.cc.armorChild) {
				at.myCam.transform.parent = go.transform.parent;
				at.myCam.transform.position = go.transform.position;
				at.myCam.transform.localEulerAngles = Vector3.zero;
			}
			at.rotator.localEulerAngles = new Vector3 (0, at.rotator.localEulerAngles.y, 0);
		}
		currentlyRotating = false;
	}
}
