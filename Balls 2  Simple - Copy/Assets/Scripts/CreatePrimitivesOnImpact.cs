using UnityEngine;
using System.Collections;

public class CreatePrimitivesOnImpact : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = this.transform.position;
		Quaternion wantRot;
		//Vector3 direction = cube.transform.position - at.rotator.transform.position;

		//wantRot = Quaternion.LookRotation(cube.transform.position, 
		cube.transform.LookAt(this.GetComponent<ThingAttributes>().fromBallPlayer);
		cube.AddComponent<ShrinkAndDestroy> ();
		cube.GetComponent<ShrinkAndDestroy> ().shrinkTime = 1;
		cube.GetComponent<ShrinkAndDestroy> ().timeToInitializeShrink = 2;
		cube.GetComponent<ShrinkAndDestroy> ().StartShrink ();
		Destroy (this.gameObject);
	}
}
