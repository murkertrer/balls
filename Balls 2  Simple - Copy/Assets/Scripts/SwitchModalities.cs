using UnityEngine;
using System.Collections;

public class SwitchModalities : MonoBehaviour {

	Attributes at= null;
	PlayerFly pf= null;
	FlightAim fa = null;
	FlightCam fc= null;
	FlightShoot fs= null;
	AimingSystemSimpleDouble asd; 
	BallControl bc;
	PlayerHandle ph;
	KeyCode modSwitch;
	public bool ground;
	Transform groundReference;
	Quaternion initialRotArmor;
	Quaternion initialRotCam;
	Vector3 initialLocalPosCAm;
	void OnEnable()
	{
		modSwitch = KeyCode.H;
		//pf = GetComponent<PlayerFly> ();
		fa = GetComponent<FlightAim> ();
		fc = GetComponent<FlightCam> ();
		bc = GetComponent<BallControl> ();
		at = this.GetComponent<Attributes> ();
		ph = GetComponent<PlayerHandle> ();
		asd = GetComponent<AimingSystemSimpleDouble> ();
		initialRotArmor = at.myArmor.transform.localRotation;
		ChangeThings ();
		initialLocalPosCAm = at.myCam.transform.localPosition;
		initialRotCam = at.myCam.transform.localRotation;
	}
	void Update () {
		if (Input.GetKeyDown (modSwitch)) {
			ground = !ground;;
			ChangeThings ();
		}
	}
	void ChangeThings()
	{
		if (ground) {
			//pf.enabled = false;
			fc.enabled = false;
			at.myBall.GetComponent<Rigidbody> ().useGravity = true;
			bc.enabled = true;
			at.myArmor.transform.localEulerAngles = Vector3.zero;
			at.rotator.transform.localEulerAngles = Vector3.zero;
			at.myCam.transform.localPosition = initialLocalPosCAm;
			ph.isFlying = false;
			asd.iFly  = false;
			//StartCoroutine (CamToOriginal (3));
			//StartCoroutine(RotateArmorToPlane(2));
			//CamToOriginal (2);
		} else {
			//pf.enabled = true;
			fc.enabled = true;
			at.myBall.GetComponent<Rigidbody> ().useGravity = false;
			bc.enabled = false;
			ph.isFlying = true;
			asd.iFly  = true;
			at.myBall.GetComponent<MeshRenderer> ().enabled = true;
		}
	}
}

/*
	IEnumerator CamToOriginal(float t)
	{
		if (!at.relativePlane) {
			float elapsedTime = 0;
			while (elapsedTime < t) {
				at.myCam.transform.localPosition = Vector3.Lerp(at.myCam.transform.localPosition, initialLocalPosCAm, (elapsedTime/t));
				at.myCam.transform.localRotation = Quaternion.Lerp (at.myCam.transform.localRotation, initialRotCam,(elapsedTime / t) ); 
				at.rotator.localEulerAngles = Vector3.Lerp(at.rotator.localEulerAngles, Vector3.zero, (elapsedTime/t));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
	}
	*/
/*
IEnumerator RotateArmorToPlane(float t)
{
	//Vector3 curRot = at.myArmor.transform.rotation;
	if (!at.relativePlane) {
		float elapsedTime = 0;
		while (elapsedTime < t) {
			//at.myArmor.transform.localEulerAngles = V.Lerp (at.myArmor.transform.localEulerAngles, 
			//new Vector3(0, at.myArmor.transform.localEulerAngles.y,0),(elapsedTime / t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
*/
	
