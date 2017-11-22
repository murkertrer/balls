using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

	public Color commanderColor;
	public Color controllerColor;
	public Color controllerAndCommanderColor;

	public Transform aimObjCommander;
	public GameObject radarShow;

	public bool iAmFPSPlayer;
	public bool useCameraCardinality = true;
	public bool gimBall;
	public bool useAimTraze = true;
	public LineRenderer aimTraze;

	public AudioClip soundWhenShoot;
	public AudioClip soundWhenExplode;
	public AudioClip soundWhenProyectileTrace;
	public bool rotateCamWithPlayer = true; 
	public float rotationSpeed = .5f;
	public bool rotateCamWhenJumpingEvenIfArmorChild = true;

	public Vector3 originalArm1LocalPos;
	public Vector3 originalArm2LocalPos;

	public bool returnToCamView = true;
	public bool returnToPlayerView ;

	public bool slideToPoint;
	public bool commandingUnits;
	public bool comandoMode;
	public bool subordination;
	public bool isSelected = true;

	public Transform aimObjForCam;
	public UiCoordination UIC;
	public AudioListener aL;
	public RtsHealthBar rtsHB;
	public Transform rtsHBTransform;
	public GameObject selectionCircleObj;
    public BallControl bC;
    public AimingSystemSimpleDouble aSSD;
	public GenericThrowControl gTC;
	public ComanderScript cs;
	public Powers pW;
	public Behave bH;
	public RtsMovement rtsM;
	public SpaceManipulation sM;
	public AbilitySelection aS;

	public Camera kingCamera;
	public Transform myRTSCamTransform;
	public Camera myRTSCam;
	public bool inRTS;



	public Transform CameraDummy;
	public Transform anotherAxis;
	public Transform rotatorDummy;
	public Transform aimObjRightArmFar;
	public Transform shieldCarry;
	public CameraControl cc;
	public Vector3 lastKnownCamRotPos;

	public ParticleSystem ThrustUp;
	public ParticleSystem ThrustFront;
	public ParticleSystem ThrustBack;
	public ParticleSystem ThrustRight;
	public ParticleSystem ThrustLeft;
	public LineRenderer aimLineRenderer;
	public LineRenderer aimLineRenderer2;
    public LineRenderer movementRenderer;
	public LineRenderer angleLineRenderer;



    public int team =1;
	public PlayerHandle pH;

	public GameObject aimObj;
	public GameObject centeredAimObj;
	public GameObject cameraCardianlity;

	public Camera myCam;


	public Transform abilitiesCollection;
	public Transform aimObjR1;
	public Transform aimObjR2;
	public Transform aimObjL1;
	public Transform aimObjL2;
	public Transform relativePlane;
	public Transform forwardReference;
	public Transform zoneOfDetectR;
	public Transform zoneOfDetectL;
	public Transform actualRadar;
	public Transform myBall;
	public Transform rotator;
    public Transform rotatorOne;
	public Transform myArmor;
	public Transform myArm;
	public Transform myArmGhost;
	public Transform shieldRotateDummy;
	public Transform shieldCarryDummy;
	public Transform ballDummy;
	public Transform ballDummyHack;
	public Transform radarCardinality;
	public Transform myArm2;
	public Transform myThrowPoint2;
	public Transform shieldRotate;
	public Transform myThrowPoint;
	public ParticleSystem throwPointParticle;


	public KeyCode shifter = KeyCode.LeftShift;
	public KeyCode controller = KeyCode.LeftControl;
	public KeyCode abilityDelete = KeyCode.T;
	public KeyCode timeManipulation = KeyCode.Q;
	public KeyCode camControl = KeyCode.G;
	public KeyCode powers = KeyCode.E;
	public KeyCode usePowers = KeyCode.CapsLock;
	public KeyCode radarKey = KeyCode.R;
	public KeyCode breaks = KeyCode.F;
	public KeyCode commander = KeyCode.CapsLock;
	public KeyCode cycle = KeyCode.Tab;
	public KeyCode gimballerSwitch = KeyCode.C;
	public KeyCode change2ArmorChild = KeyCode.V;

	public float currentHealth;
	public float maxHealth;
	public float currentMana;
	public float maxMana;
	public float manaRegenRate = .5f;

	public Transform explosionRadiusShower;
	public KeepTrack kT;
	public Quaternion lastKnownCamRotRot;
	//public Vector3 initialCamPos;
	public Quaternion initialCamRot;
	public Transform myCurrentShot;
	public Transform ballGravityExplosionShot;
	public Transform ballShotStraight;
	public Transform ballTrazer;
	public Transform ballRegular;
	public Transform likeBullet;
	public Transform impactBall;
	public Transform kinematicBomb;
	public Transform planetMaker;

	public Rigidbody firecracker;

	void OnEnable()
	{
        bC = GetComponent<BallControl>();
		pH = GetComponent<PlayerHandle> ();
        aSSD = GetComponent<AimingSystemSimpleDouble>();
		gTC = GetComponent<GenericThrowControl> ();
		UIC = GetComponent<UiCoordination> ();
		cs = GetComponent<ComanderScript> ();
		pW = GetComponent<Powers> ();
		cc = GetComponent<CameraControl> ();
		aL = myCam.GetComponent<AudioListener> ();
		bH = GetComponent<Behave> ();
		rtsM = GetComponent<RtsMovement> ();
		sM = GetComponent<SpaceManipulation> ();
		aS = GetComponent<AbilitySelection>();

		currentHealth = maxHealth;
		currentMana = maxMana;
		initialCamRot = myCam.transform.rotation;
       
		GameObject go = GameObject.Find("EventManager");
		kT = go.GetComponent<KeepTrack> ();
		kT.player = this.transform;
		kT.playerCamTransform = myCam.transform;
		kT.playerTeam = team;
		kT.RegisterAnotherPlayer (gameObject);

		if (kT.currentFPSPlayer == null && inRTS == false) {		
			kT.EstablishCurrentFPSPlayer (gameObject);
		}

		originalArm1LocalPos = myArm.localPosition;
		originalArm2LocalPos = myArm2.localPosition;

		CheckIfIAmFpsPlayer ();

		if (kT.currentFPSPlayer == gameObject) {
		} else {

			aimObjCommander.gameObject.SetActive (false);
		}
			
	}

	public void CreateRTS()
	{
		if (!gameObject.GetComponent<RtsHealthBar> ())
		{
			gameObject.AddComponent<RtsHealthBar> ();
			//gameObject.AddComponent<RtsHealthBar> ().CreateHealthBar();
		}
	}

	void Update()
	{
		RegenerateMana ();
		CheckIfIAmFpsPlayer ();
	}

	void CheckIfIAmFpsPlayer()
	{
		if (kT.currentFPSPlayer == gameObject) {
			iAmFPSPlayer = true;
		} else {
			iAmFPSPlayer = false;
		}	


		if (isSelected) {
	
		}
	}


	void RegenerateMana ()
	{
		if (currentMana < maxMana) {
			currentMana += manaRegenRate;
		}
	}
	public void TakeDamage(float amount)
	{
		currentHealth -= amount;
		HealthManage ();

		if (!inRTS) {
			this.GetComponent<UiCoordination> ().HealthActionNotification (amount, 1);
		}

		if (rtsHB) {
			rtsHB.TakeDamage (amount);
		}
	
	}

	public void AddHealth(float amount)
	{
		currentHealth += amount;
		HealthManage ();
		this.GetComponent<UiCoordination> ().HealthActionNotification (amount, 2);

	}

	public void ResToreHealth()
	{
		currentHealth = maxHealth;
		this.GetComponent<UiCoordination> ().UpdateHealthUI ();

	}
	void HealthManage()
	{
		if (currentHealth < 0) {
			print ("health below 0");

			if (kT.allMyPlayers.Count > 1)
			{
				kT.DeRegisterAnotherPlayer (transform.root.gameObject);
			}
			if (kT.allMyPlayers.Count == 1)
			{
				kT.DeRegisterAnotherPlayer (transform.root.gameObject);
			}

			//this.GetComponent<PlayerHandle> ().Respawn2 ();
			//currentHealth = maxHealth;
		}
		if (currentMana > maxHealth) {
			currentHealth = maxHealth;
		}
	}
	public void UseMana(float ammount)
	{
		currentMana -= ammount;
	}
	public void GetSelectionCircle(GameObject it)
	{
		selectionCircleObj = it;

	}
	public void DestroySelectionCircle()
	{
		Destroy (selectionCircleObj);
	}
	public void AssingRtsCam(Transform it)
	{
		myRTSCamTransform = it;
		myRTSCam = myRTSCamTransform.GetComponent<Camera> ();
		rtsHB = GetComponent<RtsHealthBar>();
	}
	public void MrRtsHealthBarMakingHimselfKnown(RtsHealthBar it)
	{
		rtsHB = it;
	}

	public void AssignHBObj( Transform it)
	{
		rtsHBTransform = it;
	}

	public void TurnOffCamAndAudio()
	{
		aL.enabled = false;
		myCam.enabled = false;
		
	}


	public void InRTS()
	{
		inRTS = true;
	}
	public void OutRTS()
	{
		inRTS = false;
		//
		if (GetComponent<RtsHealthBar> ()) {
			Destroy(GetComponent<RtsHealthBar>());
		}	
		if (rtsHBTransform) {
			Destroy (rtsHBTransform.gameObject);
		}
		rtsHBTransform = null;
	}
	public void Select()
	{
		isSelected = true;
		//subordination = true;
	}

	public void DeSelect()
	{

		isSelected = false;
		//subordination = false;
	}

	public void AddUnitSelectionComponentToBall()
	{
		//myBall.gameObject.AddComponent(
	}

	public void ToggleCommandoOn()
	{
		//comandoMode = !comandoMode;
		comandoMode = true;
	}
	public void ToggleCommandoOff()
	{
		//comandoMode = !comandoMode;
		comandoMode = false;
	}
	public void Commanding ()
	{
		commandingUnits = true;
	}
	public void NotCommanding()
	{
		commandingUnits = false;
	}

	public void DeActivateGimball()
	{

	}
	public void GimballSwitcher()
	{
		gimBall = !gimBall;

		if (!gimBall) {
		
			//cc.ReturnCamToOriginalRotAndPos ();
			//cc.ReturnCamToOriginalRotAndPosNoModArm ();
			cc.ReturnFromGimBall();
			if (!Input.GetKey (shifter)) {
				aimObj.SetActive (false);	
			}
		}
	}
}
