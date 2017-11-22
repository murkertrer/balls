using UnityEngine;
using System.Collections;

public class FlightAim : MonoBehaviour {
	Attributes at;
	public float y;
	public float x;

	public float x2;
	public float y2;
	KeyCode shifter;
	KeyCode powerKey;
	//Aim;
	public Camera myCam;
	public Transform myArm;
	public GameObject aimObj;
	LineRenderer aimLineRenderer;
	LineRenderer aimLineRenderer2;

	public float yMinLimit = -90;
	public float yMaxLimit = 0;

	public bool onlyRight;
	public bool onlyLeft;
	bool lerpedRight;
	bool lerpedLeft;
	public float widthOfDoubleShoot;

	Quaternion  origRotR;
	Quaternion  origRotL;

	Quaternion origianlRot;	
	void OnEnable () {
		at = this.GetComponent<Attributes> ();
		myArm = at.myArm;
		aimObj = at.aimObj;
		myCam = at.myCam;
		origianlRot = myCam.transform.localRotation;
		aimLineRenderer = at.aimLineRenderer;
		aimLineRenderer2 = at.aimLineRenderer2;
		shifter = at.shifter;
		powerKey = at.usePowers;
		ResetAim ();
		Quaternion origRotR = at.myArm.transform.localRotation;
		Quaternion origRotL = at.myArm2.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.T))
		{
			StartCoroutine (LookBack (1));
		}
	
	}
	IEnumerator LookBack(float time)
	{
		Vector3 inFront =myCam.transform.forward *5;
		Vector3 prePos = myCam.transform.localPosition;
		Quaternion newRot = myCam.transform.localRotation* Quaternion.AngleAxis (180, myCam.transform.up);
		Quaternion oldRot = myCam.transform.localRotation;

		float elapsedTime = 0;
		while (elapsedTime < time) {
			//myCam.transform.localRotation= Quaternion.Slerp (myCam.transform.localRotation, origianlRot, (elapsedTime / time));
			myCam.transform.localPosition = Vector3.Slerp(myCam.transform.position, inFront, (elapsedTime/time));
			myCam.transform.localRotation = Quaternion.Slerp(myCam.transform.transform.localRotation, newRot, (elapsedTime/time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds (2);

		float elapsedTime2 = 0;
		while (elapsedTime2 < time) {
			//myCam.transform.localRotation= Quaternion.Slerp (myCam.transform.localRotation, origianlRot, (elapsedTime / time));
			myCam.transform.localRotation = Quaternion.Slerp(myCam.transform.transform.localRotation, oldRot, (elapsedTime/time));
			myCam.transform.localPosition= Vector3.Slerp(myCam.transform.position, prePos, (elapsedTime2/time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		print ("finished");
	}

	void ResetAim()
	{
		x2 = Screen.width / 2;
		y2 = Screen.height / 2;
		y = 0;
		onlyLeft = false;
		onlyRight = false;
		at.myArm.transform.localRotation = Quaternion.identity;
		at.myArm2.transform.localRotation = Quaternion.identity;
		aimObj.SetActive (false);
	}

	void TurnOffLR()
	{
		aimLineRenderer.enabled = false;
		aimLineRenderer2.enabled = false;
	}
	void FixedUpdate (){
		AimingSystem ();
	}
	void AimingSystem()
	{
		if (Input.GetKey (at.shifter) && Input.GetMouseButton(1)) {	

			y2 += Input.GetAxis ("Mouse Y") * 10;
			x2 += Input.GetAxis ("Mouse X") * 10;
			Vector3 screenPoint = new Vector3 (x2, y2 , 2);
			Vector3 WorldPos = this.GetComponent<Attributes> ().myCam.ScreenToWorldPoint (screenPoint);
			aimObj.transform.position = WorldPos;
			LookIfHit ();


			Vector3 tmp = myCam.transform.localEulerAngles;
			tmp.x = y;
			tmp.y = x;

			myCam.transform.localEulerAngles = tmp;
			myArm.transform.localEulerAngles = tmp;
		}
		if(Input.GetKey(at.shifter)){	
			aimObj.SetActive (true);
			LookIfHit ();
			y2 += Input.GetAxis ("Mouse Y") * 10;
			x2 += Input.GetAxis ("Mouse X") * 10;
			Vector3 screenPoint = new Vector3 (x2, y2 , 2);
			Vector3 WorldPos = this.GetComponent<Attributes> ().myCam.ScreenToWorldPoint (screenPoint);
			aimObj.transform.position = WorldPos;
		}
		if (Input.GetKeyUp(at.shifter))
		{
			ResetAim ();
			TurnOffLR ();
		}

	}
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
			angle += 360f;
		if(angle > 360f)
			angle-= 360f;
		return Mathf.Clamp(angle,min,max);
	}
	void LookIfHit()
	{
		Ray ray = new Ray (this.GetComponent<Attributes> ().myCam.transform.position,( aimObj.transform.position - this.GetComponent<Attributes> ().myCam.transform.position));
		RaycastHit hit;	
		if (Physics.Raycast (ray, out hit, 1000, ~2)) {
			if (hit.transform != at.myBall) {
				//Restrict here Movement Boundaries;
				Vector3 directionToTarget = at.myBall.transform.position - hit.point;
				float angle = Vector3.Angle (at.myBall.parent.GetComponent<Attributes> ().rotator.right, directionToTarget);
				if (Mathf.Abs (angle) > 90 + (widthOfDoubleShoot/2)) {
					DoTheAimThing (hit.point, 1);
					onlyRight = true;
					StartCoroutine(ReturnToOriginArm (2, at.myArm2, origRotL, 2));
					aimLineRenderer2.enabled = false;

				}
				if (Mathf.Abs (angle) < 80- (widthOfDoubleShoot/2)) {
					DoTheAimThing (hit.point, 2);
					onlyLeft = true;

					aimLineRenderer.enabled = false;
					StartCoroutine(	ReturnToOriginArm (2, at.myArm, origRotR, 1));
				}
				if (Mathf.Abs (angle) <  90 + (widthOfDoubleShoot/2) && Mathf.Abs (angle) > 80 -(widthOfDoubleShoot/2)) {
					DoTheAimThing (hit.point, 3);
					onlyLeft = false;
					onlyRight = false;
				}
			} 
		}
	}


	IEnumerator ReturnToOriginArm (float time, Transform armz, Quaternion rot, int casex)
	{	
		/*
		float elapsedTime = 0;
		while (elapsedTime < time) {
			armz.transform.localRotation= Quaternion.Slerp (armz.localRotation, rot, (elapsedTime / time));
			elapsedTime += Time.deltaTime;

		}
*/
		if (casex == 1) {
			at.myArm.transform.localRotation = rot;

		} else {
			at.myArm2.transform.localRotation = rot;
		}
		yield return null;
	}

	void DoTheAimThing(Vector3 it, int casex)
	{
		if (casex == 1) {
			at.myArm.transform.LookAt (it);

			if (Input.GetMouseButton (0)) {
				aimLineRenderer.enabled = true;
				aimLineRenderer.SetPosition (0, myArm.transform.position);
				aimLineRenderer.SetPosition (1, it);
			}
			if (Input.GetMouseButtonUp (0)) {
				aimLineRenderer.enabled = false;
			}

		}
		if (casex == 2) {
			at.myArm2.transform.LookAt (it);
			if (Input.GetMouseButton (0)) {
				
				aimLineRenderer2.enabled = true;
				aimLineRenderer2.SetPosition (0, at.myArm2.transform.position);
				aimLineRenderer2.SetPosition (1, it);

			}
			if (Input.GetMouseButtonUp (0)) {
				aimLineRenderer2.enabled = false;
			}
		}
		if (casex == 3) {
			at.myArm.transform.LookAt (it);
			at.myArm2.transform.LookAt (it);


			if (Input.GetMouseButton (0)) {
				aimLineRenderer.enabled = true;
				aimLineRenderer.SetPosition (0, myArm.transform.position);
				aimLineRenderer.SetPosition (1, it);
				aimLineRenderer2.enabled = true;
				aimLineRenderer2.SetPosition (0, at.myArm2.transform.position);
				aimLineRenderer2.SetPosition (1, it);
			}
			if (Input.GetMouseButtonUp (0)) {
				aimLineRenderer.enabled = false;
				aimLineRenderer2.enabled = false;
			}
		}
	}
}
