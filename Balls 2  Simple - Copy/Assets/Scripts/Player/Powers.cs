using UnityEngine;
using System.Collections;

public class Powers : MonoBehaviour {
	public bool cancelRotationOnPLanes = true;

	public bool addExtraPunchToSpin = true;
	public float extraSpinPunhcAmount = 13;
	public float spintAtPercentage = .7f;

	public Vector3 shieldDisplace= new Vector3(-1.5f,0,0);
	public Vector3 shieldDisplaceWorld= new Vector3(-1.5f,0,4);
	public float shield1CD;
	public float shield2CD;
	public bool shieldOnePlaced;


	public AudioClip dash;
	public AudioClip rocket;
	public Transform myArm;
	public GameObject it;
	public GameObject it2;

	public Attributes at;
	Rigidbody rb;
	Transform director;
	public float forceOfMove = 80;
	public float durationOfMove = 3;
	bool trick;
	float endTime;
	float dashCoolDown = 5;
	bool ability1;
	bool ability2;
	public float dashManaCost = 50;
	public float somersaultForce = 100;
	public bool rotating;
	public bool finishedRotation;
	public float sidePushForceWhenJump =50;
	bool allreadyDidFirstJump;
	public bool doubleJump = true;
	public bool allreadyDidSecond =true;
	public bool allreadyDidthird;
	public bool didSpaceRoot;
	public float jumpRotTime =.6f;
	public bool midAirRot;
	BallControl bc;
	public KeyCode powerKey;
    bool rotateCamWhileJumping;
    float mouseDragX3;


	float currentXrotation;

    GameObject tempObj;


    public float flipSpeed = 400;



