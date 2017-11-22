using UnityEngine;
using System.Collections;

public class AddPlayerGear : MonoBehaviour {

	public bool anotherArm;
	public int howManyArms;
	public bool addShield;

	public Material shieldMaterial;
	public Material armMaterial;
	Renderer r;

	void OnEnable()
	{
		r = this.GetComponent<Renderer> ();

		if (anotherArm) {
			r.material = armMaterial;
		}
		if (addShield) {
			r.material = shieldMaterial;
		}
	}


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {


			if (col.transform.root.GetComponent<PlayerGear>()) {
				if (anotherArm) {					
					col.transform.root.GetComponent<PlayerGear> ().AddArm (1);
				}
				if (addShield) {
					col.transform.root.GetComponent<PlayerGear> ().AddShield ();

				}
			}
		}
		Destroy (this.gameObject);

	}
}
