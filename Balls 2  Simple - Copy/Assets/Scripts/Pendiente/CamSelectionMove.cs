using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamSelectionMove : MonoBehaviour {
	Attributes at;
	Vector3 camLocalPositionAsRotatorChild;
	public Transform Cam;
	PlayerHandle pH;
	CameraAttributes cA;
	public float speed = 10f;
	public float startTime;
	public float journeyLenght;
	bool arm;
	public int armPos = 2;
	bool rotator;
	public int rotatorPos =2;	

	public bool inFPS;
	Vector3 originalPos;


	void Start () {
		at = this.GetComponent<Attributes> ();
		camLocalPositionAsRotatorChild = Cam.transform.localPosition;
		pH = this.transform.GetComponent<PlayerHandle> ();
		cA = at.myCam.GetComponent<CameraAttributes> ();
	}

	// Update is called once per frame

		/*
		if (Input.GetKeyDown (KeyCode.C)) {
			inFPS = !inFPS;

			if (inFPS) {
				originalPos = Cam.transform.localPosition;
				Cam.transform.localPosition = at.myBall.transform.transform.position;
			
			}
			else
			{
				Cam.transform.localPosition = originalPos;
				
			}
		}
	}
	*/
}
/*
 * 
		if (Input.GetKeyDown (KeyCode.C)) {
			//Cam.transform.position = Vector3.zero
			Cam.transform.localPosition = at.myArm.transform.localPosition + new Vector3(-1,0,0);
			//Cam.transform.localPosition = at.myBall.position + new Vector3 (0,0,-.2f);

			Cam.transform.localRotation = at.myArmor.localRotation;
			Cam.transform.transform.parent = at.myArm;
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			Cam.transform.parent = at.myArm;
			if (armPos == 2) {
				//Cam.transform.localPosition = Vector3.zero;
				Cam.transform.localPosition = new Vector3 (0, 0 ,0);
				armPos = 1;
				return;
			}
			if (armPos == 1) {
				Cam.transform.localPosition = new Vector3 (0, 0 ,- 15);
				armPos = 2;
			}
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			Cam.transform.parent = at.rotator;

			if (rotatorPos == 2) {

			Cam.transform.localPosition = camLocalPositionAsRotatorChild;
				rotatorPos = 1;
				return;
			}

			if (rotatorPos == 1) {
				Cam.transform.localPosition = at.myBall.localPosition + new Vector3 (0, .5f, -.3f);
				rotatorPos = 2;
			}
		}
	}
 * */