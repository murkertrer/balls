using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AbilityInfo : MonoBehaviour {

	//Add an Inspector Editing Mode?

	public List <GameObject> abilities = new List<GameObject> ();

	public GameObject ability;
	public Proyectiles ammo;

	public bool changeSize;
	public int type;
	public int maxAmmo;
	int curAmmo;
	public float fireRate;
	public  int expulsion;

	public string nameOfCurrentAbility;
	public string Expulsion;
	//public List<string> namesOfAbilities = new List<string>  ();
	//public List<string> namesOfExpulsion = new List<string>  ();
	public Color backgroundColor = Color.cyan;
	public Color textColor = Color.red;

	public Font myNewFont;
	public AudioClip pickUpSound;
	public int rotatingSpeed =100;
	public GameObject text;
	public GameObject EventManager;
	Transform playerCam;
	public float heightOfCanvas = 1.7f;
	bool CanvasHasBeenCalled;


	public Material ability1;
	public Material ability2;
	public Material ability3;
	public Material ability4;
	public Material ability5;
	public Material ability6;
	public Material ability7;
	public Material ability8;
	public Material ability10;


	public Vector2 rect= new Vector2 (60,10);
	public Vector3 lclSale  = new Vector3 (.05f, .05f, .05f);
	GameObject go;
	GameObject ga;
	void Start()
	{
		ability = abilities [type-1];
	}


	void MakePrettyText()
	{
		EventManager = GameObject.Find("EventManager");
		playerCam = EventManager.GetComponent<KeepTrack> ().playerCamTransform;
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
		go.GetComponent<Text> ().font = myNewFont;
		go.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		go.GetComponent<RectTransform> ().sizeDelta = rect;
		go.transform.localScale	= new Vector3(1,1,1);
		go.GetComponent<Text> ().text = nameOfCurrentAbility + "\n" + "MaxAmmo: "+ maxAmmo+"\n" + "Fire rate: " + fireRate + "\n"+  Expulsion;
		text = ga;
		CanvasHasBeenCalled = true;
	}
	public void RecreateCanvas()
	{
		if (CanvasHasBeenCalled == false) {
			MakePrettyText ();
		}
	}
	void OnEnable()
	{
		curAmmo = maxAmmo;
		//nameOfCurrentAbility = namesOfAbilities [type-1];
		//Expulsion =  namesOfExpulsion[expulsion-1];

		MakePrettyText ();
		//[EstablishAbilityMaterialCorrespondance ();
		Chamelon ();
	}
	/*
	void EstablishAbilityMaterialCorrespondance()
	{
		KeepTrack kt;
		kt = EventManager.GetComponent<KeepTrack> ();
		ability1 = kt.ability1;
		ability2 = kt.ability2;
		ability3 = kt.ability3;
		ability4 = kt.ability4;
		ability5 = kt.ability5;
		ability6 = kt.ability6;
		ability7 = kt.ability7;
		ability8 = kt.ability8;
		//ability9 = kt.ability9;
		ability10 = kt.ability10;


	}
	*/
	public void Chamelon()
	{

		if (changeSize) {
			Vector3 lcl = this.transform.localScale;
			lcl.y = maxAmmo / 30;
			this.transform.transform.localScale = lcl;

			Vector3 posY = this.transform.position;
			float y = this.GetComponent<Collider> ().bounds.extents.y;
			posY.y += y;
			this.transform.position = posY;
		}

		MeshRenderer mesh = this.GetComponent<MeshRenderer> ();
		if (type == 1) {
			mesh.material = ability1;
		}
		if (type == 2) {
			mesh.material = ability2;
		}
		if (type == 3) {
			mesh.material = ability3;
		}
		if (type == 4) {
			mesh.material = ability4;
		}
		if (type == 5) {
			mesh.material = ability5;
		}
		if (type == 6) {
			mesh.material = ability6;
		}
		if (type == 7) {
			mesh.material = ability7;
		}
		if (type == 8) {
			mesh.material = ability8;
		}
		if (type == 10) {
			mesh.material = ability10;
		}

	}
	void Update () {
		this.transform.Rotate (Vector3.up * Time.deltaTime * rotatingSpeed);
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
	void OnTriggerEnter(Collider colo)
	{
		if (colo.tag == "Player") {
			if (colo.transform.root.GetComponent<AbilitySelection> ()) {
				if (colo.GetComponent<Rigidbody> ()) {
					//colo.transform.root.GetComponent<AbilitySelection> ().CheckIfWeAllreadyHaveThisAbility (type, maxAmmo, curAmmo, expulsion, nameOfCurrentAbility, ability);
					//colo.transform.root.GetComponent<AbilitySelection> ().CheckIfWeHaveIt (ability);


					Destroy (text);
					Destroy (this.gameObject);
				}
			}
		}
	}
	public void DestroyAbilityInfo()
	{
		Destroy (text);
	}

}
