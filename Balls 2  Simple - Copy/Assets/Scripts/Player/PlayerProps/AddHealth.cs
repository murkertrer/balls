using UnityEngine;
using System.Collections;

public class AddHealth : MonoBehaviour {
	public  float healthReplenish = 50;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			if (col.transform.parent.GetComponent<Attributes> ()) {
				col.transform.parent.GetComponent<Attributes> ().AddHealth(healthReplenish );
				Destroy (this.gameObject);
			}
		}
	}
}
