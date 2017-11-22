using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RtsHealthBar : MonoBehaviour {
	public Transform RTSCam;
	public AudioClip explosionSound;
	public GameObject explosionWhenDied;
	public Transform throwPoint;
	public float influenceRadius;
	public  Rigidbody rb;
	public Transform myAiArmor;
	public Transform myAiBall;
	public Transform myArm;
	public Transform myArmor;
	public float maxH = 100;
	float curH;
	public Image myHealthBar;
	public float initialXLocalScale;
	public int team = 2;
	public bool respawn = true;
	Vector3 initialPos;
	public GameObject eventManger;
	public Vector3 lclSale  = new Vector3 (.1f, .1f, .1f);
	public GameObject actualCreture;
	public GameObject HB;
	public GameObject actualHealthBar;
	public float healthBarHeight = 2;
	Attributes at;

	public Transform hBObj;


	void OnEnable()
	{
		at = transform.root.GetComponent<Attributes> ();
		at.MrRtsHealthBarMakingHimselfKnown (this);
		CreateHealthBar ();	
	}

	void Update()
	{
		/*
		if (GetComponent<LookAtPlayerCam> ()) {
		
			if (GetComponent<LookAtPlayerCam> ().playerCam == null) {
				GetComponent<LookAtPlayerCam> ().playerCam = at.kT.kingCamera.transform;
				print ("ssss look");
			}
		} else {
			print ("no look");
		}
		*/	
	}
	void LateUpdate()
	{
		if (at.rtsHB && at.kT.kingCamera) {
			at.rtsHBTransform.LookAt(at.kT.kingCamera.transform);

			if (at.kT.RTSCamDirector) {
				//kING CAM
				Vector3 calib = new Vector3 (360- at.rtsHBTransform.eulerAngles.x, at.kT.RTSCamDirector.transform.eulerAngles.y, at.kingCamera.transform.eulerAngles.z); 
				at.rtsHBTransform.transform.eulerAngles = calib;
			}
		}
	}
	public void CreateHealthBar()
	{
		if (at.rtsHBTransform == null) {
			//print ("Creating Health baar" + Time.time);
			if (!eventManger) {
				eventManger = GameObject.Find ("EventManager");
			}
			HB = GameObject.Instantiate (eventManger.GetComponent<KeepTrack> ().CreatureHealthBar.gameObject);
			//hBObj = HB.transform;
			HB.transform.position = at.myArmor.transform.position;
			HB.transform.SetParent (at.myArmor.transform);
			HB.transform.localPosition = new Vector3 (0, healthBarHeight, 0);
			actualHealthBar = HB.GetComponent<LookAtPlayerCam> ().actualHealthBar;
			initialXLocalScale = actualHealthBar.transform.localScale.x;
			at.AssignHBObj (HB.transform);
			//at.GetKingKamTransform (HB.transform);
			//at.rtsHBTransform = HB.transform;
			//at.kT.EstablishingKingCam (at.myCam);
			//RTSCam = at.kT.kingCamera.transform;

			myHealthBar = HB.GetComponent<LookAtPlayerCam> ().actualHealthBar.GetComponent<Image> ();
			TakeDamage (0);
		}
	}
	public void EstablishRTSCam(Transform it)
	{
		//print ("XXXXXXXXXX");
		HB.GetComponent<LookAtPlayerCam> ().EstablishPlayerCam (at.kT.RTSCam.transform);
		myHealthBar = HB.GetComponent<LookAtPlayerCam> ().actualHealthBar.GetComponent<Image>();
		TakeDamage (0);
	}
	public void TakeDamage(float amount)
	{
		maxH = at.maxHealth;
		curH = at.currentHealth;
		float calculateHealth = (curH / maxH) * initialXLocalScale;
		if (actualHealthBar)
			{
				//print ("YES8888");

			}
		actualHealthBar.transform.localScale = new Vector3 (calculateHealth, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
		if (amount != 0) {
			HealthActionNotification (amount, 1);
		}
	}
	public void HealthActionNotification(float amount, int it)
	{
		GameObject go = new GameObject ("Cool GO by AI");
		go.transform.localScale	= lclSale;
		go.transform.parent = eventManger.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;
		go.transform.position = at.myBall.transform.position;
		//go.AddComponent<LookAtPlayerCam>();
		//go.GetComponent<LookAtPlayerCam> ().EstablishPlayerCam (at.kT.RTSCam.transform);
		go.AddComponent<CanvasRenderer>();
		go.AddComponent<Text> ();
		if (it == 1) {
			go.GetComponent<Text> ().color = Color.red;
		}
		if (it == 2) {
			go.GetComponent<Text> ().color = Color.green;
		}
		go.GetComponent<Text> ().fontSize = 20;
		go.GetComponent<Text> ().font = eventManger.GetComponent<KeepTrack> ().thingsFont;
		go.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		amount = Mathf.Round (amount);
		go.GetComponent<Text> ().text = amount.ToString();
		go.AddComponent<TextNotificationFloat> ();
		go.AddComponent<TextNotificationFloat> ().doDouble = true;
		go.GetComponent<TextNotificationFloat> ().playerCam = eventManger.GetComponent<KeepTrack> ().playerCamTransform;
		go.GetComponent<TextNotificationFloat> ().DestroyText ();
		go.GetComponent<TextNotificationFloat> ().rotationReference = at.myArmor;
		go.GetComponent<TextNotificationFloat> ().EstablishPlayerCam (at.kT.kingCamera.transform);
	}

	public void AssignRTSCam()
	{
		print ("got a new cam called" + transform.root.name);
		print (transform.root);
		print (at.kT.kingCamera.transform.name);
		RTSCam = at.kT.kingCamera.transform;
		print (hBObj);
		//print (
	}

	public void DestroyHealthBar()
	{
		if (HB) {
			Destroy (HB);		
		}
	}
}
