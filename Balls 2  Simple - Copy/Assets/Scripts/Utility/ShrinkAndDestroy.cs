using UnityEngine;
using System.Collections;

public class ShrinkAndDestroy : MonoBehaviour {

	public float shrinkTime=2;
	public float timeToInitializeShrink = 2;
	// Use this for initialization
	void OnEnable(){

	}
	public void SetTimes(float amount, float delay)
	{
		shrinkTime = amount;
		timeToInitializeShrink = delay;
		StartShrink ();
	}

	public void StartShrink()
	{
		StartCoroutine (Shrink (shrinkTime, timeToInitializeShrink));

	}
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator Shrink(float time, float delay)
	{
		yield return new WaitForSeconds (delay);
		Vector3 originalScale = this.transform.localScale;
		Vector3 destinationScale = new Vector3 (0.2f, 0.2f, 0.2f);

		float currentTime = 0.0f;
		if (!System.Single.IsNaN (destinationScale.x) && !System.Single.IsNaN (destinationScale.y) && !System.Single.IsNaN (destinationScale.z)) {
		do {			
			transform.localScale = Vector3.Lerp (originalScale, Vector3.zero, (currentTime / time));
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= time);
	}

		Destroy(gameObject);
	}


}
