using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float lifeSpan = 3.5f;
	void Start()
	{
		StartCoroutine(DestroyItAfterTimeX (lifeSpan));
	}

	IEnumerator DestroyItAfterTimeX(float it)
	{
		yield return new WaitForSeconds (it);
		Destroy (this.gameObject);
	}
}
