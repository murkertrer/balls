using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InformAboutCollision : MonoBehaviour {
	public List<Collision> collidingObjects = new List<Collision> ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
		if (collidingObjects.Count > 0) {
			transform.root.GetComponent<BallControl> ().Colliding ();
		} else {
			transform.root.GetComponent<BallControl> ().NotColliding ();

		}

	}

	void OnCollisionEnter(Collision col)
	{
		collidingObjects.Add (col);
	}
	void OnCollisionExit(Collision col)
	{
		collidingObjects.RemoveAt (0);
	}

}
