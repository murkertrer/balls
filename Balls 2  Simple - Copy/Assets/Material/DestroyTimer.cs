using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {
	public bool destroyAfterTime;
	public float maxLifeTime = 15;

	float timeOfCreation;

	// Use this for initialization
	void OnEnable () {
		timeOfCreation = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (destroyAfterTime) {
		
			if ((Time.time - timeOfCreation) > maxLifeTime) {
				Destroy(gameObject);
			}
		}
	
	}

	public void TickingClockLifeSpan(int it)
	{
		destroyAfterTime = true;
		maxLifeTime = it;
	}
}
