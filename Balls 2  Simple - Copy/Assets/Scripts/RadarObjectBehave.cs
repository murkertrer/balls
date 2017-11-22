using UnityEngine;
using System.Collections;

public class RadarObjectBehave : MonoBehaviour {
	Transform ballDummy;

	Transform representedObject;
	Transform playerBall;
	float radarDummyBounds;
	float radius ;
	bool activated;
	// Use this for initialization
	void Start () {
	
	}
	public void SetUp(Transform ball, Transform repObj, float rdrDB, float radius2, Transform ballD)
	{

		playerBall = ball;
		representedObject = repObj;
		radarDummyBounds = rdrDB;
		radius = radius2;
		ballDummy = ballD;
		activated = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (activated) {
			float distance = Vector3.Distance (playerBall.transform.position, this.transform.position);
			float percentage = (radius - (radius - distance)) / radius;

			float distanceToWorkWith = radarDummyBounds;
			float relativeDistance = distanceToWorkWith * percentage;
			Vector3 relativePos = playerBall.transform.position - this.transform.position;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			rotation = ballDummy.transform.localRotation * rotation;

			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -relativeDistance);
			Vector3 position = rotation * negDistance + ballDummy.transform.position;
			this.transform.position = position;
		}
	}
}
