using UnityEngine;
using System.Collections;

public class CreatureSpaceManipulation : MonoBehaviour {

	bool artificailGravity;
	bool fauxGrav;
	bool planeGrav;
	PlanetPower pp;
	Transform attractorForce;
	Transform ball;
	Transform armor;

	void OnEnable()
	{
		ball = this.GetComponent<CreatureAttributes> ().myAiBall;
		armor = this.GetComponent<CreatureAttributes> ().myAiArmor;
	}

	public void ActivatePlanetaryAttraction(Transform planet, PlanetPower it)
	{
		attractorForce = planet;
		artificailGravity = true;
		ball.GetComponent<Rigidbody>().useGravity = false;
		artificailGravity = true	;
		fauxGrav = true;
		pp = it;
	}
		
	void FixedUpdate()
	{
		FauxAttraction ();
	}
	void FauxAttraction ()
	{
		if (artificailGravity && fauxGrav) {
			if (pp) {
				//pp.AttractBastard (ball);
			}
			Vector3 gravityUp = (armor.position - pp.transform.position).normalized;
			Vector3 localUp = armor.up;
			Quaternion targetRotation = Quaternion.FromToRotation(localUp,gravityUp) * armor.rotation;
			armor.rotation = Quaternion.Slerp (armor.rotation, targetRotation, 50f * Time.deltaTime);
			//armorFlip.rotation = Quaternion.FromToRotation (armorFlip.transform.up, attractor.GetRotation(armorFlip), 50f * Time.deltaTime);
		}
	}
}
