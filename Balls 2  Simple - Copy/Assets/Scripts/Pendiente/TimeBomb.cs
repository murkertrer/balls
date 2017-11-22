using UnityEngine;
using System.Collections;

public class TimeBomb : MonoBehaviour {
	
	public float radius = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter()
	{
		//FreezeEm ();
	}
	void FreezeEm()
	{
		print (Time.fixedDeltaTime);
		Time.timeScale = .5f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		Collider[] objectsInRange = Physics.OverlapSphere (this.transform.position ,radius);
		foreach (Collider col in objectsInRange) {
		}
	}
}
