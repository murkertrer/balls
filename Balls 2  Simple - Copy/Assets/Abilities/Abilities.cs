using UnityEngine;
using System.Collections;

public class Abilities : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public 	virtual void  GetAmmoCharacteristics(int max, int cur, int type, float rate, string name)
	{
		print ("called get ammo on base");

	}

	void ThrowItDouble (Rigidbody it, float force, int wha)
	{
		print ("got base but only for one level");

	}
}
