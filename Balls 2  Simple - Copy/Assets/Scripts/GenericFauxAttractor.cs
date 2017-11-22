using UnityEngine;
using System.Collections;

public class GenericFauxAttractor : MonoBehaviour {

	public FauxGravityAttractor attractor;

	void FixedUpdate () {
		if (attractor) {


			Vector3 gravityUp = (this.transform.position - attractor.transform.position).normalized;
			Vector3 localUp = this.transform.up;
			Quaternion targetRotation = Quaternion.FromToRotation (localUp, gravityUp) * this.transform.rotation;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, targetRotation, 50f * Time.deltaTime);

			//armorFlip.rotation = Quaternion.FromToRotation (armorFlip.transform.up, attractor.GetRotation(armorFlip), 50f * Time.deltaTime);
		}
	}
}
