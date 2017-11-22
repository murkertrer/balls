using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreatureAttributes : MonoBehaviour {
	public AudioClip explosionSound;
	public GameObject explosionWhenDied;
	public Transform throwPoint;
	public float influenceRadius;
	public  Rigidbody rb;
	public Transform myAiArmor;
	public Transform myAiBall;
	public Transform myArm;
	public Transform myArmor;
	public float maxH = 100;
	float curH;
	public Image myHealthBar;
	public float initialXLocalScale;
	public int team = 2;
	public bool respawn = true;
	Vector3 initialPos;
	public GameObject eventManger;
	public Vector3 lclSale  = new Vector3 (.1f, .1f, .1f);
	public GameObject actualCreture;
	public GameObject HB;
	public GameObject actualHealthBar;
	public float healthBarHeight = 2;

	void OnEnable()
	{
		curH = maxH;
		//initialXLocalScale = myHealthBar.transform.localScale.x;
		//myArmor.GetComponent<SphereCollider> ().radius = influenceRadius;
		//initialPos = rb.transform.position;

		if (!eventManger) {
			CreateHealthBar ();	

		}
	}

	void CreateHealthBar()
	{
		if (!eventManger) {
			eventManger = GameObject.Find ("EventManager");
		}
		HB = GameObject.Instantiate (eventManger.GetComponent<KeepTrack> ().CreatureHealthBar.gameObject);
		//HB.transform.parent = eventManger.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;
		//HB.transform.localScale = lclSale;
		//HB.AddComponent<LookAtTransform> ();
		//HB.GetComponent<LookAtTransform> ().thingToLook = eventManger.GetComponent<KeepTrack> ().playerCamTransform;
		HB.transform.position = actualCreture.transform.position;
		HB.transform.SetParent(actualCreture.transform);
		HB.transform.localPosition = new Vector3 (0, healthBarHeight, 0);
		actualHealthBar = HB.GetComponent<LookAtPlayerCam>().actualHealthBar;
		initialXLocalScale = actualHealthBar.transform.localScale.x;
		//myHealthBar.transform.parent = eventManger.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;


	}
	public void TakeDamage(float amount)
	{
		curH -= amount;
		float calculateHealth = (curH / maxH )*initialXLocalScale;
		actualHealthBar.transform.localScale = new Vector3 (calculateHealth, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
		HealthActionNotification (amount, 1);
		if (respawn) {
			if (curH < 0) {
				Respawn ();
				myHealthBar.transform.localScale = new Vector3 (initialXLocalScale, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
				curH = maxH;
			}
		} else {
			if (curH < 0) {
				Destruction ();
			}
		}	
	}
	void Destruction()
	{
		Instantiate (explosionWhenDied, this.transform.position, Quaternion.identity);
		//AudioSource.PlayClipAtPoint (explosionSound, this.transform.position);
		Destroy (this.gameObject);
	}
	void Respawn()
	{

		rb.transform.position = initialPos;
		rb.velocity = Vector3.zero;
		this.transform.rotation = Quaternion.identity;

	}
	void Start () {
		eventManger = GameObject.Find ("EventManager");
	}
	public void HealthActionNotification(float amount, int it)
	{
		if (!eventManger) {
			eventManger = GameObject.Find ("EventManager");
		}
		GameObject go = new GameObject ("Cool GO by AI");
		go.transform.localScale	= lclSale;
		go.transform.parent = eventManger.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;
		go.transform.position = this.GetComponent<CreatureAttributes> ().myAiBall.transform.position;
		go.AddComponent<CanvasRenderer>();
		go.AddComponent<Text> ();
		if (it == 1) {
			go.GetComponent<Text> ().color = Color.red;
		}
		if (it == 2) {
			go.GetComponent<Text> ().color = Color.green;
		}
		go.GetComponent<Text> ().fontSize = 20;
		go.GetComponent<Text> ().font = eventManger.GetComponent<KeepTrack> ().thingsFont;
		go.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		amount = Mathf.Round (amount);
		go.GetComponent<Text> ().text = amount.ToString();
		go.AddComponent<TextNotificationFloat> ();
		go.AddComponent<TextNotificationFloat> ().doDouble = true;
		go.GetComponent<TextNotificationFloat> ().playerCam = eventManger.GetComponent<KeepTrack> ().playerCamTransform;
		go.GetComponent<TextNotificationFloat> ().DestroyText ();
		go.GetComponent<TextNotificationFloat> ().rotationReference = this.GetComponent<CreatureAttributes> ().myArmor;	;
	}
}
