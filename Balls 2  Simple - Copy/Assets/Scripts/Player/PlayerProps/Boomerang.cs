using UnityEngine;
using System.Collections;
public class Boomerang : MonoBehaviour {
	public AudioClip soundWhenCollision;
	public float damage = 20;
	public bool oneCollision;
	public Transform playerOrigin;
	Rigidbody rb;
	public float disto;
	public float withdo;
	public float timeo;
	public Vector3 direct;
	Coroutine co=null;
	Transform origin;
	LineRenderer lr;
	bool runningCor;
	public float boomerangForceWhenCollided = -10;
	public Transform myPlayerz;

	float poso;
	bool onedone;
	float h;

	void OnEnable()
	{
		h = this.transform.position.y;
	}

	public void Doit(float d, float w, float t,Vector3 directionz, float inclinationU, float inclinationS, float y, Transform origin, Transform myPlayer )
	{
		myPlayerz = myPlayer;
		co = StartCoroutine(Throw(d, w, directionz,t, inclinationU,inclinationS, y, origin ));
	}	public Transform fwd;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
		//	StartCoroutine(Throw(18.0f, 1.0f, fwd.forward, 2.0f));
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if (!oneCollision) {    
			if (runningCor){
				StopCoroutine (co);
			}
			this.GetComponent<Rigidbody> ().useGravity = true;
			this.gameObject.AddComponent<ShrinkAndDestroy> ();

			if (col.gameObject.GetComponent<Rigidbody> () && col.transform != origin) {
				col.gameObject.GetComponent<Rigidbody> ().velocity = col.impulse * boomerangForceWhenCollided;
			}

			if (col.gameObject.tag == "Player") {
				if (col.transform != myPlayerz) {

					if (soundWhenCollision) {
						AudioSource.PlayClipAtPoint (soundWhenCollision, this.transform.position);
					}
					if (col.transform.parent.GetComponent<Attributes> ()) {
						col.transform.parent.GetComponent<Attributes> ().TakeDamage (damage);
					}

					if (col.transform.parent.GetComponent<CreatureAttributes> ()) {
						col.transform.parent.GetComponent<CreatureAttributes> ().TakeDamage (damage);
					}
				} else {
					Destroy (this.gameObject);
				}
			}
			oneCollision = true;
		}
	}

	Vector3 pos1;
	Vector3 pos2;

	IEnumerator Throw(float dist, float width, Vector3 direction, float time, float inclinationUp, float inclinationSide, float yz, Transform op) {

		runningCor = true;
		rb = this.GetComponent<Rigidbody> ();

		Vector3 pos = op.transform.position;
		Quaternion q = Quaternion.FromToRotation (Vector3.forward, direction);
		float timer = 0.0f;
		while (timer < time) {
			float t = Mathf.PI * 2.0f * timer / time - Mathf.PI/2.0f;
			float x = width * Mathf.Cos(t);
			float z = dist * Mathf.Sin (t);
			Vector3 v = new Vector3(x,h,z+dist);
			//Vector3 v = new Vector3(x,op.transform.position.y,z+dist);
			v = Quaternion.AngleAxis(inclinationUp,Vector3.right)*v;
			v = Quaternion.AngleAxis(inclinationSide,Vector3.forward)*v;
			this.transform.Rotate (Vector3.up * Time.deltaTime * 800);
			if (rb) {
				rb.MovePosition (op.transform.position + (q * v));
				//Object follow target but not regarding the height.
				//+(op.transform.right *-.5f)+(op.transform.forward *-.5f)
				//rigidbody.MovePosition (pos + (q * v));
			}
			timer += Time.deltaTime;
			yield return null;
		}

		if (Vector3.Distance (this.transform.position, op.transform.position) < 3) {
			Destroy (this.gameObject);
		} else {

			//Implement here a methood for the object to keep moving in the same trayectory

			this.GetComponent<Rigidbody> ().useGravity = true;
			oneCollision = true;
			this.gameObject.AddComponent<ShrinkAndDestroy> ();

			/*
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        rigidbody.rotation = Quaternion.identity;
        rigidbody.MovePosition (pos);
        */
			runningCor = false;
			print (runningCor);
		}
	}
} 
