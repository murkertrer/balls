using UnityEngine;
using System.Collections;

public class AssignRbMassAccordingToSize : MonoBehaviour {
	public float isSphere;
	public float planetMass;
	public float radius;

	public Renderer rend;
	void OnEnable() {

		planetMass = GetPlanetMass ();

	}
	void OnDrawGizmosSelected() {
		rend = GetComponent<Renderer>();

		Vector3 center = rend.bounds.center;
		float radius = rend.bounds.extents.y;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(center, radius);
	}
	public float GetPlanetMass()
	{
		rend = GetComponent<Renderer>();
		radius = rend.bounds.extents.y;
		float mass = (4 / 3) * Mathf.PI *((radius)*(radius)*(radius));
		planetMass = mass;
		return mass;
	}
	public void AssignPlanetMass()
	{
		GetComponent<Rigidbody> ().mass = GetPlanetMass ();
	}
}
