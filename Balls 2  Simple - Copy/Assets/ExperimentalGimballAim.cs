using UnityEngine;
using System.Collections;

public class ExperimentalGimballAim : MonoBehaviour {

	Attributes at;
	float mouseDragY;
	// Use this for initialization

	void OnEnable()
	{
		at = transform.root.GetComponent<Attributes> ();
		at.aimObj.SetActive (true);
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLookatAim ();
		DoRot ();
	
	}
	public void DoRot()
	{		
		at.rotatorDummy.RotateAround (at.myBall.transform.position, at.myArmor.up, at.bC.mouseDragX);
		mouseDragY = Input.GetAxis ("Mouse Y") * at.bC.rotationSpeed*-1;
		//at.rotator.RotateAround (at.myBall.transform.position, at.rotator.right, mouseDragY);
		at.myCam.transform.RotateAround (at.myBall.transform.position, at.rotator.right, mouseDragY);
	}

	public void PlayerLookatAim()
	{
		at.aimObj.SetActive (true);
		//at.aSSD.LookIfHit ();

		Ray ray = new Ray (at.myCam.transform.position,( at.aimObj.transform.position - at.myCam.transform.position));
		RaycastHit hit;	
		if (Physics.Raycast (ray, out hit, 10000, ~2)) {
			if (hit.transform != at.myBall) {

				at.aSSD.LookAtPointInQuestion (hit.point);

			}
		}

	}
}