    void Start () {
		bc = this.GetComponent<BallControl> ();
		at = this.GetComponent<Attributes> ();
		myArm = at.myArm;

		rb = at.myBall.GetComponent<Rigidbody>();
		director = at.rotator;
		powerKey = this.GetComponent<Attributes> ().powers;

		//**

	/*
		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
		*/
	}
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!midAirRot) {}
			if (!rotating) {

				if (cancelRotationOnPLanes && at.sM.planeOrientation) {
				} else {

					Somersault ();
				}
			}

		}

		if (at.isSelected) {
			if (Input.GetKeyDown (powerKey)) {
				//PlaceShield ();
				trick = true;
			}
			if (trick) {
				if (it == null) {
					if (Input.GetKeyDown (KeyCode.Alpha1)) {
						if (ability1) {
							Destroy (it);
							ability1 = false;
							return;
						}
						if (!ability1) {
							ability1 = true;
							//PlaceShield ();
							return;
						}
					}
				}
				if (it2 == null) {
					if (Input.GetKeyDown (KeyCode.Alpha2)) {
						ability2 = true;
					}
				}
				//Dash ();
			}
		}
	}

	Coroutine currentRotation;

	void Somersault()
	{
		print ("somer");
		if (at.iAmFPSPlayer) {
			
			if (at.pH.isInFirstPerson) {
				/*
			if (Input.GetKey (KeyCode.W)) {
				at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.forward * sidePushForceWhenJump);
				return;
			}
			if (Input.GetKey (KeyCode.A)) {
				at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.right * sidePushForceWhenJump*-1);
				return;
			}
			if (Input.GetKey (KeyCode.S)) {
				at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.forward * sidePushForceWhenJump*-1);
				return;
			}
			if (Input.GetKey (KeyCode.D)) {
				at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.right * sidePushForceWhenJump);	
				return;
			}
			StartCoroutine (RotatePlayer (1, at.rotator.right, "Space"));
			*/


				if (Input.GetKey (KeyCode.W)) {
					//****
					currentRotation = StartCoroutine (RotatePlayer (1, at.rotator.right, "W"));
					rotating = true;
					trick = false;
					return;
				}
				if (Input.GetKey (KeyCode.A)) {
					StartCoroutine (RotatePlayer (1, at.rotator.forward, "A"));
					rotating = true;
					trick = false;
					return;
				}
				if (Input.GetKey (KeyCode.S)) {
					StartCoroutine (RotatePlayer (1, at.rotator.right, "S"));
					rotating = true;
					trick = false;
					return;
				}
				if (Input.GetKey (KeyCode.D)) {
					StartCoroutine (RotatePlayer (1, at.rotator.forward, "D"));
					rotating = true;
					trick = false;
					return;
				}
				StartCoroutine (RotatePlayer (1, at.rotator.right, "Space"));


			} else {

				if (at.myBall.GetComponent<Rigidbody> ().angularVelocity.magnitude > ((at.myBall.GetComponent<Rigidbody> ().maxAngularVelocity / 2) * spintAtPercentage)) {

					if (Input.GetKey (KeyCode.W)) {
						currentRotation = StartCoroutine (RotatePlayer (1, at.forwardReference.right, "W"));
						rotating = true;
						trick = false;
						return;
					}
					if (Input.GetKey (KeyCode.A)) {
						StartCoroutine (RotatePlayer (1, at.rotator.forward, "A"));
						rotating = true;
						trick = false;
						return;
					}
					if (Input.GetKey (KeyCode.S)) {
						StartCoroutine (RotatePlayer (1, at.rotator.right, "S"));
						rotating = true;
						trick = false;
						return;
					}
					if (Input.GetKey (KeyCode.D)) {
						StartCoroutine (RotatePlayer (1, at.rotator.forward, "D"));
						rotating = true;
						trick = false;
						return;
					}
					StartCoroutine (RotatePlayer (1, at.rotator.right, "Space"));
				} else {				
					//DoStuff For Not So Fast;

					if (Input.GetKey (KeyCode.W)) {
						at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.forward * sidePushForceWhenJump/2);
						return;
					}
					if (Input.GetKey (KeyCode.A)) {
						at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.right * sidePushForceWhenJump*-1/2);

						return;
					}
					if (Input.GetKey (KeyCode.S)) {
						
						at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.forward * sidePushForceWhenJump*-1/2);
		
						return;
					}
					if (Input.GetKey (KeyCode.D)) {
						at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.right * sidePushForceWhenJump/2);

						return;
					}
					//StartCoroutine (RotatePlayer (1, at.rotator.right, "Space"));

				}
			}
		}


	}
	public void DoubleSomersault()
	{
		/*
		if (at.pH.isInFirstPerson) {

			if (Input.GetKey (KeyCode.W)) {
				at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.forward * sidePushForceWhenJump);
				return;
			}
			if (Input.GetKey (KeyCode.A)) {
				at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.right * sidePushForceWhenJump * -1);
				return;
			}
			if (Input.GetKey (KeyCode.S)) {
				at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.forward * sidePushForceWhenJump * -1);
				return;
			}
			if (Input.GetKey (KeyCode.D)) {
				at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.right * sidePushForceWhenJump);	
				return;
			}
			StartCoroutine (RotatePlayer (1, at.rotator.right, "Space"));
		} else {
			if (!midAirRot) {}

				if (Input.GetKey (KeyCode.W)) {
					//if (currentRotation) {}
				
					StopCoroutine (currentRotation);
					StartCoroutine (SecondRotation ());
				
					
				StartCoroutine (RotatePlayer (1, at.rotator.right, "W"));
				rotating = true;
				trick = false;
				return;
			
				}
				if (Input.GetKey (KeyCode.A)) {
					StartCoroutine (RotatePlayer (1, at.rotator.forward, "A"));
					rotating = true;
					trick = false;
					return;
				}
				if (Input.GetKey (KeyCode.S)) {
					StartCoroutine (RotatePlayer (1, at.rotator.right, "S"));
					rotating = true;
					trick = false;
					return;
				}
				if (Input.GetKey (KeyCode.D)) {
					StartCoroutine (RotatePlayer (1, at.rotator.forward, "D"));
					rotating = true;
					trick = false;
					return;
				}
				StartCoroutine (RotatePlayer (1, at.rotator.right, "Space"));
			
		}
		*/
	}
    void LateUpdate()
    {
        if (rotateCamWhileJumping)
        {
            at.myCam.transform.RotateAround(at.myBall.transform.position, at.myArmor.up, mouseDragX3);
        }
    }

	public void GetRotationIndicationFromRTS()
	{
		print ("ASDASDASDASDS");
		StartCoroutine (RotatePlayer (1, at.rotator.right, "W"));
	}

    bool midAirRot22 = false;
    IEnumerator RotatePlayer(float inTime, Vector3 axis, string key)
    {


		float rotationSpeed = at.bC.rotationSpeed;

		midAirRot = true;
		if (!allreadyDidSecond) {
			allreadyDidSecond = true;
		}


		Vector3 intialPosCam = at.myCam.transform.localPosition;
		Transform dad = at.myCam.transform.transform.parent;
		float angle = 360;
		float time = 0;
		time = jumpRotTime;
		float elapsedTime = Time.time + (time);
		Quaternion localCamRot = at.myCam.transform.localRotation;
		Quaternion localDesiredRot = Quaternion.identity;
		GameObject go = new GameObject();
		go.transform.position = at.myCam.transform.position;
		go.transform.rotation = at.myCam.transform.rotation;
		go.transform.parent = at.myCam.transform.parent;
		go.transform.localPosition = at.myCam.transform.localPosition;
		at.myCam.transform.SetParent(at.myArmor);

		if (key == "W" ) {
			rotating = true;
			GameObject temporaryObjectForRotationOnYAxis = new GameObject();
			temporaryObjectForRotationOnYAxis.name = "temp";
			temporaryObjectForRotationOnYAxis.transform.position = at.myArmor.transform.position;
			temporaryObjectForRotationOnYAxis.transform.rotation = at.myArmor.transform.rotation;
			temporaryObjectForRotationOnYAxis.transform.SetParent(at.myArmor);
			GameObject go2 = new GameObject ();
			if (at.rotateCamWithPlayer) {
				at.myCam.transform.SetParent (at.rotator);
				go2.transform.position = at.rotator.position;
				go2.transform.rotation = at.rotator.rotation;
				go2.transform.SetParent (at.rotator.parent);
				temporaryObjectForRotationOnYAxis.transform.SetParent (go2.transform);
			} else {	
				//at.rotator.SetParent (temporaryObjectForRotationOnYAxis.transform);
				rotateCamWhileJumping = true;
			}				
			at.rotator.SetParent(temporaryObjectForRotationOnYAxis.transform);



			at.myBall.GetComponent<Rigidbody>().AddForce(at.forwardReference.forward * sidePushForceWhenJump);
			float curAngleX = at.rotator.localEulerAngles.x;

			Quaternion start = at.rotator.transform.localRotation;

			while (Mathf.Abs(curAngleX - angle) > 0.0001f)
			{
				print (sidePushForceWhenJump / 30);
				if (addExtraPunchToSpin) {
					at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.up * extraSpinPunhcAmount);
				}

				ExecuteWhileRotating ();
				mouseDragX3 = Input.GetAxis ("Mouse X") * rotationSpeed;
				if (at.rotateCamWithPlayer) {
					go2.transform.position = at.rotator.position;
					go2.transform.localRotation = Quaternion.AngleAxis (go2.transform.localEulerAngles.y + mouseDragX3, at.myArmor.transform.up);
				} else {
					temporaryObjectForRotationOnYAxis.transform.localRotation = Quaternion.AngleAxis (temporaryObjectForRotationOnYAxis.transform.localEulerAngles.y + mouseDragX3, at.myArmor.up);
				}
				curAngleX = Mathf.MoveTowards (curAngleX, angle, Time.deltaTime * flipSpeed);
				at.rotator.transform.localRotation = Quaternion.AngleAxis (curAngleX, axis) * start;
				print (at.rotator.transform.localRotation );

				//currentXrotation = curAngleX;

				yield return null;
			}
			Destroy (go2);
			at.rotator.SetParent(at.myArmor);
			Destroy(temporaryObjectForRotationOnYAxis);
			midAirRot = false;
			rotating = false;
		}
		if (key == "S" ) {
			rotating = true;
			GameObject temporaryObjectForRotationOnYAxis = new GameObject();
			temporaryObjectForRotationOnYAxis.transform.position = at.myArmor.transform.position;
			temporaryObjectForRotationOnYAxis.transform.rotation = at.myArmor.transform.rotation;
			temporaryObjectForRotationOnYAxis.transform.SetParent(at.myArmor);

			if (at.rotateCamWithPlayer) {
				at.myCam.transform.SetParent (at.rotator);
			} else {	
				at.rotator.SetParent (temporaryObjectForRotationOnYAxis.transform);
			}

			at.rotator.SetParent(temporaryObjectForRotationOnYAxis.transform);
			at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.forward * sidePushForceWhenJump*-1);
			float curAngleX = at.rotator.localEulerAngles.x;
			Quaternion start = at.rotator.transform.localRotation;
			while (Mathf.Abs(curAngleX - angle) < 719.9f)
			{

				if (addExtraPunchToSpin) {
					at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.up * extraSpinPunhcAmount);
				}



				ExecuteWhileRotating ();
				rotateCamWhileJumping = true;
				curAngleX = Mathf.MoveTowards(curAngleX, angle*-1, Time.deltaTime * flipSpeed);
				at.rotator.transform.localRotation= Quaternion.AngleAxis(curAngleX, axis) * start;
				//float rotationSpeed = at.bC.rotationSpeed;
				mouseDragX3 = Input.GetAxis("Mouse X") * rotationSpeed;
				temporaryObjectForRotationOnYAxis.transform.localRotation = Quaternion.AngleAxis(temporaryObjectForRotationOnYAxis.transform.localEulerAngles.y + mouseDragX3 , at.myArmor.up);
				yield return null;
			}

			at.rotator.SetParent(at.myArmor);
			Destroy(temporaryObjectForRotationOnYAxis);
			midAirRot = false;
			rotating = false;
		}
		//How To Make The player RotateAroundWhile undergoing the other rotation¡? For D and A
		//**************************************
		//**************************************
		//An inperfection O la la

		//TE quiero Mucho pablo!!!!

		/*
        if (key == "D")
        {
			at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.right * sidePushForceWhenJump);
			GameObject temporaryObjectForRotationOnYAxis = new GameObject();
			temporaryObjectForRotationOnYAxis.transform.position = at.myArmor.transform.position;
			temporaryObjectForRotationOnYAxis.transform.rotation = at.myArmor.transform.rotation;
			temporaryObjectForRotationOnYAxis.transform.SetParent(at.myArmor);
			GameObject go2 = new GameObject ();
			if (at.rotateCamWithPlayer) {
				//at.myCam.transform.SetParent (temporaryObjectForRotationOnYAxis.transform);
				at.myCam.transform.SetParent (at.rotator);
				go2.transform.position = at.rotator.position;
				go2.transform.rotation = at.rotator.rotation;
				go2.transform.SetParent (at.rotator.parent);
				temporaryObjectForRotationOnYAxis.transform.SetParent (go2.transform);

			} else {	
				at.rotator.SetParent (temporaryObjectForRotationOnYAxis.transform);
				at.myCam.transform.SetParent (temporaryObjectForRotationOnYAxis.transform);
				//rotateCamWhileJumping = true;
			}
			at.rotator.SetParent(temporaryObjectForRotationOnYAxis.transform);            
			float curAngleZ = at.rotator.localEulerAngles.z;            
            Quaternion start = at.rotator.transform.localRotation;

			while (Mathf.Abs(curAngleZ - angle) <= 720)
            {   
				ExecuteWhileRotating ();
				mouseDragX3 = Input.GetAxis ("Mouse X") * rotationSpeed;
				if (at.rotateCamWithPlayer) {
					if (!Input.GetKey (at.shifter)) {
						go2.transform.position = at.rotator.position;
						go2.transform.localRotation = Quaternion.AngleAxis (go2.transform.localEulerAngles.y + mouseDragX3, at.myArmor.transform.up);
					}

				} else {
					//We need to Upgrade Terminology. Tranfer From at.cc to at.
					if (at.cc.armorChild) {

						if (at.rotateCamWhenJumpingEvenIfArmorChild) {
							temporaryObjectForRotationOnYAxis.transform.localRotation = Quaternion.AngleAxis (temporaryObjectForRotationOnYAxis.transform.localEulerAngles.y + mouseDragX3, at.myArmor.up);

						} else {
						}				
					}

					else {
						temporaryObjectForRotationOnYAxis.transform.localRotation = Quaternion.AngleAxis (temporaryObjectForRotationOnYAxis.transform.localEulerAngles.y + mouseDragX3, at.myArmor.up);

					}
				}
				curAngleZ = Mathf.MoveTowards(curAngleZ, angle, Time.deltaTime * flipSpeed*-1);
				at.rotator.transform.localRotation = Quaternion.AngleAxis (curAngleZ, axis) * start;	
				yield return null;
            }				
			at.rotator.SetParent(at.myArmor);

			if (at.cc.armorChild) {
				at.myCam.transform.SetParent (at.myArmor);
				Destroy (go2);
				Destroy (temporaryObjectForRotationOnYAxis);

			} else {
				
				StartCoroutine(DestroyTempObjAfterTime(go2, temporaryObjectForRotationOnYAxis));
			}
			midAirRot = false;
			rotating = false;
        }
		*/


		if (key == "D")
		{

			at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.right * sidePushForceWhenJump);
			GameObject temporaryObjectForRotationOnYAxis = new GameObject();
			temporaryObjectForRotationOnYAxis.transform.position = at.myArmor.transform.position;
			temporaryObjectForRotationOnYAxis.transform.rotation = at.myArmor.transform.rotation;
			temporaryObjectForRotationOnYAxis.transform.SetParent(at.myArmor);
			GameObject go2 = new GameObject ();
			if (at.rotateCamWithPlayer) {
				at.myCam.transform.SetParent (at.rotator);
				go2.transform.position = at.rotator.position;
				go2.transform.rotation = at.rotator.rotation;
				go2.transform.SetParent (at.rotator.parent);
				temporaryObjectForRotationOnYAxis.transform.SetParent (go2.transform);

			} else {	
				at.rotator.SetParent (temporaryObjectForRotationOnYAxis.transform);
				at.myCam.transform.SetParent (temporaryObjectForRotationOnYAxis.transform);
			}
			at.rotator.SetParent(temporaryObjectForRotationOnYAxis.transform);            
			float curAngleZ = at.rotator.localEulerAngles.z;            
			Quaternion start = at.rotator.transform.localRotation;

			while (Mathf.Abs(curAngleZ - angle) <= 720.0001f)
			{   

				if (addExtraPunchToSpin) {
					at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.up * extraSpinPunhcAmount);
				}


				//Wierd Rotation Here

				ExecuteWhileRotating ();
				mouseDragX3 = Input.GetAxis ("Mouse X") * rotationSpeed;
				if (at.rotateCamWithPlayer) {
					if (!Input.GetKey (at.shifter)) {
						go2.transform.position = at.rotator.position;
						go2.transform.localRotation = Quaternion.AngleAxis (go2.transform.localEulerAngles.y + mouseDragX3, at.myArmor.transform.up);
					}

				} else {
					temporaryObjectForRotationOnYAxis.transform.localRotation = Quaternion.AngleAxis (temporaryObjectForRotationOnYAxis.transform.localEulerAngles.y + mouseDragX3, at.myArmor.up);
				}
				curAngleZ = Mathf.MoveTowards(curAngleZ, angle, Time.deltaTime * flipSpeed*-1);
				at.rotator.transform.localRotation = Quaternion.AngleAxis (curAngleZ, axis) * start;

				//


				yield return null;
			}

			//Compensating due to unkown error on rotation;
			Vector3 compensation = at.rotator.localEulerAngles;
			compensation.z = 0;
			at.rotator.localEulerAngles = compensation;


			at.rotator.SetParent(at.myArmor);
			StartCoroutine(DestroyTempObjAfterTime(go2, temporaryObjectForRotationOnYAxis));
			midAirRot = false;
			rotating = false;


		}
		if (key == "A")
		{

			at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.right * sidePushForceWhenJump*-1);
			GameObject temporaryObjectForRotationOnYAxis = new GameObject();
			temporaryObjectForRotationOnYAxis.transform.position = at.myArmor.transform.position;
			temporaryObjectForRotationOnYAxis.transform.rotation = at.myArmor.transform.rotation;
			temporaryObjectForRotationOnYAxis.transform.SetParent(at.myArmor);
			GameObject go2 = new GameObject ();
			if (at.rotateCamWithPlayer) {
				at.myCam.transform.SetParent (at.rotator);
				go2.transform.position = at.rotator.position;
				go2.transform.rotation = at.rotator.rotation;
				go2.transform.SetParent (at.rotator.parent);
				temporaryObjectForRotationOnYAxis.transform.SetParent (go2.transform);

			} else {	
				at.rotator.SetParent (temporaryObjectForRotationOnYAxis.transform);
				at.myCam.transform.SetParent (temporaryObjectForRotationOnYAxis.transform);
			}
			at.rotator.SetParent(temporaryObjectForRotationOnYAxis.transform);            
			float curAngleZ = at.rotator.localEulerAngles.z;            
			Quaternion start = at.rotator.transform.localRotation;

			while (Mathf.Abs(curAngleZ - angle) >= .00001)
			{   


				if (addExtraPunchToSpin) {
					at.myBall.GetComponent<Rigidbody> ().AddForce (at.rotator.up * extraSpinPunhcAmount);
				}

				ExecuteWhileRotating ();
				mouseDragX3 = Input.GetAxis ("Mouse X") * rotationSpeed;
				if (at.rotateCamWithPlayer) {
					if (!Input.GetKey (at.shifter)) {
						go2.transform.position = at.rotator.position;
						go2.transform.localRotation = Quaternion.AngleAxis (go2.transform.localEulerAngles.y + mouseDragX3, at.myArmor.transform.up);
					}

				} else {
					temporaryObjectForRotationOnYAxis.transform.localRotation = Quaternion.AngleAxis (temporaryObjectForRotationOnYAxis.transform.localEulerAngles.y + mouseDragX3, at.myArmor.up);
				}
				curAngleZ = Mathf.MoveTowards(curAngleZ, angle, Time.deltaTime * flipSpeed);
				at.rotator.transform.localRotation = Quaternion.AngleAxis (curAngleZ, axis) * start;	
				yield return null;
			}				
			at.rotator.SetParent(at.myArmor);
			StartCoroutine(DestroyTempObjAfterTime(go2, temporaryObjectForRotationOnYAxis));
			midAirRot = false;


		}


		if (at.gimBall) {

			//localCamRot = at.myCam.transform.localRotation;
			//intialPosCam = at.myCam.transform.localPosition;
		
		}


        rotateCamWhileJumping = false;
        if (!at.cc.armorChild) {
            at.myCam.transform.SetParent(dad);
        }
        Destroy(go);
        if (!Input.GetKey(at.shifter)) {
			//Why this
            //at.rotator.localEulerAngles = new Vector3(0, at.rotator.localEulerAngles.y, 0);
           // at.rotator.transform.localPosition = Vector3.zero;
        }


			at.rotator.localEulerAngles = new Vector3 (0, at.rotator.localEulerAngles.y, 0);
			at.rotator.transform.localPosition = Vector3.zero;
		


		if (!at.cc.armorChild) {
			if (!Input.GetKey (at.shifter)) {}

			if (!at.gimBall) {
				at.myCam.transform.localRotation = localCamRot;
				at.myCam.transform.localPosition = intialPosCam;
			} else {
			
			
			}
			
				//print ("SETTING cAM ROTATION");
			
		}
		finishedRotation = true;
		if (Input.GetKey (at.shifter)) {
			at.aSSD.MakeCamTransferEntityChild ();
		}
        rotating = false;
		midAirRot = false;
		yield return null;
    }

	bool midRotationShifter;
	void ExecuteWhileRotating()
	{




		if (Input.GetKey (at.shifter)) {
			midRotationShifter = true;
		}


		if (Input.GetKeyUp (at.shifter)) {

			at.myArm.transform.localPosition = at.originalArm1LocalPos;
			at.myArm2.transform.localPosition = at.originalArm2LocalPos;

			at.myArm.transform.localRotation = Quaternion.identity;
			at.myArm2.transform.localRotation = Quaternion.identity;

			if (midRotationShifter) {
			}


			midRotationShifter = true;
		}
		
	}

	IEnumerator DestroyTempObjAfterTime(GameObject tmp1, GameObject tmp2)
	{
		midAirRot = false;
		rotating = false;
		yield return new WaitForSeconds (1);
		Destroy (tmp1);
		Destroy (tmp2);
	}
	IEnumerator SecondRotation()
	{
		float rotationSpeed = at.bC.rotationSpeed;

		midAirRot = true;
		if (!allreadyDidSecond) {
			allreadyDidSecond = true;
		}


		Vector3 intialPosCam = at.myCam.transform.localPosition;
		Transform dad = at.myCam.transform.transform.parent;
		float angle = 360 + 360-currentXrotation;
		float time = 0;
		time = jumpRotTime;
		float elapsedTime = Time.time + (time);
		Quaternion localCamRot = at.myCam.transform.localRotation;
		Quaternion localDesiredRot = Quaternion.identity;
		GameObject go = new GameObject();
		go.transform.position = at.myCam.transform.position;
		go.transform.rotation = at.myCam.transform.rotation;
		go.transform.parent = at.myCam.transform.parent;
		go.transform.localPosition = at.myCam.transform.localPosition;
		at.myCam.transform.SetParent(at.myArmor);


		//if (key == "W") {}

			rotating = true;
			GameObject temporaryObjectForRotationOnYAxis = new GameObject();
			temporaryObjectForRotationOnYAxis.transform.position = at.myArmor.transform.position;
			temporaryObjectForRotationOnYAxis.transform.rotation = at.myArmor.transform.rotation;
			temporaryObjectForRotationOnYAxis.transform.SetParent(at.myArmor);
			GameObject go2 = new GameObject ();
			if (at.rotateCamWithPlayer) {
				at.myCam.transform.SetParent (at.rotator);
				go2.transform.position = at.rotator.position;
				go2.transform.rotation = at.rotator.rotation;
				go2.transform.SetParent (at.rotator.parent);
				temporaryObjectForRotationOnYAxis.transform.SetParent (go2.transform);
			} else {	
				//at.rotator.SetParent (temporaryObjectForRotationOnYAxis.transform);
				rotateCamWhileJumping = true;
			}				
			at.rotator.SetParent(temporaryObjectForRotationOnYAxis.transform);
			at.myBall.GetComponent<Rigidbody>().AddForce(at.rotator.forward * sidePushForceWhenJump);
			float curAngleX = at.rotator.localEulerAngles.x;
			Quaternion start = at.rotator.transform.localRotation;
			while (Mathf.Abs(curAngleX - angle) > 0.0001f)
			{
				mouseDragX3 = Input.GetAxis ("Mouse X") * rotationSpeed;

				if (at.rotateCamWithPlayer) {
					go2.transform.position = at.rotator.position;
					go2.transform.localRotation = Quaternion.AngleAxis (go2.transform.localEulerAngles.y + mouseDragX3, at.myArmor.transform.up);
				} else {
					temporaryObjectForRotationOnYAxis.transform.localRotation = Quaternion.AngleAxis (temporaryObjectForRotationOnYAxis.transform.localEulerAngles.y + mouseDragX3, at.myArmor.up);
				}
				curAngleX = Mathf.MoveTowards (curAngleX, angle, Time.deltaTime * flipSpeed);
			at.rotator.transform.localRotation = Quaternion.AngleAxis (curAngleX, at.rotator.right) * start;

				currentXrotation = curAngleX;

				yield return null;
			}
			Destroy (go2);
			at.rotator.SetParent(at.myArmor);
			Destroy(temporaryObjectForRotationOnYAxis);
			midAirRot = false;
			rotating = false;

	}
}
