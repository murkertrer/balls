using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextNotificationFloat : MonoBehaviour {
	public Transform playerT;
	public Transform playerCam;
	public float amountOfTime = 2;
	public float upIncrement=.005f;

	public Transform rotationReference;

	public bool doDouble;

	void Onenable ()
	{
		print (this.transform.gameObject.name);
	}
	void LateUpdate()
	{
		if (playerCam) {
			this.transform.LookAt (2 * this.transform.position - playerCam.transform.position);
		}
		Vector3 thispos = this.transform.position;
		if (!doDouble) {

			thispos.y += upIncrement * 3;
			this.transform.position = thispos;
			Color it = this.GetComponent<Text> ().color;
			it.a -= upIncrement;
			this.GetComponent<Text> ().color = it;
		} else {
			thispos.y += upIncrement * 6;
			this.transform.position = thispos;
			Color it = this.GetComponent<Text> ().color;
			it.a -= upIncrement*2;
			this.GetComponent<Text> ().color = it;		
		}
	}

	public void DestroyText()
	{
		StartCoroutine (FloatAndFade (amountOfTime));
	}

	public IEnumerator FloatAndFade(float time) {
		yield return new WaitForSeconds (time);
		yield return null;
	}
	public void EstablishPlayerCam(Transform it)
	{
		playerCam = it;
	}
}

/*		
		Vector3 temp;
			temp = new Vector3(this.transform.position.x, this.transform.position.y + upIncrement, this.transform.position.z);
			this.transform.position = temp;

			//float alphaCalculation = (100*timer)/time;
			//print (alphaCalculation);

	
		timer += Time.deltaTime;*/