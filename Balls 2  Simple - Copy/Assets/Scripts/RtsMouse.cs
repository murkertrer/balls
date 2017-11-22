using UnityEngine;
using System.Collections;

public class RtsMouse : MonoBehaviour {

	CameraAttributes cA;
	Quaternion orientationPlane;


	Attributes at;
	float sH;
	float sW;
	public float mouseRange= 5;
	public float scrollSpeed = 10;
	// Use this for initialization
	void OnEnable()
	{
		at = this.transform.root.GetComponent<Attributes> ();
		sH = Screen.height;
		sW = Screen.width;
		cA = this.GetComponent<CameraAttributes> ();
	}

	public void SetOrientationPlane(Quaternion it)
	{
		orientationPlane = it;
	}
	// Update is called once per frame
	void FixedUpdate () {
		MouseClicks ();
		Translation ();
	}

	void Translation()
	{
		if (Input.mousePosition.y >= sH - mouseRange) {
			transform.Translate ((orientationPlane * Vector3.forward) * Time.deltaTime * scrollSpeed, Space.World);
		}
		if (Input.mousePosition.y <=  mouseRange) {
			transform.Translate ((orientationPlane * Vector3.back) * Time.deltaTime * scrollSpeed, Space.World);
		}


		if (Input.mousePosition.x >= sW - mouseRange) {

			transform.Translate ((this.transform.rotation * Vector3.right) * Time.deltaTime * scrollSpeed, Space.World);
		}
		if (Input.mousePosition.x <=  mouseRange) {
			transform.Translate ((this.transform.rotation * Vector3.left) * Time.deltaTime * scrollSpeed, Space.World);
		}
	}

	void MouseClicks()
	{
		if (Input.GetMouseButtonDown (0)) {

			RaycastHit hitInfo = new RaycastHit ();
			Physics.Raycast (this.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition), out hitInfo);
			if (hitInfo.transform.tag == "Player") {
				print ("gt player");

				hitInfo.transform.GetComponent<MeshRenderer> ().material.color = Color.green;
			} else {
				if (cA.selectedObjects.Count> 0)
				{
					for (int i = 0; i < cA.selectedObjects.Count; i++) {
						//Assign material to internal variable
						//cA.selectedObjects[i].GetComponent<MeshRenderer>().material.color= 
					}
				}
			}
		}
	}
}
