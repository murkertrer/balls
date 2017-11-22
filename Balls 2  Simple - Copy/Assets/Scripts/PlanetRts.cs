using UnityEngine;
using System.Collections;

public class PlanetRts : MonoBehaviour {
	public Transform planet;

	float x;
	float y;
	public float roationSpeed = .5f;
	public float keyBoardRotationSpeed = 2;

	void Start()
	{
		this.transform.LookAt (planet);
	}
	// Update is called once per frame
	void Update () {

		/*
		x += Input.GetAxis ("Mouse X");
		y += Input.GetAxis ("Mouse Y");
		*/
		if (Input.GetKey(KeyCode.W))
		{
			y =  keyBoardRotationSpeed;
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			y = 0;
		}

		if (Input.GetKey(KeyCode.D))
		{
			x =  keyBoardRotationSpeed;
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			x = 0;
		}

		this.transform.RotateAround (planet.transform.position, this.transform.up, x * roationSpeed*-1);
		this.transform.RotateAround (planet.transform.position, this.transform.right, y * roationSpeed);

	}
}
