using UnityEngine;
using System.Collections;

public class PlayerFly : MonoBehaviour {

	Rigidbody rb;
	Transform wings;
	Attributes at;
	PlayerHandle ph;
	KeyCode shifter;
	public float maxFwd=40;
	public float minFwd = 7;
	public float fwdScale=.3f;
	public float currentFwdThrust;
	float multiply = 20.0f;
	float forwardInput = 0.00f;
	float pitchInput = 0.00f;
	public float pitchSpeed = 20;
	float rollInput = 0.00f;
	float yawInput = 0.00f;

	//Aim;
	public Camera myCam;
	public Transform myArm;
	public GameObject aimObj;
	Quaternion origianlRot;

	public bool mouseControl;


	void LateUpdate()
	{		at.myArmor.transform.position = at.myBall.transform.position;
		
	}
	void OnEnable()
	{
		ph = this.GetComponent<PlayerHandle> ();
		ph.isFlying = true;
		at = this.GetComponent<Attributes> ();
		rb = at.myBall.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		if (wings) {
			wings.gameObject.SetActive (true);
		}
		currentFwdThrust = (maxFwd + minFwd) / 2;
		shifter = at.shifter;
		at.myCam.transform.transform.LookAt (at.myBall);


		//at.rotator.Rotate(pitchInput, yawInput, -rollInput);

	}

	void FixedUpdate (){
		if (Input.GetKeyDown(at.shifter))
		{
			yawInput = 0;
		}
		if (!Input.GetKey(at.shifter))
		{
			if (mouseControl) {
				FlightControlMouse ();
			} else {
				//FlightControlKey ();
				Hybrid();
			}
		}

	}
	void Hybrid()
	{
		pitchInput += Input.GetAxis ("Mouse Y") * -.5f;
		if (Input.GetKey (KeyCode.D)) {rollInput += fwdScale*5;}
		if (Input.GetKey (KeyCode.A)) {rollInput -= fwdScale*5;}
		yawInput = Input.GetAxis ("Mouse X") * .1f;
		//currentFwdThrust += Input.GetAxis ("Mouse ScrollWheel");
		if (Input.GetKey (KeyCode.W)) {currentFwdThrust += fwdScale;}
		if (Input.GetKey (KeyCode.S)) {currentFwdThrust -= fwdScale;}
		ApplyMotion ();

	}

	void ApplyMotion()
	{
		yawInput = Mathf.Clamp (yawInput, -1, 1);
		currentFwdThrust = Mathf.Clamp (currentFwdThrust, minFwd, maxFwd);
		rollInput *= Time.deltaTime * multiply;
		pitchInput *= Time.deltaTime * multiply;
		//at.myArmor.Rotate (0, yawInput, -rollInput);
		//at.myArmor.Rotate (pitchInput, 0, 0);
		rb.velocity = at.rotator.forward * currentFwdThrust;
		//at.myArmor.Rotate (pitchInput, yawInput, -rollInput);
		//at.myArmor.transform.localPosition = at.myBall.transform.position;
		at.rotator.Rotate(pitchInput, yawInput, -rollInput);

		//at.myArmor.Rotate (pitchInput, 0, 0);

	}

	void FlightControlKey()
	{
		if (Input.GetKey (KeyCode.D)) {rollInput += fwdScale*5;}
		if (Input.GetKey (KeyCode.A)) {rollInput -= fwdScale*5;}
		if (Input.GetKey (KeyCode.S)) {pitchInput += fwdScale*5;}
		if (Input.GetKey (KeyCode.W)) {pitchInput -= fwdScale*5;}
		yawInput = Input.GetAxis ("Mouse X") * .5f;
		currentFwdThrust += Input.GetAxis ("Mouse ScrollWheel");
	}
	void FlightControlMouse()
	{
		if (!Input.GetKey (at.shifter)) {
			if (Input.GetKey (KeyCode.W)) {currentFwdThrust += fwdScale;}
			if (Input.GetKey (KeyCode.S)) {currentFwdThrust -= fwdScale;}
			pitchInput = Input.GetAxis ("Mouse Y") * -15;
			rollInput = Input.GetAxis ("Mouse X") * 15;

			if (Input.GetMouseButton (1)) {
				yawInput = Input.GetAxis ("Mouse X") * 15;
			}
			if (Input.GetMouseButtonUp (1)) {
				yawInput = 0;
			}

			at.myArmor.Rotate (0, yawInput, -rollInput);
			at.myArmor.Rotate (pitchInput, 0, 0);
		}
	}




}
	