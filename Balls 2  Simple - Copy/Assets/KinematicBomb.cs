using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KinematicBomb : MonoBehaviour {
	public List<GameObject> rbUnderInfluence = new List<GameObject> ();
	public List<Vector3> originalSpeed = new List<Vector3> ();

	public float radius = 8;
	public float freezeDuration = 3;
	public Transform radiusShower;
	public GameObject radiusShowerInstantiated;
	bool inProcess;
	public bool restorOriginalSpeed;

	void OnCollisionEnter()
	{
		if (!inProcess) {
			AreaFreeze (this.transform.position);
			inProcess = true;

		}
		//AudioSource.PlayClipAtPoint (explosionSound, this.transform.position);	
	}
	void AreaFreeze (Vector3 origin)
	{
		Collider[] objectsInRange = Physics.OverlapSphere (origin ,radius);

		for (int i = 0; i < objectsInRange.Length; i++) {
			if (objectsInRange [i].gameObject.GetComponent<Rigidbody> ()) {
				if (restorOriginalSpeed) {
					originalSpeed.Add(objectsInRange [i].gameObject.GetComponent<Rigidbody> ().velocity);
				}				
				rbUnderInfluence.Add (objectsInRange [i].gameObject);
				objectsInRange[i].gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
		/*
		foreach (Collider col in objectsInRange)
		{
			if (col.GetComponent<Rigidbody> ()) {
				rbUnderInfluence.Add (col.GetComponent<Rigidbody>());
				originalSpeed.Add (col.GetComponent<Rigidbody> ().velocity);

				col.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
		*/
		//radiusShowerInstantiated = go;
		//radiusShowerInstantiated.transform.SetParent (this.gameObject.transform);
		//radiusShowerInstantiated.transform.localScale = new Vector3 (5, 5, 5);
		StartCoroutine (Unfreeze ());


	}

	IEnumerator Unfreeze ()
	{

		yield return new WaitForSeconds (freezeDuration);
		for (int i = 0; i < rbUnderInfluence.Count; i++) {
			if (rbUnderInfluence [i].gameObject != null) {

				if (rbUnderInfluence [i].gameObject.GetComponent<Rigidbody> ()) {
					rbUnderInfluence [i].gameObject.GetComponent<Rigidbody> ().isKinematic = false;
					if (restorOriginalSpeed) {
						rbUnderInfluence [i].gameObject.GetComponent<Rigidbody> ().velocity = originalSpeed [i];
					}	
				}
			}
		}
		Destroy (this.gameObject);
	}
}
