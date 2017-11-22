using UnityEngine;
using System.Collections;

public class xBoomernag : MonoBehaviour {

	Rigidbody rigidbody;
	public Transform fwd;
	Coroutine co=null;
	float h;

	void OnEnable()
	{
		h = this.transform.position.y;
	}

	public void Doit(float d, float w, float t,Vector3 directionz, float inclinationU, float inclinationS, float y, Transform origin, Transform myPlayer )
	{
		co = StartCoroutine(Throwp(d, w, directionz,t, origin.transform.position.y));
	}

	void Start()
	{
		rigidbody = this.GetComponent<Rigidbody> ();
	}
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			//StartCoroutine(Throw(18.0f, 1.0f, fwd.forward, 2.0f));
		}
	}

	IEnumerator Throwp(float dist, float width, Vector3 direction, float time, float heighto) {
		rigidbody = this.GetComponent<Rigidbody> ();

		Vector3 pos = transform.position;
		float height = heighto;
		Quaternion q = Quaternion.FromToRotation (Vector3.forward, direction);
		float timer = 0.0f;
		rigidbody.AddTorque (0.0f, 400.0f, 0.0f);
		while (timer < time) {
			float t = Mathf.PI * 2.0f * timer / time - Mathf.PI/2.0f;
			float x = width * Mathf.Cos(t);
			float z = dist * Mathf.Sin (t);
			Vector3 v = new Vector3(x,h,z+dist);
			rigidbody.MovePosition(pos + (q * v));
			timer += Time.deltaTime;
			yield return null;
		}

		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		rigidbody.rotation = Quaternion.identity;
		rigidbody.MovePosition (pos);
	}

}
