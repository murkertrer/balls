using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraAttributes : MonoBehaviour {
	Attributes at;
	public bool inPlayer = true;
	public Transform currentPlayer;
	public bool inWolrd;
	Transform aimObj;
	//CameraControl cc;
	public List<GameObject> selectedObjects= new List<GameObject>();

	void OnEnable()
	{
		at = transform.root.GetComponent<Attributes> ();
		aimObj = at.aimObj.transform; 
		at.kingCamera = this.GetComponent<Camera> ();
	}
	void Start()
	{
		if (!at.kT.kingCamera ) {
			if (GetComponent<Camera>())
			{
				//at.kT.EstablishingKingCam (GetComponent<Camera> ());
			}
			else
			{
				print ("WAnt to establish camera but have non2");
			}	
		}
		
	}
	public void EnterRTS()
	{
		//cc.enabled = false;
	}
	public void EnterFPS()
	{
		//cc.enabled = true;
	}
	void FixedUpdate()
	{
		//aimObj.transform.localRotation = transform.localRotation;
	}
}
