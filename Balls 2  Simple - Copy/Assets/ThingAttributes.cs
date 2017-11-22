using UnityEngine;
using System.Collections;

public class ThingAttributes : MonoBehaviour {
	public Transform fromBallPlayer;
	public bool friendlyFire;
	public int team;
	public bool takeDamage;
	public float maxH = 100;
	float curH;

	public bool isPlayer;
	public bool isCreature;
	public bool isGenericThing;

	void OnEnable()
	{
		curH = maxH;
		if (this.GetComponent<Attributes> ()) {
			isPlayer = true;
			return;
		}
		if (this.GetComponent<CreatureAttributes>())
		{
			isCreature = true;
			return;
		}
	}

	public void TakeDamage(float amount, int teamz)
	{
		if (!takeDamage) {
			return;
		}
		if (!friendlyFire) {
			if (teamz != team) {
				ApplyDamage (amount);
				curH -= amount;

				if (curH < 0) {
					Destruction ();
				}
			}
		} else {
			ApplyDamage (amount);
		}
	}

	void ApplyDamage(float amount)
	{
		if (isPlayer) {
			this.GetComponent<Attributes> ().TakeDamage (amount);

		}
		if (isCreature) {
			this.GetComponent<CreatureAttributes> ().TakeDamage (amount);
		}

		if (curH < 0) {
			Destruction ();
		}
	}
		
	void Destruction()
	{
		Destroy (this.gameObject);
	}
}
