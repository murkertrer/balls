using UnityEngine;
using System.Collections;

public class TimeManipulation : MonoBehaviour {
	Attributes at;
	KeyCode timeManipualtionKey;
	public bool manipulatingTime;
	public bool manipulatingDown;
	public bool manipulatingUp;
	public float manipulationRate;
	public float timeManipulationDown = .5f;
	public float lerpSpeed = 10;
	public float durationOfManipulation = 2;
	float it = 10;
	public float increment = .05f;
	void OnEnable()
	{
		at = GetComponent<Attributes> ();
		timeManipualtionKey = this.GetComponent<Attributes> ().timeManipulation;
	}
	void FixedUpdate () {

		if (at.isSelected) {
			if (Input.GetKeyDown (timeManipualtionKey)) {


				Time.timeScale = .2f;
				Time.fixedDeltaTime = 0.02f * Time.timeScale;

				// manipulatingTime = true;
				//manipulatingDown = true;



			}
			if (manipulatingTime) {
				if (manipulatingDown) {
					if (it >= .5f) {
						it -= increment;
						Time.timeScale = it;
						Time.fixedDeltaTime = 0.02f * Time.timeScale;
					} else {
						manipulatingDown = false;
						StartCoroutine (TimeInTheZone ());
					}
				}
				if (manipulatingUp) {

					if (it <= 1) {
						it += increment;
						Time.timeScale = it;
						Time.fixedDeltaTime = 0.02f * Time.timeScale;

					} else {
						manipulatingUp = false;
						manipulatingTime = false;
					}
				}
			}
		}
	}
	IEnumerator TimeInTheZone()
	{
		yield return new WaitForSeconds (1);
		manipulatingUp = true;
		yield return null;
	}
}
