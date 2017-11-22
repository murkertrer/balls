using UnityEngine;
using System.Collections;

public class FadeOverTime : MonoBehaviour {

	public float fadeTime;

	public void StartFade(float t)
	{
		fadeTime = t;
		StartCoroutine(FadeTo(.1f, fadeTime));
	}

	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = this.GetComponent<Renderer> ().material.color.a;
		for (float t = 0.0f; t < 1; t += Time.deltaTime/aTime )
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			this.GetComponent<Renderer> ().material.color = newColor;
			yield return null;

		}
	//	Destroy (this.gameObject);
	}
	void Update () {
	
	}
}
