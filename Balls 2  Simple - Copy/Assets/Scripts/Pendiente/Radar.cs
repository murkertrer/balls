using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

	bool activateRadar;
	public List<GameObject> radarObjects = new List<GameObject> ();
	public Transform mainParent;
	GameObject it;

	void Start () {
		StartCoroutine (WaitAndActivate ());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{

		if (activateRadar) {
			if (other.gameObject.tag != "Terrain" && other.gameObject.name != "Arm") {

				print ("spawning proyectiles");
				
				Vector3 miniatureObjPos = other.transform.position;
				transform.LookAt (other.transform.position);
				print (other.gameObject.name);

				Vector3 relativePos = mainParent.transform.InverseTransformDirection (other.transform.position);
					//mainParent.transform.position -other.transform.position;

				GameObject go = Instantiate (other.gameObject, relativePos, other.transform.rotation) as GameObject;
				it = go;
				radarObjects.Add (go);
				if (go.GetComponent<BoxCollider> ()) {
					Destroy (go.GetComponent<BoxCollider> ());
				}
			}
		}
	}
	void LateUpdate()
	{
		if (it) {
			it.transform.position = mainParent.transform.InverseTransformDirection (it.transform.position);
		}
	}
	void OnTriggerExit(Collider other)
	{
		print ("exit" + other.gameObject.name);
		if (radarObjects.Contains(other.gameObject))
			{
				radarObjects.Remove (other.gameObject);
				Destroy(other.gameObject);
			}
	}

	IEnumerator WaitAndActivate()
	{
		yield return new WaitForSeconds (2);
		activateRadar = true;
	}
}
