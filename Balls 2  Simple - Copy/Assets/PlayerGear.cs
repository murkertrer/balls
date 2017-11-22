using UnityEngine;
using System.Collections;

public class PlayerGear : MonoBehaviour {

	public Transform shieldCarry;
	bool shieldPresent;
	public GameObject it;
	float distanceForShield;
	public KeyCode powerControll;
	//orbits

	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = .5f;
	public float distanceMax = 15f;

	private Rigidbody rigidbody;

	float x = 0.0f;
	float y = 0.0f;

	//arms
	public Transform armR;
	public Transform armL;
	public bool R = true;
	public bool L;

	PlayerHandle ph;
	Attributes at;



	// Update is called once per frame
	void Start () {
		at = this.GetComponent<Attributes> ();
		//shield
		ph = GetComponent<PlayerHandle>();
		shieldCarry = at.shieldCarry;
		target = at.myBall;
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		powerControll = KeyCode.CapsLock;

		//arms
		armR = at.myArm;
		armL = at.myArm2;
		if (!R) {
			armR.gameObject.SetActive (false);
		}
		if (!L) {
			armL.gameObject.SetActive (false);
		}

		if (R) {
			ph.R = true;
		}

		if (L) {
			ph.L = true;
		}


	}

	public void AddShield()
	{
		if (!shieldPresent) {
			
			Transform shield = Instantiate (shieldCarry, at.shieldCarryDummy.position, at.shieldCarryDummy.rotation) as Transform;
			shield.eulerAngles = new Vector3 (0, 180, 0);
			shield.gameObject.tag = "PO";
			shield.transform.parent = at.rotator;
			it = shield.gameObject;
			it.transform.position = at.shieldCarryDummy.transform.position;
			it.transform.rotation = at.shieldCarryDummy.transform.rotation;
			distanceForShield = Vector3.Distance (at.myBall.position, at.shieldCarryDummy.transform.position);
		}
	}
	void LateUpdate()
	{
		if (it && Input.GetKeyDown (powerControll))
		{
			x = (it.transform.eulerAngles.y - 360);
		}
		if (it && Input.GetKey (powerControll)) {Orbit ();}
	}
	void Orbit() 
	{
		x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
		y += Input.GetAxis("Mouse Y") * xSpeed * distance * 0.007f;
		y = ClampAngle (y, yMinLimit, yMaxLimit);			
		Quaternion rotation = Quaternion.Euler(0,x, 0);
		rotation = rotation * at.rotator.transform.rotation;
		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + at.myBall.transform.position;
		it.transform.rotation = rotation;
		it.transform.position = position;
	}
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	public void AddArm(int arms)
	{
		if (arms == 1) {
			if (armR.gameObject.activeSelf == false) {
				armR.gameObject.SetActive (true);
				R = true;
				ph.R = true;
				return;
			}
			if (armL.gameObject.activeSelf == false) {
				armL.gameObject.SetActive (true);
				L = true;
				ph.L = true;
				return;
			}
		}
		if (arms == 2) {
			armR.gameObject.SetActive (true);
			armL.gameObject.SetActive (true);
			R = true;
			L = true;
			ph.L = true;
			ph.R = true;
		}
	}

}
