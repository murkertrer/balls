using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHandle : MonoBehaviour {
	GeneralUIControll generalUIC;



	public bool aimObjLookCam;
	public bool aimObjLookWorld;
	public bool closeAims;
	public bool farAims;
	public bool R = true;
	public bool L;
	public bool isFlying;
	public bool isSelected = true;
	public bool fpsControl = true;
	public bool controllingComanded = false;




	public bool rtsControl;
	public bool isInFirstPerson;
	Quaternion initialRotArmor;
	Quaternion initialRot;
	Vector3 initialPos;
	Vector3 initialBallPos;
	public float minAllowedMinusYPos = -5;
	CursorLockMode wantedMode;
	Transform myBall;
	bool coursorState = false;
	Attributes at;
	public GameObject aimCloseR;
	public GameObject aimCloseL;
	public GameObject aimFarR;
	public GameObject aimFarL;
	bool pause;
	bool camFPS;
	public bool respawnLoadingLevel = true;
	public bool currentlyRotating;
    public float RtsCamOfset = 40;

	void OnEnable()
	{


		at = GetComponent<Attributes> ();
        //****
        //gameObject.AddComponent<RtsMovement>();

		if (!gameObject.GetComponent<ComanderScript> ()) {
			//gameObject.AddComponent<ComanderScript> ();
		}
		generalUIC = GetComponent<GeneralUIControll> ();
    }
	/*
	public void ActivateArm(bool r, bool l)
	{
		if (r) {
			armR.gameObject.SetActive (true);
			R = true;
		}
		if (l) {
			armL.gameObject.SetActive (true);
			L = true;
		}
	}
	public void DeActivateArm(bool r, bool l)
	{
		if (r) {
			armR.gameObject.SetActive (false);
			R = false;
		}
		if (l) {
			armL.gameObject.SetActive (false);
			L = false;
		}
 

	 */


	void Start()
	{
		myBall = this.GetComponent<Attributes> ().myBall;
		initialPos = this.transform.position;
		initialRot = myBall.transform.rotation;
		initialRotArmor = this.GetComponent<Attributes> ().myArmor.rotation;
		ActivateCrossAims ();

		if (at.kT.currentFPSPlayer == null) {
			print ("ph says null at kt, so fix");
			FixCoursor ();
		}

		if (!R) {
			at.myArm.gameObject.SetActive (false);
		}
		if (!L) {
			at.myArm2.gameObject.SetActive (false);
		}


	}
	void Update () {
		
		if (myBall.transform.position.y < minAllowedMinusYPos) {
			at.kT.DeRegisterAnotherPlayer (transform.root.gameObject);

			//Notify String according to Players Official Name In attributes?
			GetComponent<UiCoordination> ().NotifyPlayer ("You fell");
		}


		if (at.isSelected) {


			if (at.kT.currentFPSPlayer == gameObject) {}

			if (at.iAmFPSPlayer) {
				CheckCam ();
				CheckIfRTS ();
				CheckIfPause ();
				CheckIfEsc ();
				CheckIfComandoMode ();
				CheckIfControlledOverCommanded ();
				CheckIfCycle ();
				CheckIfGimball ();
			}			
		}
	}

	public void CheckIfGimball ()
	{
		if (Input.GetKeyDown (at.gimballerSwitch)) {
			print ("click one");

			at.GimballSwitcher();
		}
	}

	public void ActivateCrossAims()
	{
		if (closeAims) {	
			at.aimObjL1.gameObject.SetActive (true);
			at.aimObjR1.gameObject.SetActive (true);
		}
		if (farAims) {
			at.aimObjL2.gameObject.SetActive (true);
			at.aimObjR2.gameObject.SetActive (true);
		}
	}

	void CheckIfCycle ()
	{
		if (Input.GetKeyDown (at.cycle)) {
			TransferSubjectivity ();
		}	
	}

	void TransferSubjectivity()
	{
		if (at.kT.allMyPlayers.Count > 1) {

			if(!GetComponent<RtsHealthBar>())
			{
				//GetComponent<RtsHealthBar> ().DestroyHealthBar ();
				gameObject.AddComponent<RtsHealthBar>();
			}
			//Destroy(GetComponent<ComanderScript>());
			TurnOffSimpleFpsStuff ();
			GameObject go = new GameObject ();
			go.name = "TransferEntity" + this.gameObject.name;
			go.AddComponent<Camera> ();
			at.kT.EstablishNewKingCam (go.GetComponent<Camera>());
			go.transform.position = at.myCam.transform.position;
			go.transform.rotation = at.myCam.transform.rotation;
			int myNumber = at.kT.allMyPlayers.IndexOf (gameObject);
			if (myNumber + 1 == at.kT.allMyPlayers.Count) {
				//print ("im last one, should go back to first");
				StartCoroutine(TransferSubjectivity(go, at.kT.allMyPlayers[0].GetComponent<Attributes>().myCam.transform));
			} else {
				StartCoroutine(TransferSubjectivity(go, at.kT.allMyPlayers[myNumber+1].GetComponent<Attributes>().myCam.transform));
				//print ("can still go on");
			}
		} else {
			//print ("only me on stage");
		}	
	}

	IEnumerator TransferSubjectivity(GameObject entity, Transform adoptedEntity)
	{
		bool firstSwtich = false;
		float time = at.kT.transferSubjectivityTime;
		float elapsedTime = 0;
		while (elapsedTime < time) {
			entity.transform.position= Vector3.Lerp(entity.transform.position, adoptedEntity.position, (elapsedTime/time));
			entity.transform.rotation= Quaternion.Lerp(entity.transform.rotation, adoptedEntity.rotation, (elapsedTime/time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		adoptedEntity.root.GetComponent<PlayerHandle> ().TurnOnFPSStuff ();
		at.kT.EstablishNewKingCam(adoptedEntity.root.GetComponent<Attributes> ().myCam);
		adoptedEntity.root.GetComponent<PlayerHandle> ().AddCommander ();

		// Only Deselct If not on comando List;
		at.DeSelect ();
		adoptedEntity.root.GetComponent<Attributes> ().Select ();
		//Select Player 


		Destroy (entity);
		yield return null;

	}
	void AddCommander ()
	{
		gameObject.AddComponent<ComanderScript> ();
	}

	public void TurnOffSimpleFpsStuff()
	{
		at.isSelected = false;
		at.myCam.enabled = false;
		at.bC.enabled = false;
		at.aSSD.enabled = false;
		at.UIC.enabled = false;

	}
	void CheckIfComandoMode()
	{
		/*
		if (Input.GetKeyDown (at.commander)) {
			at.aimObj.SetActive (true);
		}
		if (Input.GetKeyUp (at.commander)) {	
			at.aimObj.SetActive (false);
			at.aSSD.ResetAim ();
		}

		if (Input.GetKey (at.commander)) {
			at.aSSD.MoveAimObj ();
			at.cs.CommandoSelect ();
		}
		/*/

	}


	void CheckIfControlledOverCommanded()
	{
		/*
		if (at.cs.commandoSelectedObjects.Count > 0) {

			print ("calling controller");
			if (Input.GetKey (at.controller)) {
				at.aSSD.MoveAimObj ();
				//at.cs.CommandoLookAt ();
			}
			if (Input.GetKeyDown(at.controller))
			{
				at.aimObj.SetActive (true);
			}

			if (Input.GetKeyUp(at.controller))
			{
				at.aimObj.SetActive (false);
			}
		}
		*/
	}

	void CheckCam()
	{		
		if (Input.GetKeyDown(KeyCode.J))
		{
			camFPS = !camFPS;
            /*
			if (camFPS) {
				aimCloseR.SetActive (false);
				aimCloseL.SetActive (false);
				aimFarR.SetActive (false);
				aimFarL.SetActive (false);
			}	*/

                if (!camFPS)
                {
                    
                }
            }
        if (camFPS) {
			//at.myCam.transform.position = at.myArmor.transform.position;

			/*
			Vector3 mimicArm = at.myArm.transform.localEulerAngles;
			mimicArm.y = 0;
			mimicArm.z = 0;
			at.myCam.transform.localEulerAngles = mimicArm;
		*/

			//at.myCam.transform.localEulerAngles = at.myArm.transform.localEulerAngles;
		}
	}

    void CheckIfRTS()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!at.kT.thereIsARTSCAm)
            {
                StartRTS();
                at.kT.NotifyOfRTSCamCreation();
            }
           
        }
    }
    //Make Function To notify at.kt of RTS DEstruction
    void StartRTS()
    {
        TurnOffFPSStuff();
        TurnOnRTSStuff();
    }

	public void TurnOffFPSStuff()
	{
		at.myCam.enabled = false;
		at.bC.enabled = false;
		at.aSSD.enabled = false;
		at.UIC.enabled = false;
		at.kT.RemoveCurrentFPSPlayer ();
		at.kT.gameObject.GetComponent<GeneralUIControll>().TurnOffFPSCanvas ();
	}
    public void TurnOnRTSStuff()
    {
		UnFixCoursor();
		gameObject.GetComponent<CameraControl>().enabled = false;
		at.InRTS ();
        GameObject go = new GameObject();
        go.name = "RTS CAM by" + this.gameObject.name;
        go.AddComponent<RtsConditions>();
        Vector3 newPosRelative = new Vector3(at.myBall.position.x, at.myBall.position.y + RtsCamOfset, at.myBall.position.z - RtsCamOfset);
        go.GetComponent<RtsConditions>().SetOrientationFromLastPlayer(at.myBall, newPosRelative, at.myCam.GetComponent<FollowCamScript>().transparent);
        GameObject go2 = new GameObject();
        go2.name = "camDirector";
		go2.transform.SetParent (go.transform);
		at.kT.RTSCamDirector = go2.transform 	;
		go2.AddComponent<RtsCamDirector> ();
		//go2.AddComponent<RtsCamDirector>();
        go.GetComponent<RtsConditions>().SetCamDir(go2.transform);
        go.GetComponent<RtsConditions>().SelectedObjectsWhenSpawn(this.GetComponent<SelectableUnitComponent>());
        if (!at.myBall.gameObject.GetComponent<SelectableUnitComponent>())
        {
            at.myBall.gameObject.AddComponent<SelectableUnitComponent>();
        }
        gameObject.AddComponent<RtsAiming>();
        gameObject.AddComponent<RtsThrowControl>();
		at.CreateRTS ();

    }
	public void TransferFromRTSToFPS()
	{
		TurnOnFPSStuff ();
		if (at.rtsHBTransform)
		{
			Destroy (at.rtsHBTransform.gameObject);
		}
	}

    public void TurnOnFPSStuff()
    {
		at.kT.gameObject.GetComponent<GeneralUIControll>().TurnOnFPSCanvas ();
		at.kT.EstablishCurrentFPSPlayer (gameObject);
		at.kT.EstablishNewKingCam (at.myCam);
		at.kT.NotifyOfRTSCamDestruction();    


		at.UIC.enabled = true;
		at.isSelected = true;
		at.OutRTS ();
    
        gameObject.GetComponent<CameraControl>().enabled = true;
        at.myCam.enabled = true;
        at.bC.enabled = true;
        at.aSSD.enabled = true;
        FixCoursor();
        Destroy(GetComponent<RtsThrowControl>());
        Destroy(GetComponent<RtsAiming>());
		Destroy (gameObject.GetComponent<RtsHealthBar> ());
    }
	void CheckIfPause()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			pause = !pause;
			if (pause) {				
				at.kT.generealUIC.Notification ("Game Pause  *p*" );
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
				at.kT.generealUIC.Notification ("Game Unpaused  *p*" );
			}
		}
	}


	void CheckIfEsc()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (wantedMode == Cursor.lockState) {
				print ("fixing curosr cus esc");
				FixCoursor ();
			} else {
				UnFixCoursor ();
			}
		}
	}
	public void Respawn()
	{
		Respawn2 ();
		this.transform.position = initialPos;
		myBall.GetComponent<Rigidbody> ().isKinematic = true;
		myBall.GetComponent<Rigidbody> ().isKinematic = false;
		myBall.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		myBall.transform.position = initialPos;
		myBall.transform.rotation = initialRot;
		this.GetComponent<Attributes> ().myArmor.rotation = initialRotArmor;
		this.GetComponent<Attributes> ().ResToreHealth ();
	}
	public void Respawn2()
	{
		if (respawnLoadingLevel) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
	public void FixCoursor()
	{
		at.kT.FixCoursor();
	}
	public void UnFixCoursor()
	{
		print ("Unfixed");
		at.kT.UnFixCoursor();
	}
	void FixedUpdate()
	{
		AimObjLookAtCam ();
	}
	void  AimObjLookAtCam()
	{		
		if (aimObjLookCam) {
			if (closeAims) {
				at.aimObjL1.LookAt (at.myCam.transform);
				at.aimObjR1.LookAt (at.myCam.transform);			
			}
		}
	}
	void SetCursorState ()
	{
		//Cursor.lockState = wantedMode;
		//Cursor.visible = (CursorLockMode.Locked != wantedMode);
	}
	public void ExitedArtificialGravity()
	{
		print ("exited");
		//StartCoroutine (RotateToFitWorld (2));
		StartCoroutine(GetComponent<SpaceManipulation>().RotateToFitWorld(1));
	}
}
	