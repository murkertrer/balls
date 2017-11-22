using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ProyectileHandle : NetworkBehaviour {

	float timeOfCreation;
	float maxLifeTime = 2;
	void OnEnable()
	{
		timeOfCreation = Time.time;

		if (!isServer) {
			//this.gameObject.GetComponent<Rigidbody> ().useGravity = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isServer) {
			if (this.transform.position.y < -50) {
				NetworkServer.Destroy (this.gameObject);
			}


			if ((Time.time - timeOfCreation) > maxLifeTime) {
				print ("cuz of time");
				NetworkServer.Destroy (this.gameObject);
			}
		}
	}
}
