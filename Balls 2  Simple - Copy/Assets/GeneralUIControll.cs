using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneralUIControll : MonoBehaviour {

	public GameObject RtsBackground;
	public float RtsCanvasHeight;
	//GameObject FPSCanvas;
	public GameObject playerDetails;
	public GameObject abilityDetails;
	public GameObject aimingSystem;

	KeepTrack kT;
	public Color notificationColor = Color.yellow;
	bool dimNotifyText;
	public string notificationTextContent = "Teste1";
	public float fadeTime = .7f;
	public float durationOfNotification = 2f;
	public Text notificationText;
	public 	float curH;
	public float maxH;
	public float curM;
	public float maxM;

	public Image myPowerBar;
	public Image myHealthBar;
	public Image myAngleBar;
	public Image myManaBar;

	public Text myTextHealth;
	public Text myTextPower;
	public Text myTextAngle;
	public Text myTextProyectile;
	public Text myHealthInText;
	public Text myManaInText;

	public void TurnOffFPSUI()
	{
		ShowRtsHUD();
		playerDetails.SetActive(false);
		abilityDetails.SetActive(false);
		aimingSystem.SetActive(false);
	}
	public void TurnOnFPSUI()
	{
		HideRtsHUD();
		playerDetails.SetActive(true);
		abilityDetails.SetActive(true);
		aimingSystem.SetActive(true);
	}



	public void UpdateHealthUI(float  cur, float max)
	{
		//curH = at.currentHealth;

		//float curH = Mathf.Round(curH);
		//maxH = this.transform.GetComponent<Attributes> ().maxHealth;
		//maxH = Mathf.Round (maxH);

		float calculateHealth = cur / max;
		if (!System.Single.IsNaN (calculateHealth)) {
			myHealthInText.text =  curH+ " / " + maxH;
			myHealthBar.transform.localScale = new Vector3 (calculateHealth, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
		}
	}
	void OnEnable()
	{
		kT = GetComponent<KeepTrack> ();
		ScaleCanvasToFitCurrentScreen ();
	}


	void ScaleCanvasToFitCurrentScreen()
	{
		RtsCanvasHeight = RtsBackground.transform.position.y;
		RectTransform rt = RtsBackground.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2 (Screen.width, rt.sizeDelta.y);
	}

	void HideRtsHUD()
	{
		StartCoroutine (HideHud ());
	}

	void ShowRtsHUD()
	{
		StartCoroutine (ShowHud ());
	}


	IEnumerator HideHud()
	{
		float belowY = RtsCanvasHeight -500;
		bool firstSwtich = false;
		float time = kT.transferSubjectivityTime;
		float elapsedTime = 0;
		while (elapsedTime < time) {
			RtsBackground.transform.position = Vector3.Lerp (RtsBackground.transform.position
				, new Vector3 (RtsBackground.transform.position.x, belowY, RtsBackground.transform.position.z)
				, (elapsedTime / time));

			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return null;
	}
	IEnumerator ShowHud()
	{
		RtsCanvasHeight = Screen.height / 30;
		bool firstSwtich = false;
		float time = kT.transferSubjectivityTime;
		float elapsedTime = 0;
		while (elapsedTime < time) {
			RtsBackground.transform.position = Vector3.Lerp (RtsBackground.transform.position
				, new Vector3 (RtsBackground.transform.position.x, RtsCanvasHeight, RtsBackground.transform.position.z)
				, (elapsedTime / time));

			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return null;
	}






	void Start()
	{
		notificationText = kT.notificationCenter;
		Notification (notificationTextContent);
		myHealthInText = kT.healthTxt;
		//boomerangInfo = kT.BoomerangInfo;
		//myCurrentShotAmmo = kT.curAmmo;
		//myCurrentShotText = kT.currentShot;
		//myCurrentFireRate = kT.fireRate;
		myAngleBar = kT.angleBar;
		myHealthBar = kT.healthBar;
		myHealthInText = kT.healthTxt;
		myTextAngle = kT.angleTxt;
		myManaBar = kT.manaBar;
		myManaInText = kT.manaText;
		notificationText = kT.notificationCenter;
	}
	void Update () {
		//UpdateHealthUI ();
		//UpdateManaUI ();
		//AngleCalculation ();
		//UpdateAngleUi ();
	}
	public void UpdateManaUI ()
	{
		curM = Mathf.Round (this.transform.GetComponent<Attributes> ().currentMana);
		maxM = Mathf.Round (this.transform.GetComponent<Attributes> ().maxMana);
		float calculataMana = curM / maxM;
		myManaInText.text = curM +" / " + maxM;
		if (!System.Single.IsNaN (calculataMana)) {
			myManaBar.transform.localScale = new Vector3 (calculataMana, myManaBar.transform.localScale.y, myManaBar.transform.localScale.z);
		}
	}
	public void UpdateAngleUi (float anglez)
	{
		if (anglez <= 90) {
			myTextAngle.text = anglez.ToString ();
			if (!System.Single.IsNaN (anglez)) {
				float equalized = anglez / 90;
				if (equalized < 91) {
					myAngleBar.transform.localScale = new Vector3 (equalized, myAngleBar.transform.localScale.y, myAngleBar.transform.localScale.x);
				}
			}
		} 
		if (anglez > 90) {
			float tempAng = 360 - anglez;
			if (!System.Single.IsNaN (anglez)) {
				myTextAngle.text = tempAng.ToString ();
				float equalized = tempAng / 90;
				myAngleBar.transform.localScale = new Vector3 (equalized, myAngleBar.transform.localScale.y, myAngleBar.transform.localScale.x);
			}
		}
	}
	public void TurnOnFPSCanvas()
	{
		//print ("case !");
		//kT.FPSCanvas.SetActive (true);
	}
	public void TurnOffFPSCanvas()
	{
		//kT.FPSCanvas.SetActive (false);
	}
	public void Notification(string it)
	{
		StartCoroutine(FadeNotificationText (durationOfNotification, it));
	}
	public void NotificationOut()
	{
		StartCoroutine(FadeOut ());
	}
	public IEnumerator FadeNotificationText(float time, string message)
	{
		notificationText.color = notificationColor;
		notificationText.text = message;
		StartCoroutine(FadeIn ());
		yield return new WaitForSeconds(time);
		StartCoroutine(FadeOut ());
		yield return null;
	}
	public IEnumerator FadeIn()
	{
		float elapsedTime = 0;
		Color it = notificationText.color;
		while (elapsedTime < fadeTime) {
			it.a = (elapsedTime / fadeTime);
			notificationText.color = it;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	public IEnumerator FadeOut()
	{
		float elapsedTime = 0;
		Color it = notificationText.color;
		while (elapsedTime < fadeTime) {
			it.a = 1 - (elapsedTime / fadeTime) ;
			notificationText.color = it;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
