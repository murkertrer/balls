using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	bool pendingMakeArmorChild;
	public bool movingCam;
	public bool rotatingPlayerToCamFromAssdLookPlayerToCam;
	public bool closeTheDistanceOnlyOnRightMouse = true;
	public KeyCode controller;
	public KeyCode adjustCam;
	Vector3 originalLocalPos;
	Vector3 tmpCamEuler;
	public Camera myCam;
	float abap = 0f;
	public float time2Return2Original = .5f;
	public float minCamTilt = -15;
	public float maxCamTilt = 50;
	public float camTiltSens = 5;
	public Quaternion originalRot;
	bool rotating;
	public float lerpSpeed = 5;
	public bool tiltAsArm;
	Transform arm;
	Attributes at;
	public bool armorChild;
	public bool armorChild2;
	float y;
	float x;
	float rndm = 0;
	public bool changeChangeCamRotationDependingOnArm= true;
	public float originalDistance;
	public float rClickInterval  = 1;
	float timeWhenRecievedClick;
	float nextClickActivation;
	public bool rightClickRecieved;
	float initialCamHeight;
	public Quaternion originalCamLocalRot;
	public float rightclickLerpRate = 6;
	public bool changeCamPosAndRotAccordingToArmsX = false;
	public bool firstPersonCam;
	PlayerHandle ph;
	public float speed = 3;
	bool currentlyUnderTransform;

	void OnEnable () {
		at = transform.root.GetComponent<Attributes> ();
		ph = GetComponent<PlayerHandle> ();
		arm = at.myArm;
		controller = at.controller;
		myCam = at.myCam;
		originalRot = myCam.transform.localRotation;
		adjustCam = at.camControl;
		originalLocalPos = at.myCam.transform.localPosition;
		originalDistance = Vector3.Distance (at.myCam.transform.position, at.myBall.transform.position);
		initialCamHeight = at.myCam.transform.localPosition.y;
		originalCamLocalRot = at.myCam.transform.localRotation;
		at.CameraDummy.transform.localPosition = myCam.transform.localPosition;
	}


	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			SwitchFPS ();
		}

		if (Input.GetKey (adjustCam)) {
			AdjustCam ();
		}			

		CheckForDeCouple ();
		if (armorChild) {
			if (Input.GetMouseButton (1)) {
			}
				TakeChildToSweetSpot ();			
		}
	}
	void TakeChildToSweetSpot()
	{
		if (at.pW.midAirRot == false) {
			if (Input.GetMouseButton (1)) {
				at.myCam.transform.SetParent (at.CameraDummy.transform.parent);

				at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, at.CameraDummy.transform.localRotation, Time.deltaTime * 5);
				at.myCam.transform.position = Vector3.Lerp (at.myCam.transform.position, at.CameraDummy.transform.position, Time.deltaTime * 5);
			}
			if (Input.GetMouseButtonUp (1)) {
				at.myCam.transform.SetParent (at.myArmor);
			}
		} else {

		//Make Cam Go To sweet spot even if its rotating
		
		}
	}
	void SwitchFPS()
	{
		ph.isInFirstPerson = !ph.isInFirstPerson;

		if (ph.isInFirstPerson) {
			at.myBall.GetComponent<Renderer> ().enabled = false;
			ph.isInFirstPerson = true;
			at.aimObjR1.gameObject.SetActive (false);
			at.aimObjL1.gameObject.SetActive (false);
			StartCoroutine (CamToBall (1));

		}
		if (!ph.isInFirstPerson) {
			at.myBall.GetComponent<Renderer> ().enabled = true;
			at.centeredAimObj.SetActive (false);
			at.aimObjL1.gameObject.SetActive (true);
			at.aimObjR1.gameObject.SetActive (true);
			StartCoroutine (CamToDummy (1));
		}
	}

	public void ReturnFromGimBall()
	{

		if (!ph.isInFirstPerson) {	}
			at.myBall.GetComponent<Renderer> ().enabled = true;
			at.centeredAimObj.SetActive (false);
			at.aimObjL1.gameObject.SetActive (true);
			at.aimObjR1.gameObject.SetActive (true);
			StartCoroutine (CamToDummy (1));
	
		
	}


	void CheckForDeCouple()
	{


		if (Input.GetKeyDown (at.change2ArmorChild)) {
			CheckForDeCoupling ();
		}
		/*
		if (rightClickRecieved) {
			if (nextClickActivation > Time.time) {
				if (Input.GetMouseButtonDown (1)) {
					CheckForDeCoupling ();
				}
			} else {
				nextClickActivation = 0;
				timeWhenRecievedClick = 0;
				rightClickRecieved = false;
			}
		}

		if (!rightClickRecieved) {
			if (Input.GetMouseButtonDown (1) && !Input.GetKey(at.commander)) {
				rightClickRecieved = true;
				timeWhenRecievedClick = Time.time;
				nextClickActivation = timeWhenRecievedClick + rClickInterval;
			}
		}

		if (armorChild) {
				
		}
		*/
	}
	public IEnumerator CamToDummy(float t)
	{
		movingCam = true;
		Vector3 pointOfDeployment = at.CameraDummy.transform.position;
		Quaternion rotationOfDeployment = at.CameraDummy.transform.rotation;


		float elapsedTime = 0;
		while (elapsedTime < t) {
			if (pendingMakeArmorChild) {

				//Improve Here a Lil Jittery
				at.myCam.transform.rotation = Quaternion.Lerp (at.myCam.transform.rotation,rotationOfDeployment, (elapsedTime / t));
				at.myCam.transform.position = Vector3.Lerp (at.myCam.transform.position, pointOfDeployment, (elapsedTime / t));

			} else {
				at.myCam.transform.rotation = Quaternion.Lerp (at.myCam.transform.rotation, at.CameraDummy.transform.rotation, (elapsedTime / t));
				at.myCam.transform.position = Vector3.Lerp (at.myCam.transform.position, at.CameraDummy.transform.position, (elapsedTime / t));
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		if (pendingMakeArmorChild) {
			at.myCam.transform.SetParent (at.myArmor);
			pendingMakeArmorChild = false;
		}
		movingCam = false;
	}
	public IEnumerator CamToBall(float t)
	{
		movingCam = true;

		if (armorChild) {

		}
		GameObject go = new GameObject ();
		GameObject go2 = new GameObject ();

		go.transform.position = at.rotator.position;
		go2.transform.position = at.rotator.position;

		go2.transform.SetParent (at.rotator.parent);
		go.transform.SetParent (go2.transform);
		float mouseDragX = 0;
		float time = time2Return2Original;


		float elapsedTime = 0;
		while (elapsedTime < t) {

			Vector3 tempRot = at.myCam.transform.eulerAngles;
			tempRot.x = at.myArm.transform.eulerAngles.x;
			//at.myCam.transform.localEulerAngles = tempRot;
			Quaternion itz = Quaternion.Euler(tempRot);
			at.myCam.transform.rotation = Quaternion.Lerp (at.myCam.transform.rotation, itz, (elapsedTime / t));

			at.myCam.transform.position = Vector3.Lerp (at.myCam.transform.position, at.myBall.transform.position, (elapsedTime / t));

			if (armorChild) {
				Vector3 desiredRot2 = new Vector3 (0, at.myCam.transform.eulerAngles.y, at.myCam.transform.eulerAngles.z);
				Quaternion it = Quaternion.Euler (desiredRot2);
				at.rotator.transform.rotation = Quaternion.Slerp(at.rotator.rotation, it, (elapsedTime / time));

				mouseDragX = Input.GetAxis ("Mouse X") * at.bC.rotationSpeed;
				go.transform.RotateAround (at.myBall.transform.position, at.myArmor.up, mouseDragX);
			}


			elapsedTime += Time.deltaTime;
			yield return null;
		}
		movingCam = false;
		at.centeredAimObj.SetActive (true);	

		if (armorChild) {
			at.myCam.transform.SetParent(at.rotator);
			pendingMakeArmorChild = true;
		}
		Destroy (go);
		Destroy (go2);
	}
	public void GetCamTilting()
	{

		//print (myCam.transform.localEulerAngles.x);
		abap += Input.GetAxis ("Mouse Y") * camTiltSens *-1;
		abap = ClampAngle (abap, minCamTilt, maxCamTilt);
		TiltCamera (abap);
	}
	void CheckForDeCoupling ()
	{
		if (at.myCam.transform.parent == at.myArmor) {
			StartCoroutine (DriftToposition (1));
		} 
		else
		{
			
		//	at.lastKnownCamRotPos = at.myCam.transform.transform.position;
		//	at.lastKnownCamRotRot = at.myCam.transform.localRotation;
			StartCoroutine(at.aSSD.RotatorNoTiltOnX(.1f));
			at.myCam.transform.parent = at.myArmor;
			armorChild = true;
		}
	}
	public IEnumerator DriftToposition( float time)
	{
		at.myCam.transform.parent = at.CameraDummy.transform.parent;
		armorChild = false;
		float elapsedTime = 0;
		at.CameraDummy.transform.localEulerAngles = Vector3.zero;
		while (elapsedTime < time) {
			at.myCam.transform.position = Vector3.Lerp (at.myCam.transform.position, at.CameraDummy.transform.position, (elapsedTime / time));
			at.myCam.transform.rotation = Quaternion.Lerp (at.myCam.transform.rotation, at.CameraDummy.transform.rotation, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	void LateUpdate()
	{
		if (ph.isInFirstPerson) {
			if (Input.GetKeyUp (at.shifter)) {
				Vector3 forArmdoCam = new Vector3 (
					                      at.myCam.transform.localEulerAngles.x,
					                      0,
					                      at.myCam.transform.localEulerAngles.z);
				at.myArm.transform.localEulerAngles = forArmdoCam;
			}

			if (!Input.GetKey (at.shifter)) {
				if (!movingCam) {
					Vector3 tempRot = at.myCam.transform.localEulerAngles;
					tempRot.x = at.myArm.transform.localEulerAngles.x;
					at.myCam.transform.localEulerAngles = tempRot;
				}

			}
		} else {
			if (!Input.GetKey (at.shifter)) {
				if (changeCamPosAndRotAccordingToArmsX && GetComponent<Powers> ().midAirRot == false) {
					if (closeTheDistanceOnlyOnRightMouse) {

						if (!movingCam) {
							if (!armorChild && !Input.GetKey(at.commander)) {

							if (Input.GetMouseButtonDown (1)) {	

									initialCamHeight  = at.myCam.transform.localPosition.y;
							}
							if (Input.GetMouseButton (1)) {	

									if (!at.gimBall)
									{

									CloseTheDistance ();	
								
									}
								}
							
							if (Input.GetMouseButtonUp (1)) {
									if (!at.gimBall) {
										StartCoroutine (ReturnCamToOriginalRotAndPos ());
									}
								}
							}
						}
					}
				}
			}

			if (Input.GetKey(at.shifter))
			{
				//originalCamLocalRot = at.myCam.transform.localRotation;
				//originalLocalPos = at.myCam.transform.localPosition;
			}

			if (rotating) {		
				float angle = Mathf.LerpAngle (myCam.transform.localEulerAngles.x, 0, Time.deltaTime * lerpSpeed);
				myCam.transform.localEulerAngles = new Vector3 (angle, 0, 0);
				if (Mathf.RoundToInt (myCam.transform.localEulerAngles.x) == 0) {
					rotating = false;//print ("arrived by mathf Round");
				}
				if (Mathf.Approximately (myCam.transform.localEulerAngles.x, 0)) {
					rotating = false;//print ("arrived by aprox");
				}
			}
		}

		if (at.cameraCardianlity) {

			//Is this reallly usefull?

			MantainCameraCardinalityInLine ();
		
		}
	
	}
	void MantainCameraCardinalityInLine()
	{
		at.cameraCardianlity.transform.position = at.myCam.transform.position;
		//at.cameraCardianlity.transform.SetParent (at.myCam.transform);
		Vector3 cardinalityRestrictingX = new Vector3 (0, at.myCam.transform.localRotation.y, at.myCam.transform.localRotation.z);
		at.cameraCardianlity.transform.eulerAngles = cardinalityRestrictingX;
		//at.cameraCardianlity.transform.localRotation = at.myCam.transform.localRotation;
	}
	public void ReturnCam2Origin()
	{
		if (!movingCam) {
			//StartCoroutine (ReturnCamToOriginalRotAndPos ());
		}
	}
	public IEnumerator ReturnCamToOriginalRotAndPos()
	{	
		//
		//at.aSSD.LerpingArm(true);
		/*
		Vector3 curArmAngles = at.myArm.eulerAngles;
		curArmAngles.y = 0;
		curArmAngles.z = 0;
		Quaternion RotRetainX = Quaternion.Euler(curArmAngles);
		*/
		float relativeX = at.rotator.transform.localEulerAngles.x + at.myArm.localEulerAngles.x;
		Vector3 retainRotX = new Vector3 (relativeX, 0, 0);
		Quaternion qRetainRotX = Quaternion.Euler (retainRotX);

		movingCam = true;
		float elapsedTime = 0;
		while (elapsedTime < time2Return2Original) {

			if (Input.GetKey(at.shifter))
			{
				
				movingCam = false;
				//A Methood to return To Good Position
				at.aSSD.MakeCamTransferEntityChild ();
				yield  break;
			}

			at.myCam.transform.localRotation= Quaternion.Lerp (at.myCam.transform.localRotation, originalCamLocalRot, (elapsedTime / time2Return2Original));
			at.myCam.transform.localPosition= Vector3.Lerp (at.myCam.transform.localPosition, originalLocalPos, (elapsedTime / time2Return2Original));
			elapsedTime += Time.deltaTime;
			Vector3 RotNoX = new Vector3 (0, at.rotator.transform.localEulerAngles.y, at.rotator.transform.localEulerAngles.z);		
			Quaternion desiredRot = Quaternion.Euler (RotNoX);
			at.rotator.transform.localRotation = Quaternion.Lerp(at.rotator.transform.localRotation, 
				desiredRot, (elapsedTime / time2Return2Original));

	
			at.myArm.transform.localRotation= Quaternion.Lerp (at.myArm.transform.localRotation, qRetainRotX, (elapsedTime / time2Return2Original));
			at.myArm2.transform.localRotation= Quaternion.Lerp (at.myArm2.transform.localRotation, qRetainRotX, (elapsedTime / time2Return2Original));

			//at.myArm.transform.rotation = Quaternion.Lerp(at.myArm.transform.rotation, RotRetainX, Time.deltaTime*50);


			yield return null;

		}
		//
		//at.aSSD.LerpingArm(false);
		at.aSSD.getYbasedonArmX();

		if (armorChild) {
			at.myCam.transform.SetParent (at.myArmor);
		}

		movingCam = false;
	}
	public IEnumerator ReturnCamToOriginalRotAndPosNoModArm()
	{	

		float relativeX = at.rotator.transform.localEulerAngles.x + at.myArm.localEulerAngles.x;
		Vector3 retainRotX = new Vector3 (relativeX, 0, 0);
		Quaternion qRetainRotX = Quaternion.Euler (retainRotX);

		movingCam = true;
		float elapsedTime = 0;
		while (elapsedTime < time2Return2Original) {

			if (Input.GetKey(at.shifter))
			{

				movingCam = false;
				//A Methood to return To Good Position
				at.aSSD.MakeCamTransferEntityChild ();
				yield  break;
			}

			at.myCam.transform.localRotation= Quaternion.Lerp (at.myCam.transform.localRotation, originalCamLocalRot, (elapsedTime / time2Return2Original));
			at.myCam.transform.localPosition= Vector3.Lerp (at.myCam.transform.localPosition, originalLocalPos, (elapsedTime / time2Return2Original));
			elapsedTime += Time.deltaTime;
			Vector3 RotNoX = new Vector3 (0, at.rotator.transform.localEulerAngles.y, at.rotator.transform.localEulerAngles.z);		
			Quaternion desiredRot = Quaternion.Euler (RotNoX);
			at.rotator.transform.localRotation = Quaternion.Lerp(at.rotator.transform.localRotation, 
				desiredRot, (elapsedTime / time2Return2Original));
			yield return null;

		}

		at.aSSD.getYbasedonArmX();

		if (armorChild) {
			at.myCam.transform.SetParent (at.myArmor);
		}
		movingCam = false;
	}
	public IEnumerator RetunPlayerToWhereCameraIsWatching()
	{	

		rotatingPlayerToCamFromAssdLookPlayerToCam = true;
		GameObject go = new GameObject ();
		GameObject go2 = new GameObject ();

		go.transform.position = at.rotator.position;
		go2.transform.position = at.rotator.position;
		go.transform.rotation = at.rotator.rotation;
		go2.transform.rotation = at.rotator.rotation;

		go2.transform.SetParent (at.rotator.parent);
		go.transform.SetParent (go2.transform);

		GameObject temp4Arm = new GameObject ();
		temp4Arm.transform.SetParent (at.myArmor);
		temp4Arm.transform.position = at.rotator.position;
		temp4Arm.transform.rotation = at.rotator.rotation;


		GameObject armReferences = new GameObject ();
		armReferences.transform.SetParent(at.myArmor);
		armReferences.transform.position = at.myArm.position;
		armReferences.transform.rotation = at.myArm.transform.rotation;

		Vector3 retainRotX = new Vector3 (armReferences.transform.localEulerAngles.x, 0, 0);
		Quaternion qRetainRotX = Quaternion.Euler (retainRotX);


		if (armorChild) {
		} else {
			at.myCam.transform.SetParent (go.transform);
		}
		float mouseDragX = 0;
		float time = time2Return2Original;
		time = time *4;	
		float elapsedTime = 0;
		while (elapsedTime < time2Return2Original) {			

			at.myArm.transform.localRotation= Quaternion.Lerp (at.myArm.transform.localRotation, qRetainRotX, (elapsedTime / time2Return2Original));
			at.myArm2.transform.localRotation= Quaternion.Lerp (at.myArm2.transform.localRotation, qRetainRotX, (elapsedTime / time2Return2Original));

			temp4Arm.transform.rotation = at.myCam.transform.rotation;
			Vector3 desiredRot2 = new Vector3 (0, temp4Arm.transform.localEulerAngles.y, temp4Arm.transform.localEulerAngles.z);
			Quaternion it = Quaternion.Euler (desiredRot2);
			at.rotator.transform.localRotation = Quaternion.Slerp(at.rotator.localRotation, it, (elapsedTime / time2Return2Original));

			mouseDragX = Input.GetAxis ("Mouse X") * at.bC.rotationSpeed;
			go.transform.RotateAround (at.myBall.transform.position, at.myArmor.up, mouseDragX);


			elapsedTime += Time.deltaTime;

			at.aSSD.getYbasedonArmX ();
			yield return null;
		}

		Destroy (temp4Arm);
		Destroy (armReferences);

		//StartCoroutine (ReturnCamToOriginalRotAndPosNoModArm ());



		if (armorChild) {
			at.myCam.transform.SetParent (at.myArmor);
		} else {
			at.myCam.transform.SetParent (at.rotator);

		}
		//Attemp to retain Arm;
		//at.myArm.transform.SetParent (at.rotator);
		//at.myArm2.transform.SetParent (at.rotator);
		Destroy (go);
		Destroy (go2);
		rotatingPlayerToCamFromAssdLookPlayerToCam = false;
	}
	public IEnumerator RetunPlayerToWhereCameraIsWatching4FPS()
	{	

		rotatingPlayerToCamFromAssdLookPlayerToCam = true;
		GameObject go = new GameObject ();
		go.transform.position = at.myCam.transform.position;
		go.transform.rotation = at.myCam.transform.rotation;
		go.transform.SetParent (at.myCam.transform.parent);
		at.myCam.transform.SetParent (null);

		float mouseDragX = 0;
		float time = time2Return2Original;
		time = time *4;	
		float elapsedTime = 0;
		while (elapsedTime < time2Return2Original) {

			at.myCam.transform.position = go.transform.position;
			at.myCam.transform.rotation = go.transform.rotation;


			//Vector3 desiredRot2 = new Vector3 (0, at.myCam.transform.localEulerAngles.y, at.myCam.transform.localEulerAngles.z);
			Vector3 desiredRot2 = new Vector3 (0, at.myCam.transform.eulerAngles.y, at.myCam.transform.eulerAngles.z);
			Quaternion it = Quaternion.Euler (desiredRot2);


			//at.rotator.transform.localRotation= Quaternion.Slerp (at.rotator.transform.localRotation, it, (elapsedTime / time));
			at.rotator.transform.rotation = Quaternion.Slerp(at.rotator.rotation, it, (elapsedTime / time));

			///mouseDragX = Input.GetAxis ("Mouse X") * at.bC.rotationSpeed;
			//go.transform.RotateAround (at.myBall.transform.position, at.myArmor.up, mouseDragX);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		at.myCam.transform.SetParent (go.transform.parent);
		Destroy (go.gameObject);

		rotatingPlayerToCamFromAssdLookPlayerToCam = false;
		//print ("inusitado");
		//StartCoroutine (ReturnCamToOriginalRotAndPos ());
		if (armorChild) {
			//at.myCam.transform.SetParent (at.myArmor);
		} else {
			//at.myCam.transform.SetParent (at.rotator);

		}
		//rotatingPlayerToCamFromAssdLookPlayerToCam = false;

	}
	void TiltCamera(float amount)
	{
		Vector3 tmp = myCam.transform.localEulerAngles;
		tmp.x = amount;
		if (Input.GetMouseButtonDown (1)) 
		{
			rndm = myCam.transform.localEulerAngles.y;
		}
		if (Input.GetMouseButton (1)) {
			rndm += Input.GetAxis ("Mouse X") * camTiltSens/5;
			tmp.y = rndm;
		}
		myCam.transform.localEulerAngles = tmp;
	}
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
			angle += 360f;
		if(angle > 360f)
			angle-= 360f;
		return Mathf.Clamp(angle,min,max);
	}
	void AdjustCam()
	{
		Vector3 newPos = myCam.transform.localPosition;


		if (Input.GetKey(KeyCode.UpArrow))
		{
			newPos.y += .2f;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			newPos.y -= .2f;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			newPos.z += .2f;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			newPos.z -= .2f;
		}


		//newPos.y += y;
		//newPos.z += x;
		myCam.transform.localPosition = newPos;

			float y = Input.GetAxis ("Mouse Y");
			Vector3 myRot = myCam.transform.localEulerAngles;
			myRot.x -= y;
			myCam.transform.localEulerAngles = myRot;

	}
	void CloseTheDistance()
	{
		if (at.myArm.transform.localEulerAngles.x < 359 && at.myArm.transform.localEulerAngles.x > 260) {
			float totalAngles = (at.myArm.transform.localEulerAngles.x - 360) * -1;
			float relativeRotationToOne = totalAngles / 90;
			float relativeRotationInverse = (1 - relativeRotationToOne);
			float minDistance = -3f;
			float maxDistance = 8;
			float totalDistanceZ = maxDistance - minDistance;
			float interestedZ = relativeRotationInverse * totalDistanceZ;
			float reverse = (totalDistanceZ - interestedZ);
			float interestedY = relativeRotationInverse * initialCamHeight;
			Vector3 camPosRelativetoAngle = new Vector3 (0, interestedY-1, ((interestedZ * -1)- minDistance ));
			at.myCam.transform.localPosition = Vector3.Lerp (at.myCam.transform.localPosition, camPosRelativetoAngle, Time.deltaTime * 5);


			//Change Min Distance Respectiveley

			if (at.myArm.transform.localEulerAngles.x > 300) {

				Vector3 plusRot = new Vector3 (at.myArm.transform.localEulerAngles.x, at.myArm.transform.localEulerAngles.y, at.myArm.transform.localEulerAngles.z);
				Quaternion desiredRot = Quaternion.Euler (plusRot);
				at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, desiredRot, Time.deltaTime * 8);

				/*
				if (at.myArm.transform.localEulerAngles.x < 320) {

					Vector3 plusRot = new Vector3 (at.myArm.transform.localEulerAngles.x + 25, at.myArm.transform.localEulerAngles.y, at.myArm.transform.localEulerAngles.z);
					Quaternion desiredRot = Quaternion.Euler (plusRot);
					at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, desiredRot, Time.deltaTime * 8);
				} else {
					Vector3 plusRot = new Vector3 (at.myArm.transform.localEulerAngles.x, at.myArm.transform.localEulerAngles.y, at.myArm.transform.localEulerAngles.z);
					Quaternion desiredRot = Quaternion.Euler (plusRot);
					at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, desiredRot, Time.deltaTime * 8);
				}
				*/


			} else {
				Vector3 plusRot = new Vector3 (at.myArm.transform.localEulerAngles.x, at.myArm.transform.localEulerAngles.y, at.myArm.transform.localEulerAngles.z);
				Quaternion desiredRot = Quaternion.Euler (plusRot);
				at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, desiredRot, Time.deltaTime * 8);
			}

		} 
		if (at.myArm.transform.localEulerAngles.x <= 89 && at.myArm.transform.localEulerAngles.x >= 1) 				
		{
			
			float totalAngles = at.myArm.transform.localEulerAngles.x;
			float relativeRotationToOne = totalAngles / 90;
			float relativeRotationInverse = (1 - relativeRotationToOne);
			float minDistance =  -3f;
			//float minDistance = .01f;
			float maxDistance = 8;
			float totalDistanceZ = maxDistance - minDistance;
			float interestedZ = relativeRotationInverse * (totalDistanceZ *-1);
			float reverse = (totalDistanceZ - interestedZ-5);


			float interestedY = relativeRotationToOne * (initialCamHeight+3);

			Vector3 camPosRelativetoAngle = new Vector3 (0, interestedY, ((interestedZ) - minDistance));
			at.myCam.transform.localPosition = Vector3.Lerp (at.myCam.transform.localPosition, camPosRelativetoAngle, Time.deltaTime * rightclickLerpRate);


			//Make a Function instead of a partition
			if (at.myArm.transform.localEulerAngles.x >= 75) {
				Vector3 plusRot = new Vector3 (at.myArm.transform.localEulerAngles.x + 25, at.myArm.transform.localEulerAngles.y, at.myArm.transform.localEulerAngles.z);
				Quaternion desiredRot = Quaternion.Euler (plusRot);
				at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, desiredRot, Time.deltaTime * rightclickLerpRate);
			} else {
				Vector3 plusRot = new Vector3 (at.myArm.transform.localEulerAngles.x , at.myArm.transform.localEulerAngles.y, at.myArm.transform.localEulerAngles.z);
				Quaternion desiredRot = Quaternion.Euler (plusRot);
				at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, desiredRot, Time.deltaTime * rightclickLerpRate);
			}	
		}
	}

}
