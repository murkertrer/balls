using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimingSystemSimpleDouble : MonoBehaviour {
	

	public bool turnOnLr;
	public bool turnOnLrWhenShifter;
	//Vector3 LastArmLocalRot;
	float mouseDragX;
	float rotSpeed;
	bool lookingToBeTransferEntityChild;

	float abap;
	Vector3 soughtLocalEulersCam;
	public bool LookCamToPlayer;
	public bool LookPlayerToCam;

	bool lerpingCamToSweetSpot;

	Quaternion LastCamLocalRot;
	Vector3 LastCamLocalPos;
	Vector3 LastCamLocalRotEuler;

	public bool gimBall;
	GameObject transferEntity;
	Quaternion tempRotArmor;
	public float ySens = 3;
	public float y;
	public float x2;
	public float y2;
	public Camera myCam;
	public float yMinLimit = 90;
	public float yMaxLimit = -90;
	Attributes at;
	Quaternion origianlRot;
	Quaternion originalRotR;
	Quaternion originalRotL;
	Quaternion restingRot;
	bool returningToOriginalRotSequence;
	bool lerpingarm;
	public bool onlyRight;
	public bool onlyLeft;
	public bool iFly;
	public bool lerpingLeft;
	public bool lerpingRight;
	public float widthOfDoubleShoot = 13;
	public float lerpRecuperateTime = .4f;
	public float lerpTimeArmLookUp = .5f;
	public float lerpTimeLookAtPoint = .2f;
	public bool aimLerp= true;
	public float allowedY = 40;
	public float allowedXUp = 90;
	public float allowedXDown = 45;
	public bool canShoot;
	bool returningCamToPos;
	public bool lookRotatorWhereHit;
	Vector3 originalCamPos;
	Quaternion originalCamRot;
	PlayerHandle ph;
	bool camIsLepring;
	public bool lerpingCamToArm;

	public bool aimingforHomming;
	public float screenSens = 4;
	float sH;
	float sW;


	void OnEnable () {
		ph = this.GetComponent<PlayerHandle> ();
		at = this.GetComponent<Attributes> ();
		myCam = at.myCam;
		origianlRot = myCam.transform.localRotation;
		originalRotR = at.myArm.localRotation;
		originalRotL = at.myArm2.localRotation;
		restingRot  = Quaternion.AngleAxis (-45, at.myArmor.right) * originalRotR;
		originalCamPos = at.myCam.transform.localPosition;
		originalCamRot = at.myCam.transform.localRotation;
	}
	void Start()
	{
		Vector3 screenPoint = new Vector3 (Screen.width/2, Screen.height/2 , 2);
		Vector3 WorldPos = at.myCam.ScreenToWorldPoint (screenPoint);
		at.aimObj.transform.position = WorldPos;
		at.aimLineRenderer.SetColors (Color.cyan, Color.green);
		at.aimLineRenderer2.SetColors (Color.cyan, Color.green);
		ResetAim ();

		if (at.useAimTraze) {
			lastThreshHoldAt = at.myArm.localEulerAngles.x;
			a = 360 - aimTrazeAnglesPerVertex;
			ThreshHoldSurpased = 2;
			origianlAimRPos = at.aimObjR1.position;
			GameObject go = new GameObject ();
			go.transform.position = origianlAimRPos;
			go.transform.SetParent (at.rotator.transform);
			aimTrazerGameObjectsPositions.Add (go);
			at.aimTraze.SetVertexCount (4);
		} else {
			at.aimTraze.enabled = false;
		}
	}
	void Update()
	{
		
		// && !at.subordination
		if (at.isSelected) {
			
			ShifterConditions ();
		} 
	
	}
	//float ThresholdDifference;
	int ThreshHoldSurpased;
	public float lastThreshHoldAt;
	public int aimTrazeAnglesPerVertex = 5;
	//List<Vector3> aimTrazerPositions = new List<Vector3>();
	List<GameObject> aimTrazerGameObjectsPositions = new List<GameObject>();
	Vector3 origianlAimRPos;
	//Vector3 LastPo
	float a = 0;
	int amountOfEvolutions =0;

	void AimTraze()
	{
		if (amountOfEvolutions >=1) {
			print ("back at top");


			for (int i = 0; i < amountOfEvolutions+4; i++) {
				print (amountOfEvolutions+4);


				if (i == 0) {
					at.aimTraze.SetPosition (0, at.myArm.position);				
				}


				if (i == 1) {
					at.aimTraze.SetPosition (1, aimTrazerGameObjectsPositions[i-1].transform.position);
				}//&& i  < amountOfEvolutions + 2
				if (i == 2 ) {
					at.aimTraze.SetPosition (2, aimTrazerGameObjectsPositions[i-1].transform.position);
				}



				if (i == amountOfEvolutions + 2) {
					at.aimTraze.SetPosition (i, at.aimObjR1.position);
				}
				if (i == amountOfEvolutions + 3) {
					print ("back to arm");
					at.aimTraze.SetPosition (i, at.myArm.transform.position);				
				}
			}

			if (at.myArm.localEulerAngles.x < a) {

				print ("hellllllllllllllll yea");
				CheckArmAnglesAndCompareWithThreshold ();
			}
		} 

		else 		
		{
			for (int i = 0; i < amountOfEvolutions+4; i++) {
				if (i == 0) {
					at.aimTraze.SetPosition (0, at.myArm.position);				
				}
				if (i == 1) {
					at.aimTraze.SetPosition (1, aimTrazerGameObjectsPositions[i-1].transform.position);
				}
				if (i > 1) {
					at.aimTraze.SetPosition (2, at.aimObjR1.position);
				}
				if (i == amountOfEvolutions + 3) {
					at.aimTraze.SetPosition (3, at.myArm.transform.position);				
				}
			}

			CheckArmAnglesAndCompareWithThreshold ();
		}
	}

	void CheckArmAnglesAndCompareWithThreshold()
	{
		if (at.myArm.localEulerAngles.x < a) {			
			at.aimTraze.SetVertexCount (5);
			amountOfEvolutions += 1;
			print ("evolition called " + amountOfEvolutions);
			a = at.myArm.localEulerAngles.x - aimTrazeAnglesPerVertex;
			ThreshHoldSurpased += 1;
			at.aimTraze.SetVertexCount (ThreshHoldSurpased+2);
			GameObject go = new GameObject ();
			go.transform.position = at.aimObjR1.position;
			go.transform.SetParent(at.rotator.transform);
			aimTrazerGameObjectsPositions.Add (go);

		}
	}

	//Enumerator CheckPeriofically;
	void ShifterConditions()
	{
		if (Input.GetKeyDown (at.shifter)) {

			//LastArmLocalRot = at.myArm.localEulerAngles;
			/*
			LastCamLocalPos = at.myCam.transform.localPosition;
			LastCamLocalRot = at.myCam.transform.localRotation;
			LastCamLocalRotEuler = at.myCam.transform.localEulerAngles;
			*/


			at.aimObj.SetActive (true);
			///&& !ph.isInFirstPerson
			if (at.slideToPoint && !ph.isInFirstPerson ) {
				if (!at.pW.midAirRot) {	
					if (!at.cc.movingCam) {
						MakeCamTransferEntityChild ();
					} 				
				} else {
					lookingToBeTransferEntityChild = true;
					StartCoroutine (WaitUntilFinishedRotatingToMakeCamTransferEntityChild ());
				}
			} else {
				print ("xxxx cont");
			}
		}
		if (Input.GetKey (at.shifter)) {
			/*
			LastCamLocalPos = at.myCam.transform.localPosition;
			LastCamLocalRot = at.myCam.transform.localRotation;
			LastCamLocalRotEuler = at.myCam.transform.localEulerAngles;
			*/
		}

		if (Input.GetKeyUp (at.shifter)) {
			ResetAim ();
			onlyLeft = false;
			onlyRight = false;

			lookingToBeTransferEntityChild = false;

			if (!at.gimBall) {
				//at.aimObj.SetActive (false);
			}
			if (!returningToOriginalRotSequence) {
				lerpingLeft = true;
				lerpingRight = true;
			}
			if (!lerpingarm) {
				//StartCoroutine (LookUpIfNoUse (lerpRecuperateTime, at.myArm, originalRotR)); 
				//StartCoroutine (LookUpIfNoUse (lerpRecuperateTime, at.myArm2, originalRotL));
			}
			if (at.slideToPoint) {
				//at.aSSD.y = 0;
				//at.aSSD.y2 = 0;
			}
			if (!ph.isInFirstPerson) {
				if (!at.pW.midAirRot) {
					if (at.returnToCamView) {

						if (!at.cc.movingCam) {
							at.myCam.transform.SetParent (at.rotator);


							StartCoroutine (at.cc.ReturnCamToOriginalRotAndPos ());

						} 
					} else {
						//This is return to PLayer View
						//Retain Arm Rotation;??
						StartCoroutine (at.cc.RetunPlayerToWhereCameraIsWatching ());

					}
				
					//StartCoroutine (ReturnArmzToCenter ());
				} else {
				
					StartCoroutine (WaitUntilFinishedRotatingToMakeCamReturnToDesignatedPosRot ());
				}
			} else {
			
				//StartCoroutine (at.cc.RetunPlayerToWhereCameraIsWatching ());
				//StartCoroutine (at.cc.RetunPlayerToWhereCameraIsWatching4FPS ());

			}

			//StartCoroutine (ReturnArmzToCenterRetainXRotation ());
			//StartCoroutine(ReturnArmzToCenterRetainXRotation());

			at.aimLineRenderer.enabled = false;
			at.aimLineRenderer2.enabled = false;
		}
		if (!Input.GetKey (at.shifter) && !Input.GetKey (at.powers) && !Input.GetKey (at.commander)) {
			if (!at.commandingUnits && !at.subordination) {
				if (!Input.GetMouseButton (1)) {
				}
				if (!at.cc.movingCam) {}
					TiltArm ();
			
				

			}
			/*
			if (at.commandingUnits) {
				if (!Input.GetMouseButton (1)) {
					TiltArm ();
				}
			}
			if (at.subordination) {
				if (Input.GetMouseButton (1)) {
					TiltArm ();
				}
			}
			*/
		}
	}
	IEnumerator WaitUntilFinishedRotatingToMakeCamReturnToDesignatedPosRot()
	{
		while (at.pW.midAirRot) {
			yield return null;
		}
		if (!ph.isInFirstPerson) {
			if (at.returnToCamView) {
				at.myCam.transform.SetParent (at.rotator);
				//StartCoroutine (at.cc.ReturnCamToOriginalRotAndPos ());
				//arm

			} else {
				//StartCoroutine (at.cc.RetunPlayerToWhereCameraIsWatching ());
			}
		}
		yield return null;
	}
	IEnumerator WaitUntilFinishedRotatingToMakeCamTransferEntityChild()
	{
		while (lookingToBeTransferEntityChild) {
			if (Input.GetKey (at.shifter)) {	
				if (!at.pW.midAirRot) {
					//MakeCamTransferEntityChild ();
					yield break;	
				} else {
					yield return null;
				}
			} else {
				if (!at.pW.midAirRot) {
					MakeCamTransferEntityChild ();

					yield break;	
				}			
			}
		}
		yield return null;
	}
	void LateUpdate()
	{
		if (at.kT.currentFPSPlayer == transform.root.gameObject) {
		}
		if (at.isSelected && !at.subordination) {
			LateShifter ();	
		}

		if (at.useAimTraze) {
			AimTraze ();
		}
	}
	void LateShifter()
	{
		if (Input.GetKey (at.shifter)) {

			if (!lerpingCamToSweetSpot) {
			}
			MoveAimObj ();
			if (!ph.isInFirstPerson) {	
				if (!at.commandingUnits) {
				}

				if (!at.pW.midAirRot) {
				}
				LookIfHit ();	
			
			} else {
				LookIfHitForFPS ();
				//Do the aim thing here

			}
			if (Input.GetMouseButton (1)) {

				if (!ph.isInFirstPerson) {

					if (!at.gimBall) {
						LastCamLocalRot = at.myCam.transform.localRotation;
						LastCamLocalPos = at.myCam.transform.localPosition;
						//Here Movement
						//WTF OFFSETTT Fix Here--------
						y += Input.GetAxis ("Mouse Y") * 4 * -1;
						y = Utils.ClampAngle (y, yMinLimit, yMaxLimit);
						Vector3 tmp = myCam.transform.localEulerAngles;
						tmp.x = y;

						soughtLocalEulersCam.x = tmp.x;
						soughtLocalEulersCam.y = myCam.transform.localEulerAngles.y;
						soughtLocalEulersCam.z = myCam.transform.localEulerAngles.z;

						if (!lerpingCamToArm && !at.comandoMode) {
							myCam.transform.localEulerAngles = tmp;
						}
						//Temporary Solution With delay;
						mouseDragX = Input.GetAxis ("Mouse X") * at.bC.rotationSpeed;
						at.myCam.transform.RotateAround (at.myBall.transform.position, at.myArmor.up, mouseDragX);
					}
				}
			}				
		} else {
		
			if (at.gimBall) {
				//LookIfHit ();	

				if (!Input.GetKey(at.controller) && !Input.GetKey(at.commander)) 
				{

					Ray ray = new Ray (at.myCam.transform.position,( at.aimObj.transform.position - at.myCam.transform.position));
					RaycastHit hit;	
					if (Physics.Raycast (ray, out hit, 100000, ~2)) {
						if (hit.transform != at.myBall) {

							LookAtPointInQuestion (hit.point);
						}
					}
				}			
			}		
		}
		if (Input.GetKeyUp(at.shifter))
		{
			//y = at.myArm.transform.localEulerAngles.x;		
			//y = (360 -y)*-1;

			if (ph.isInFirstPerson) {
				//at.myArm.transform.localEulerAngles = LastArmLocalRot;

				StartCoroutine (at.cc.RetunPlayerToWhereCameraIsWatching4FPS ());


			} else {
				DestroyTrasferEntityAndCleanGo();
			}
		}
		if (Input.GetKey(at.shifter))
		{
			if (Input.GetMouseButtonUp (1)) {
				DestroyTrasferEntityAndCleanGo();
			}
		}
	}
	public void MoveAimObj()
	{

		if (!lerpingCamToSweetSpot) {
			if (ph.isInFirstPerson) {
			}
			Vector2 screenPos = at.myCam.WorldToViewportPoint (at.aimObj.transform.position);
			if (screenPos.y < .9f && screenPos.y > .1f) {
				y2 += Input.GetAxis ("Mouse Y") * 10;
			}

			//Levleing mechanism
			if (screenPos.y > .9f) {
				y2 += -1.3f;
			}
			if (screenPos.y < .1f) {
				y2 += 1.3f;
			}
			if (screenPos.x < .9f && screenPos.x > .1f) {
				if (!Input.GetMouseButtonDown (1)) {
				}
				if (!Input.GetMouseButton (1)) {		
					x2 += Input.GetAxis ("Mouse X") * 10;
				}
			}
			if (screenPos.x > .9f) {
				x2 += -1.3f;
			}
			if (screenPos.x < .1f) {
				x2 += 1.3f;
			}
			Vector3 screenPoint = new Vector3 (x2, y2, 2);
			Vector3 WorldPos = at.myCam.ScreenToWorldPoint (screenPoint);
			at.aimObj.transform.position = WorldPos;		
		}
	}

	public IEnumerator ReturnAimToCenter()
	{

		float elapsedTime = 0;
		while (elapsedTime < at.cc.time2Return2Original) {	



			Vector2 screenPos = at.myCam.WorldToViewportPoint (at.aimObj.transform.position);

			//x2 = Mathf.MoveTowards(x2,Screen.width / 2, Time.deltaTime*10);
			//Implement here a MAth Larp

			x2 =  Screen.width / 2;
			y2 = Screen.height / 2;

			Vector3 screenPoint = new Vector3 (x2, y2, 2);
			Vector3 WorldPos = at.myCam.ScreenToWorldPoint (screenPoint);
			at.aimObj.transform.position = WorldPos;	

			if (Input.GetKeyUp (at.shifter)) {
				//yield break;
			}


			elapsedTime += Time.deltaTime;
			yield return null;
		}


		//Vector2 screenPos = at.myCam.WorldToViewportPoint (at.aimObj.transform.position);

	



	}





	public void MakeCamTransferEntityChild()
	{
		//StartCoroutine (at.cc.ReturnCamToOriginalRotAndPos ());

		if (transferEntity == null) {
			GameObject go = new GameObject ();
			go.transform.name = "xxx for transfer entity";
			transferEntity = go;
		} else {
		

		}

		transferEntity.transform.SetParent (at.rotator.parent);
		transferEntity.transform.position = at.rotator.transform.position;
		transferEntity.transform.rotation = at.rotator.transform.rotation;

		if (gimBall) {		
			//dont set this:
			//at.myCam.transform.SetParent (at.myArmor);
		} else {
			at.myCam.transform.SetParent (at.myArmor);
		}


	}
	void DestroyTrasferEntityAndCleanGo()
	{
		Destroy (transferEntity);
	}
	public void ResetAim()
	{		
		x2 = Screen.width / 2;
		y2 = Screen.height / 2;
		canShoot = true;
		at.aimLineRenderer.enabled = false;
		at.aimLineRenderer2.enabled = false;
		if (!at.gimBall) {
			at.aimObj.SetActive (false);
		}
		onlyLeft = false;
		onlyRight = false;
		MoveAimObj ();
	}
	void LookIfHitForFPS()
	{
		Ray ray = new Ray (at.myCam.transform.position,( at.aimObj.transform.position - at.myCam.transform.position));
		RaycastHit hit;	
		if (Physics.Raycast (ray, out hit, 10000, ~2)) {
			if (hit.transform != at.myBall) {
				if (Input.GetMouseButton(0)) {
					LookAtPointInQuestion (hit.point);
				}
				if (Input.GetMouseButtonUp (0)) {
					at.aimLineRenderer.enabled = false;
					at.aimLineRenderer2.enabled = false;					
				}
			}
		}
	}
	public void LookIfHit()
	{
		//This is for a smooth rotation or rotator. 
		Ray ray = new Ray (at.myCam.transform.position,( at.aimObj.transform.position - at.myCam.transform.position));
		RaycastHit hit;	
		if (Physics.Raycast (ray, out hit, 10000, ~2)) {
			if (hit.transform != at.myBall) {
				
				//Rotator only look if not midRotation;

				if (!Input.GetMouseButton (1)) {
					if (GetComponent<Powers> ().midAirRot == false) {

						LookAtPointInQuestionForRotator (hit.point);
						LookAtPointInQuestion (hit.point);
						/*
						Vector3 direction = hit.point - at.rotator.transform.position;
						Quaternion totalrot = Quaternion.FromToRotation (at.rotator.forward, direction);
						Quaternion justRot = Quaternion.LookRotation (direction);
						*/


						if (at.cc.armorChild) {
							//at.rotator.transform.localRotation = Quaternion.Slerp (at.rotator.transform.localRotation, justRot, Time.deltaTime * 2);
						} else {
							if (GetComponent<Powers> ().midAirRot == false) {
							}
							if (lookRotatorWhereHit) {			
							}							
						}
					} else {
	
						LookAtPointInQuestion (hit.point);
					}
				}

				CheckWhereHitPointHitsRelativeToPlayer (hit.point);
			} 
		}
	}
	void CheckWhereHitPointHitsRelativeToPlayer(Vector3 point)
	{
		Vector3 toTarget = (point - at.forwardReference.position).normalized;
		Debug.DrawRay (at.myBall.transform.position, toTarget, Color.cyan);


		if (Vector3.Dot (toTarget, at.forwardReference.forward) > 0) {
			canShoot = true;
			Vector3 directionToTarget = at.myBall.transform.position - point;
			float angle = Vector3.Angle (at.myBall.parent.GetComponent<Attributes> ().rotator.right, directionToTarget);
			if (Mathf.Abs (angle) > 90 + (widthOfDoubleShoot / 2)) {
				DoTheAimThing (point, 1);
				onlyRight = true;
				//StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm2, restingRot));
				at.aimLineRenderer2.enabled = false;
			}
			if (Mathf.Abs (angle) < 90 - (widthOfDoubleShoot / 2)) {
				DoTheAimThing (point, 2);
				onlyLeft = true;
				at.aimLineRenderer.enabled = false;
				//StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm, restingRot));
				//Left shoot
			}
			if (Mathf.Abs (angle) < 90 + (widthOfDoubleShoot / 2) && Mathf.Abs (angle) > 80 - (widthOfDoubleShoot / 2)) {
				DoTheAimThing (point, 3);
				onlyLeft = false;
				onlyRight = false;
				//Both shhoot
			}
		} else {
			//StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm, restingRot));
			//StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm2, restingRot));
			TurnOffBothLR ();
			canShoot = false;

			print ("now");
			at.myArm.transform.localEulerAngles = at.originalArm1LocalPos;
			at.myArm2.transform.localEulerAngles = at.originalArm2LocalPos;
			//Behind Player


		}


	}
	void DoTheAimThing(Vector3 it, int caseOfArm)
	{
		if (caseOfArm == 1) {
			Vector3 relativePosz = it - at.myArm.position;
			Quaternion desiredRotx = Quaternion.LookRotation (relativePosz);
			Quaternion desiredRotz = Quaternion.AngleAxis (at.rotator.localEulerAngles.y, at.forwardReference.up) * desiredRotx;

				if (!aimLerp) {
					at.myArm.transform.LookAt (it);
				}
				if (aimLerp) {
					Vector3 relativePos = it - at.myArm.position;
					Quaternion desiredRot = Quaternion.LookRotation (relativePos);
					at.myArm.transform.rotation = Quaternion.Lerp (at.myArm.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
				}
			if (turnOnLr) {

				if (turnOnLrWhenShifter) {
				
					if (Input.GetKey (at.shifter)) {
						at.aimLineRenderer.enabled = true;

					}
				} else {

					if (Input.GetMouseButtonDown (0)) {
						at.aimLineRenderer.enabled = true;
					}
				}
			}
			if (Input.GetMouseButton (0)) {
				RaycastHit hit2;	
				Vector3 fwd = at.myArm.transform.TransformDirection (Vector3.forward);
				//Debug.DrawRay (at.myArm.position, at.myArm.forward, Color.red);
				if (Physics.Raycast(at.myArm.position, fwd, out hit2,10000, ~2))
				{
					at.aimLineRenderer.SetPosition (0, at.myArm.transform.position);
					at.aimLineRenderer.SetPosition (1, hit2.point);
				}
				//aimLineRenderer.SetPosition (0, myArm.transform.position);
				//aimLineRenderer.SetPosition (1, it);
			}
			if (Input.GetMouseButtonUp (0)) {
				at.aimLineRenderer.enabled = false;
			}
		}		
		if (caseOfArm == 2) {

			Vector3 relativePosz = it - at.myArm2.position;
			Quaternion desiredRotz = Quaternion.LookRotation (relativePosz);
			/*
			bool checkY = CheckArmRotationY (desiredRotz.eulerAngles, "left");
			bool checkX = CheckArmRotationX (desiredRotz.eulerAngles);
			if (checkX && checkY) {	}
			*/
			if (!aimLerp) {
				at.myArm2.transform.LookAt (it);
			}
			if (aimLerp) {
				Vector3 relativePos = it - at.myArm2.position;
				Quaternion desiredRot = Quaternion.LookRotation (relativePos);
				at.myArm2.transform.rotation = Quaternion.Lerp (at.myArm2.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
			}
			if (turnOnLr) {
				if (Input.GetMouseButtonDown (0)) {
					at.aimLineRenderer2.enabled = true;
				}
			}
            if (Input.GetMouseButton (0)) {
				RaycastHit hit2;	
				Vector3 fwd = at.myArm2.transform.TransformDirection (Vector3.forward);
				if (Physics.Raycast(at.myArm2.position, fwd, out hit2,10000, ~2))
				{
					at.aimLineRenderer2.SetPosition (0, at.myArm2.transform.position);
					at.aimLineRenderer2.SetPosition (1, hit2.point);
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				at.aimLineRenderer2.enabled = false;
			}
		
		}
		if (caseOfArm == 3) {
			if (!aimLerp) {
				at.myArm.transform.LookAt (it);
				at.myArm2.transform.LookAt (it);
			}
			else
			{
				Vector3 relativePos = it - at.myArm.position;
				Quaternion desiredRot = Quaternion.LookRotation (relativePos);
				at.myArm.transform.rotation = Quaternion.Lerp (at.myArm.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
				Vector3 relativePos2 = it - at.myArm2.position;
				Quaternion desiredRot2 = Quaternion.LookRotation (relativePos);
				at.myArm2.transform.rotation = Quaternion.Lerp (at.myArm2.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
			}
			if (turnOnLr) {

				if (turnOnLrWhenShifter) {

					if (Input.GetKey (at.shifter)) {
						at.aimLineRenderer.enabled = true;
						at.aimLineRenderer2.enabled = true;

					}
				} else {

					if (Input.GetMouseButton (0)) {
					
						at.aimLineRenderer.enabled = true;
						at.aimLineRenderer2.enabled = true;

					}
				}
			

				if (Input.GetMouseButton (0) || Input.GetKey (at.shifter) ) {
					//at.aimLineRenderer.enabled = true;
					//at.aimLineRenderer2.enabled = true;

	                //Fix Here so that AimLineRenderer Does not Get blocked By One´s projectiles. 

					RaycastHit hit21;	
					Vector3 fwd = at.myArm.transform.TransformDirection (Vector3.forward);
					if (Physics.Raycast(at.myArm.position, fwd, out hit21,10000, ~2))
					{
						at.aimLineRenderer.SetPosition (0, at.myArm.transform.position);
						at.aimLineRenderer.SetPosition (1, hit21.point);
					}


					RaycastHit hit2;	
					Vector3 fwd1 = at.myArm2.transform.TransformDirection (Vector3.forward);
					if (Physics.Raycast(at.myArm2.position, fwd1, out hit2,10000, ~2))
					{
						at.aimLineRenderer2.SetPosition (0, at.myArm2.transform.position);
						at.aimLineRenderer2.SetPosition (1, hit2.point);
					}
				}
				if (Input.GetMouseButtonUp (0)) {
					at.aimLineRenderer.enabled = false;
					at.aimLineRenderer2.enabled = false;
				}
				if (Input.GetMouseButton (1)) {
					at.myArm.transform.LookAt (it);
					at.myArm2.transform.LookAt (it);
				}
			}
		}
	}
	public void getYbasedonArmX()
	{
		y = at.myArm.transform.localEulerAngles.x;		
		y = (360 -y)*-1;
		
	}
	void TiltArm()
	{
		
		if (!lerpingarm ) {
			if (!Input.GetKey (at.shifter)) {
				if (!at.gTC.automaticArm) {
					if (!Input.GetMouseButton (1)) {
						if (at.myArm.localEulerAngles.x > 270 && at.myArm.localEulerAngles.x <= 360) {
						}
						if (at.myArm.localEulerAngles.x <= 90) {
							y += Input.GetAxis ("Mouse Y") * ySens * -1;
							y = Utils.ClampAngle (y, 0, 90);
						}
						if (at.myArm.localEulerAngles.x >= 270) {
							y += Input.GetAxis ("Mouse Y") * ySens * -1;
							y = Utils.ClampAngle (y, yMinLimit, yMaxLimit);
						}
						if (at.myArm.localEulerAngles.x == 0) {
							y -= .01f;
								//Input.GetAxis ("Mouse Y") * ySens * -1;
						}										
					}
					if (Input.GetMouseButton (1)) {
						y += Input.GetAxis ("Mouse Y") * ySens * -1;

						if (at.myArm.localEulerAngles.x < 90 && at.myArm.localEulerAngles.x > 0) {
							y = Utils.ClampAngle (y, 0, 89);
						}
						if (at.myArm.localEulerAngles.x > 270 && at.myArm.localEulerAngles.x <= 360) {
							y = Utils.ClampAngle (y, yMinLimit, yMaxLimit);
						}
					}

					Vector3 tmp = at.myArm.transform.localEulerAngles;
					tmp.x = y;
					at.myArm.transform.localEulerAngles = tmp;
					at.myArm2.transform.localEulerAngles = tmp;


				}
			}
		}

	}
	public void LookAtPointInQuestionForRotator(Vector3 it)
	{
		//Make Rotator Only Look at Object on one Axis;

		if (!Input.GetMouseButton (1)) {
			if (GetComponent<Powers> ().midAirRot == false) {

				//Vector3 rotatorMinus
				//it.x = at.rotator.position.x;
				Vector3 direction = it - at.rotator.transform.position;

				//direction.x = at.rotator.transform.position.x;
				Quaternion justRot = Quaternion.LookRotation (direction);
				//Quaternion justRot = Quaternion.Euler(direction);

			
				if (at.cc.armorChild) {
					if (at.slideToPoint) {
						//at.rotator.transform.localRotation = Quaternion.Slerp (at.rotator.transform.localRotation, justRot, Time.deltaTime * 2);
					} 
				} else {
					if (at.slideToPoint) {
						at.rotator.transform.localRotation = Quaternion.Slerp (at.rotator.transform.localRotation, justRot, Time.deltaTime * 4);
					} 
				}
			}
		}		
	}

	//TEMP FROM COMMANDOER
	public void LookAtSaysCommander(Vector3 it)
	{
		//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//cube.transform.position =  it;

		LookAtPointInQuestion (it);
		LookAtPointInQuestionForRotator(it);
	}
	public void LookAtPointInQuestion(Vector3 it)
	{


		Vector3 toTarget = (it - at.forwardReference.position).normalized;
		if (Vector3.Dot (toTarget, at.forwardReference.forward) > 0) {

			print ("We are here");
			canShoot = true;
			Vector3 directionToTarget = at.myBall.transform.position - it;
			float angle = Vector3.Angle (at.myBall.parent.GetComponent<Attributes> ().rotator.right, directionToTarget);
			if (Mathf.Abs (angle) > 90 + (widthOfDoubleShoot / 2)) {

				DoTheAimThing (it, 1);
				onlyRight = true;
				//StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm2, restingRot));
				at.aimLineRenderer2.enabled = false;
			}
			if (Mathf.Abs (angle) < 90 - (widthOfDoubleShoot / 2)) {
				DoTheAimThing (it, 2);
				onlyLeft = true;
				at.aimLineRenderer.enabled = false;
				//StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm, restingRot));
				//Left shoot
			}
			if (Mathf.Abs (angle) < 90 + (widthOfDoubleShoot / 2) && Mathf.Abs (angle) > 80 - (widthOfDoubleShoot / 2)) {
				DoTheAimThing (it, 3);
				onlyLeft = false;
				onlyRight = false;
				//Both shhoot
			}
		} else {
			// behind

			TurnOffBothLR ();
			canShoot = false;

			at.myArm.transform.localEulerAngles = at.originalArm1LocalPos;
			at.myArm2.transform.localEulerAngles = at.originalArm2LocalPos;
		}


	} 
	public void TurnOffBothLR()
	{
		at.aimLineRenderer.enabled = false;
		at.aimLineRenderer2.enabled = false;
	}
	public void AimForHomming()
	{
		/*
		if (Input.GetKey (shifter)) {
			sH = Screen.height;
			sW = Screen.width;

			aimingforHomming = true;

			y2 += Input.GetAxis ("Mouse Y") * screenSens;
			if (y2 >= Screen.height) {
				y2 = sH;
			}
			if (y2 <= 0) {
				y2 = 0;
			}

			x2 += Input.GetAxis ("Mouse X") * screenSens;
			if (x2 >= sW) {
				x2 = sW;
			}
			if (x2 <= 0) {
				x2 = 0;
			}

			Vector3 screenPoint = new Vector3 (x2, y2, 2);
			Vector3 WorldPos = this.GetComponent<Attributes> ().myCam.ScreenToWorldPoint (screenPoint);
			aimObj.SetActive (true);
			aimObj.transform.position = WorldPos;

		}*/
	}
	public void endAimForHomming()
	{
		/*
		aimingforHomming = false;

			aimObj.SetActive (false);
			x2 = sW / 2;;
			y2 = sH/2;
			*/

	}
	/*
	public IEnumerator CamToOriginalPosition()
	{
		float t = .2f;
		returningCamToPos = true;
		float elapsedTime = 0;
		while (elapsedTime < t) {
			Vector3 tempRotArmor = new Vector3 (LastCamLocalRotEuler.x, 
				at.myCam.transform.localEulerAngles.y, at.myCam.transform.localEulerAngles.z);
			at.myCam.transform.localRotation = Quaternion.Lerp(at.myCam.transform.localRotation, Quaternion.Euler(tempRotArmor), (elapsedTime / t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	*/
	public IEnumerator RotatorNoTiltOnX(float t)
	{
		Vector3 RotNoX = new Vector3 (0, at.rotator.transform.localEulerAngles.y, at.rotator.transform.localEulerAngles.z);		
		Quaternion desiredRot = Quaternion.Euler (RotNoX);
		float elapsedTime = 0;
		while (elapsedTime < t) {
			//at.rotator.transform.localRotation = Quaternion.Lerp (at.rotator.transform.localRotation, rot, (elapsedTime / t));
			if (!at.cc.armorChild) {
				//at.rotator.transform.localRotation = Quaternion.Lerp(at.rotator.localRotation, desiredRot, (elapsedTime / t));
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		returningCamToPos = false;
	}
	IEnumerator LookUpIfNoUse(float time, Transform armz, Quaternion rot)
	{
		float elapsedTime = 0;
		while (elapsedTime < time) {
			//armz.transform.localRotation= Quaternion.Lerp (armz.transform.localRotation, rot, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	IEnumerator ReturnArmzToCenter()
	{
		print ("xxx returning arms");

		lerpingarm = true;
		Vector3 retainRotX = new Vector3 (at.myArm.eulerAngles.x, 0, 0);
		Quaternion qRetainRotX = Quaternion.Euler (retainRotX);

		float time = at.rotationSpeed/5;
		float elapsedTime = 0;
		while (elapsedTime < time) {
			//at.myArm.transform.rotation= Quaternion.Lerp (at.myArm.transform.localRotation, qRetainRotX, (elapsedTime / time));
			//at.myArm2.transform.localRotation= Quaternion.Lerp (at.myArm.transform.localRotation, qRetainRotX, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		 
		//y = at.myArm.transform.localEulerAngles.x;

		lerpingarm = false;
	}
	IEnumerator ReturnArmzToCenterRetainXRotation()
	{
		/*
		GameObject go = new GameObject ();
		go.transform.position = at.rotator.transform.position;
		go.transform.rotation = at.rotator.transform.rotation;
		go.transform.SetParent (at.rotator.parent);


		at.myArm.transform.SetParent (go.transform);
		at.myArm2.transform.SetParent (go.transform);

		lerpingarm = true;
		Vector3 retainRotX = new Vector3 (at.myArm.localEulerAngles.x, 0, 0);
		Quaternion qRetainRotX = Quaternion.Euler (retainRotX);

		float time = at.rotationSpeed/5;
		float elapsedTime = 0;
		while (elapsedTime < time) {

			go.transform.localPosition = at.rotator.transform.localPosition;
			go.transform.localRotation = at.rotator.transform.localRotation;

			Vector3 mimicRotatorOnY = at.rotator.localEulerAngles;
			//mimicRotatorOnY.x = 0;
			//mimicRotatorOnY.z = 0;
			Quaternion mimicLocalRot = Quaternion.Euler (mimicRotatorOnY);
			go.transform.localRotation = mimicLocalRot;



			//at.myArm.transform.localRotation= Quaternion.Lerp (at.myArm.transform.localRotation, qRetainRotX, (elapsedTime / time));
			//at.myArm2.transform.localRotation= Quaternion.Lerp (at.myArm.transform.localRotation, qRetainRotX, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}


		y = at.myArm.transform.localEulerAngles.x;

		lerpingarm = false;
		*/
		yield return null;
	}
}
