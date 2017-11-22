using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadarPosition : MonoBehaviour {
	Transform radarD;
	Transform radarC;

	RadarOne rO;
	Attributes at;
	Vector3 originalRot;
	KeyCode radarKey;
	float yMinLimit = -90;
	float yMaxLimit = 90;
	float x;
	float y;
	public float radarSens = 5;
	void OnEnable()
	{
		at = GetComponent<Attributes> ();
		radarD = at.ballDummy;
		radarC =at.radarCardinality;
		rO = at.myArmor.GetComponent<RadarOne> ();
		originalRot = radarD.transform.localEulerAngles;
		radarKey = at.radarKey;
		rO.DeActivateRadar ();


	}
	void Update()
	{
		if (Input.GetKeyDown (radarKey)) {
			//rO.ActivateRadar ();
		}
		/*
		if (Input.GetKey (radarKey) && Input.GetMouseButton(1)) {

			x += Input.GetAxis ("Mouse Y") * radarSens * -1;
			y +=  Input.GetAxis ("Mouse X") * radarSens ;
			x = ClampAngle (x, yMinLimit, yMaxLimit);
			y = ClampAngle (y,  yMinLimit, yMaxLimit);

			Rot (x, y);
			/*Vector3 tmp = radarD.transform.localEulerAngles;
			tmp.x = x;
			tmp.y = y;
			radarD.transform.localEulerAngles = tmp;

		}

		if (Input.GetKeyUp (radarKey)) {
			radarD.transform.localEulerAngles = originalRot;
			Rot (0, 0);
			rO.DeActivateRadar ();
		}
		*/

		if (Input.GetKey (radarKey) && !Input.GetMouseButton (1)) {

			Vector3 tmp = radarD.transform.localEulerAngles;
			tmp.x = at.rotator.transform.localEulerAngles.x;
			tmp.y =  at.rotator.transform.localEulerAngles.y;
			//radarD.transform.localEulerAngles = tmp;
			//radarC.transform.localEulerAngles = tmp;
		}


	}
	void Rot(float theX, float theY)
	{
		Vector3 tmp = radarD.transform.localEulerAngles;
		tmp.x = theX;
		tmp.y = theY;
		//radarD.transform.localEulerAngles = tmp;
		//radarC.transform.localEulerAngles = tmp;

		at.radarCardinality.transform.localEulerAngles = tmp;
		////.localEulerAngles = tmp;

	}
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
			angle += 360f;
		if(angle > 360f)
			angle-= 360f;
		return Mathf.Clamp(angle,min,max);
	}

}
