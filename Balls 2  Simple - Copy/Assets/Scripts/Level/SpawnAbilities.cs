using UnityEngine;
using System.Collections;

public class SpawnAbilities : MonoBehaviour {

	public Vector3 groundZero;
	public GameObject itemAdder;
	Vector3 AtPlacing;
	public AudioClip pickUpS;

	public int numberOfCubes = 30;
	// Update is called once per frame

	public float atX;
	public float atY;
	public float atZ;

	public float varUp = 3;
	public float varDown = 5;

	void OnEnable()
	{
		AtPlacing = new Vector3(-10,2,0);
		atX = AtPlacing.x;
		atY = AtPlacing.y;
		atZ = AtPlacing.z;
		for (int i = 0; i < numberOfCubes; i++) {

			AtPlacing = new Vector3 (Random.Range (atX + varDown, atX + varUp), atY, atZ);
			GameObject it= Instantiate (itemAdder, AtPlacing, Quaternion.identity) as GameObject;
			it.AddComponent<AbilityInfo> ();
			it.GetComponent<AbilityInfo> ().type = Random.Range (1, 8);
			it.GetComponent<AbilityInfo> ().maxAmmo = Random.Range (2, 600);
			it.GetComponent<AbilityInfo> ().expulsion = Random.Range (1, 5);
			it.GetComponent<AbilityInfo> ().pickUpSound = pickUpS;
			it.GetComponent<AbilityInfo> ().Chamelon ();

			it.AddComponent<AudioSource> ();
	

			atZ += 3;



		}
	
	}
}
/* 1 - Grav Explosion
	 * 2-Straight Explosion
	 * 3-Shield rotate
	 * 4-Gravi Implosion
	 *  * 5=Gravity Bouncy MachinGun;
	 * 6 = No Garvity bouncy;
	 * 7 = Homing Missle
	 * 8 = Gravity
	 * 
	 * Expel Methood
	 * 1 - chargedShot
	 * 2 FixedShot
	 * 3 Machine Gun
	 * 4 Place
	 * 5 place And Follow;
	*/