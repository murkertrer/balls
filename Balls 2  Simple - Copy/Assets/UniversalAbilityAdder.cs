using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class UniversalAbilityAdder : MonoBehaviour {
	KeepTrack kt;
	GameObject go;
	GameObject ga;
	public AudioClip pickUpSound;
	public int rotatingSpeed =100;
	public GameObject text;
	public GameObject EventManager;
	Transform playerCam;
	public float heightOfCanvas = 1.7f;
	bool CanvasHasBeenCalled;
	public Color backgroundColor = Color.cyan;
	public Color textColor = Color.red;
	public Vector2 rect= new Vector2 (60,10);
	public Vector3 lclSale  = new Vector3 (.05f, .05f, .05f);
	public string nameOfCurrentAbility;

	public int maxAmmo;
	public float fireRate;
	public string Expulsion;

	public List <Material> abilitiesMaterials  = new List<Material>();



	public enum TypeOf{ 
		PopcornMadness,


		FireCrackerCharged,
		FireCrackerContinuous

	}
	public TypeOf typeOf;


	void OnEnable()
	{
		MakePrettyText ();


	}
	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * rotatingSpeed);
	}
	void LateUpdate()
	{
		if (playerCam) {
			text.transform.position = this.transform.position + new Vector3 (0, heightOfCanvas, 0);
			text.transform.LookAt (2 * this.transform.position - playerCam.transform.position);
		} else {
			playerCam = EventManager.GetComponent<KeepTrack> ().playerCamTransform;
		}
	}
	void MakePrettyText()
	{
		NameAbilityDependingOnType ();

		EventManager = GameObject.Find("EventManager");
		kt = EventManager.GetComponent<KeepTrack> ();
		playerCam = kt.playerCamTransform;
		ga = new GameObject ("ImgBackg");
		ga.transform.position = this.transform.position + new Vector3 (0, heightOfCanvas, 0);
		ga.transform.parent = EventManager.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;
		ga.AddComponent<CanvasRenderer>();
		ga.AddComponent<Image> ();
		backgroundColor.a = .5f;
		ga.GetComponent<Image> ().color = backgroundColor;
		ga.GetComponent<RectTransform> ().sizeDelta = rect;
		ga.transform.localScale	= lclSale;
		go= new GameObject ("TexForAbilityAndWorldCanvas");
		go.transform.position = ga.transform.position;
		go.transform.parent = ga.transform;
		go.AddComponent<CanvasRenderer>();
		go.AddComponent<Text> ();
		go.GetComponent<Text> ().fontSize = 20;
		go.GetComponent<Text> ().color = textColor;
		go.GetComponent<Text> ().font = kt.abilitiesDescriptionFont;
		go.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		go.GetComponent<RectTransform> ().sizeDelta = rect;
		go.transform.localScale	= new Vector3(1,1,1);
		go.GetComponent<Text> ().text = nameOfCurrentAbility + "\n" + "MaxAmmo: " + maxAmmo + "\n" + "Fire rate: " + fireRate;
		text = ga;
		CanvasHasBeenCalled = true;
	}
	void OnTriggerEnter(Collider colo)
	{
		if (colo.tag == "Player") {
			if (colo.transform.root.GetComponent<AbilitySelection> ()) {
				if (colo.GetComponent<Rigidbody> ()) {

					if (typeOf == TypeOf.FireCrackerCharged) {
						colo.transform.root.GetComponent<AbilitySelection> ().CheckIfWeHaveIt (kt.fireCrackerChargedAbility, nameOfCurrentAbility, fireRate, maxAmmo);
					}
					if (typeOf == TypeOf.FireCrackerContinuous) {
						colo.transform.root.GetComponent<AbilitySelection> ().CheckIfWeHaveIt (kt.fireCrackerContinuousAbility, nameOfCurrentAbility, fireRate, maxAmmo);
					}
					if (typeOf == TypeOf.PopcornMadness) {
						colo.transform.root.GetComponent<AbilitySelection> ().CheckIfWeHaveIt (kt.popcornMadnessAbility, nameOfCurrentAbility, fireRate, maxAmmo);

					}


					Destroy (text);
					Destroy (this.gameObject);
				}
			}
		}
	}
	/*
	void AddAppropiateAbility(GameObject it)
	{
		
		if (typeOf == TypeOf.FireCrackerCharged) {
			it.AddComponent<ChargedShot> ();
			it.GetComponent<ChargedShot> ().SetAbilityCharacteristics (fireRate, maxAmmo, kt.firecracker);
			//gameObject.GetComponent<ChargedShot> ().proyectile = testo;
		}

	}
	*/

	void NameAbilityDependingOnType()
	{

		MeshRenderer mesh = this.GetComponent<MeshRenderer> ();


		if (typeOf == TypeOf.FireCrackerCharged) {
			nameOfCurrentAbility = "Charged Firecracker";
		}
		if (typeOf == TypeOf.FireCrackerContinuous) {
			nameOfCurrentAbility = "Continuous Firecracker";
			//mesh.material = abilitiesMaterials [2];		
		}
		if (typeOf == TypeOf.PopcornMadness) {
			nameOfCurrentAbility = "PopcornMadness";

		}

	
	}

	TypeOf a ()
	{
		return TypeOf.FireCrackerCharged;
	}
}
