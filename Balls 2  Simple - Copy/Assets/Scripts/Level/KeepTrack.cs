using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KeepTrack : MonoBehaviour {

	public Font abilitiesDescriptionFont;
	public Rigidbody firecracker;
	public GameObject fireCrackerChargedAbility;
	public GameObject fireCrackerContinuousAbility;
	public GameObject popcornMadnessAbility;
	public Rigidbody popcorn;







	public AudioClip machineGunSound;
	public AudioClip chargingSound;
	public AudioClip expelSound;
	public AudioClip abilitySelectionSound;


	public Transform LikeBullet;
	public Transform LikeBulletGravDestroyImpact;


	public Transform LikeBulletNoGrav;
	public Transform LikeBulletNoGravDestroyImpact;
	public Transform LikeBulletNoGravConstant;
	public Transform testBall;
	public Vector3 pointInQuestion;
	public float transferSubjectivityTime = 1;
	public GameObject FPSCanvas;
	public GeneralUIControll generealUIC;
	public GameObject currentFPSPlayer;
	public Transform kingCamDirector;
	public List<GameObject> allMyPlayers = new List<GameObject> ();
    public bool thereIsARTSCAm;
	public Camera RTSCam;
	public Transform RTSCamDirector;
	public Camera kingCamera;

	public GameObject CreatureHealthBar;
    public GameObject SelectionCirclePre;

	public int playerTeam;
	public Transform player;
	public Text notificationCenter;

	public Image BoomerangInfo;

	public Material ability1;
	public Material ability2;
	public Material ability3;
	public Material ability4;
	public Material ability5;
	public Material ability6;
	public Material ability7;
	public Material ability8;
	public Material ability10;


	public Font thingsFont;
	public Image ability1Cooldown;
	public Image ability2Cooldown;
	public Image ability3Cooldown;
	public Image ability4Cooldown;
	public Image ability5Cooldown;
	public Image ability6Cooldown;
	public Image ability7Cooldown;
	public Image ability8Cooldown;

	public Image myPowerBar;
	public Image myHealthBar;
	public Image myAngleBar;
	public Image myManaBar;

	public Text myTextHealth;
	public Text myTextPower;
	public Text myTextAngle;
	public Text myTextProyectile;
	public Text myHealthInText;
	public Text myManaInText;

	public Image slot1;
	public Image slot2;
	public Image slot3;
	public Image slot4;
	public Image slot5;
	public Image slot6;
	public Image slot7;
	public Image slot8;


	public Color normalColor = Color.blue;
	public Image MouseAim;
	public Image yAim;
	public Image xAndYAim;
	public Image CamaraTiltAsArm;


	public Sprite blankSprite;
	public Transform playerCamTransform;

	public Text currentShot;
	public Text curAmmo;
	public Text fireRate;

	public Image angleBar;
	public Text angleTxt;
	public Image healthBar;
	public Text healthTxt;

	public Image manaBar;
	public Text manaText;

	public GameObject worldCanvaz;


	public Image cd1;
	public Image cd2;
	public Image cd3;
	public Image cd4;
	public Image cd5;
	public Image cd6;
	public Image cd7;
	public Image cd8;

	public Image cdb1;
	public Image cdb2;
	public Image cdb3;
	public Image cdb4;
	public Image cdb5;
	public Image cdb6;
	public Image cdb7;
	public Image cdb8;

	public void OnEnable()
	{
		generealUIC = GetComponent<GeneralUIControll> ();
	}
	int keepTrackCycle =0;

	public void CycleToNextPlayer(GameObject from)
	{
		if (keepTrackCycle == 1) {
			allMyPlayers [0].GetComponent<PlayerHandle> ().TurnOnFPSStuff ();
			keepTrackCycle = 0;
			print ("a");
			return;
		}


		if (keepTrackCycle == 0) {
			allMyPlayers [1].GetComponent<PlayerHandle> ().TurnOnFPSStuff ();
			keepTrackCycle += 1;
			print ("b");
		
		}
	}
	
    public void NotifyOfRTSCamCreation()
    {
        thereIsARTSCAm = true;
		generealUIC.TurnOffFPSUI();

    }
    public void NotifyOfRTSCamDestruction()
    {
        thereIsARTSCAm = false;

		generealUIC.TurnOnFPSUI ();
	}

	public void RegisterAnotherPlayer(GameObject it)
	{
		if (allMyPlayers.Count == 0) {
		}
		allMyPlayers.Add (it);
		if (!kingCamera) {
			kingCamera = it.GetComponent<Attributes> ().myCam;
		} 
	}

	public void DeRegisterAnotherPlayer(GameObject it)
	{
		print (">>>>>>" + it);
		print (">>>>>>" + currentFPSPlayer);

		if (it == currentFPSPlayer) {
			//Do Here If currentPlayer Is destroyed<..
			allMyPlayers[0].GetComponent<PlayerHandle>().TransferFromRTSToFPS();
		}			
		print (allMyPlayers.Count);
		allMyPlayers.Remove (it);
		Destroy (it);
		print ("deregistering");
		print (allMyPlayers.Count);
		if (allMyPlayers.Count == 0) {
			Application.LoadLevel (Application.loadedLevel);
		} else {
			allMyPlayers[0].GetComponent<PlayerHandle>().TransferFromRTSToFPS();
		}
	}
	public void EstablishNewKingCam(Camera it)
	{
		kingCamera = it;

		//Also Establish Currrent FPS PLayer
		EstablishCurrentFPSPlayer (it.transform.root.gameObject);
	}
	public void EstablishCurrentFPSPlayer(GameObject it)
	{
		currentFPSPlayer = it;
		//generealUIC.TurnOnFPSCanvas ();
	}
	public void RemoveCurrentFPSPlayer()
	{
		currentFPSPlayer = null;
		//generealUIC.TurnOffFPSCanvas ();
	}
	void Update()
	{

		if (currentFPSPlayer) {
			SetCursorState ();
		}
	}
	void Start()
	{
		FixCoursor ();
	}

	CursorLockMode wantedMode;
	public void FixCoursor()
	{
		Cursor.lockState = wantedMode = CursorLockMode.Locked;
	}
	public void UnFixCoursor()
	{
		Cursor.lockState = wantedMode = CursorLockMode.None;
	}

	void SetCursorState ()
	{
		Cursor.lockState = wantedMode;
		Cursor.visible = (CursorLockMode.Locked != wantedMode);
	}
	public void GetPointInQuestion(Vector3 it)
	{
		pointInQuestion = it;
	}
}
