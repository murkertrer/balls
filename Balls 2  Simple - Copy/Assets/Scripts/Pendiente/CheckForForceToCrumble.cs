using UnityEngine;
using System.Collections;

public class CheckForForceToCrumble : MonoBehaviour {

	Rigidbody rb;
	Color myC;
	public int LenghtCoroutine = 200;
	int it;
	float FadeTime = 1;
	bool fading;
	bool oneBehaviour;

	public void ParentSaysEnough()
	{
		Destroy (this.gameObject);
	}
	// Use this for initialization
	void Start () {
	
		rb = this.GetComponent<Rigidbody> ();
		myC = this.GetComponent<Renderer> ().material.color;
		it = LenghtCoroutine;
	}

	void OnEnable()
	{
		this.transform.parent.GetComponent<CrumbleControl> ().RegisterCrumz ();
	}
	
	// Update is called once per frame
	public void CurmbleAndFly()
	{

	}

	public void WTF(Vector3 force)
	{
		rb.isKinematic = false;
		rb.useGravity = true	;
		if (this.transform.parent) {
			this.transform.parent.GetComponent<CrumbleControl> ().DecreaseCrumz ();
		}

		rb.transform.parent = null;
		rb.AddForce (force);
		this.gameObject.AddComponent<DestroyAfterTime> ();
		this.GetComponent<DestroyAfterTime> ().lifeSpan = 2;


	}

	void OnCollisionEnter(Collision collision)
	{

		if (!oneBehaviour) {
			//StartCoroutine(Fade ());
			myC.a = .3f;
			this.GetComponent<MeshRenderer> ().material.color = myC;


			Vector3 velocity = collision.relativeVelocity;
			//print (velocity.magnitude + "   mag");
			if (velocity.magnitude > 1.5) {
				rb.isKinematic = false;
				rb.useGravity = true;
				if (this.transform.parent) {
					
					this.transform.parent.GetComponent<CrumbleControl> ().DecreaseCrumz ();
				}
				rb.transform.parent = null;
				fading = true;
				oneBehaviour = true;
				//this.gameObject.AddComponent<DestroyAfterTime> ();
				//this.GetComponent<DestroyAfterTime> ().lifeSpan = 2;
				this.gameObject.AddComponent<ShrinkAndDestroy>();
			}
		}
	}

	IEnumerator Fade() {
		for (float f = 2f; f >= 0; f -= 0.1f) {
			print (f);
			Color c = this.GetComponent<Renderer>().material.color;
			c.a = f;
			this.GetComponent<Renderer>().material.color = c;
			yield return null;
		}
	}

}
