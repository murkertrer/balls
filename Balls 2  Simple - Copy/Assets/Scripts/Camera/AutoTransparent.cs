using UnityEngine;
using System.Collections;

public class AutoTransparent : MonoBehaviour {
	public float timeToReturnOriginalMaterial = .2f;
	private Shader m_OldShader = null;
	private Color m_OldColor = Color.black;
	private float m_Transparency = 0.3f;
	private const float m_TargetTransparancy = 0.3f;
	private const float m_FallOff = 0.2f; // returns to 100% in 0.1 sec
	Material one;
	Material original;
	int originalLayer;

	float lastMode;
	float nowMod;
	void OnEnable()
	{

		one = this.GetComponentInChildren<Renderer> ().material;
		originalLayer = this.gameObject.layer;
	}

	public void BeTransparent(Material a)
	{
		if (this.GetComponent<Renderer> ()) {
			this.GetComponent<Renderer> ().material = a;
			this.gameObject.layer = 1;
			lastMode = Time.time;
		}

	}
	void LateUpdate()
	{
		nowMod = Time.time;
		float totalMod = nowMod - lastMode;
		if (totalMod > timeToReturnOriginalMaterial) {
			this.GetComponentInChildren<Renderer> ().material = one;
			this.gameObject.layer = originalLayer;
			totalMod = 0;
			Destroy (this);
		}
	}
}

