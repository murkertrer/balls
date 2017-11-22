using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextureChanger : MonoBehaviour {
	public List<Material> m = new List<Material> ();
	public List<Material> m2 = new List<Material> ();



	public int selection;
	public int selection2;

	// Use this for initialization

	public bool isForBall;
	public bool isForArm;


	//cavnas
	GameObject go;
	GameObject ga;
	public GameObject EventManager;
	public float heightOfCanvas = 1.7f;
	public Vector3 lclSale  = new Vector3 (.05f, .05f, .05f);

	public List<string> namesOfExpulsion = new List<string>  ();
	public Color backgroundColor = Color.cyan;
	public Color textColor = Color.red;
	public Vector2 rect= new Vector2 (60,10);
	public Font myNewFont;
	public string firstLine;
	public string secondLine;
	bool switchi;

	void OnTriggerEnter(Collider col)
	{
		print ("trigger in");
		if (isForBall) {
			col.GetComponent<MeshRenderer> ().material = m [selection];
		} 

		if (isForArm) {
			print ("trying to change arm");
			if (col.transform.parent.GetComponent<Attributes> ()) {
				//col.transform.parent.GetComponent<Attributes> ().myArm.GetComponent<MeshRenderer> ().material = m2 [selection];
				col.transform.parent.GetComponent<Attributes> ().myArm.GetComponent<MeshRenderer> ().material = this.gameObject.GetComponent<MeshRenderer> ().material;

			}

		}
		
	}

	void OnEnable()
	{
		EventManager = GameObject.Find("EventManager");
		if (isForBall) {
			
			StartCoroutine (ChangeTexture ());
		} else {
			StartCoroutine (ChangeTexture2 ());

		}
		MakeCanvasInfo ();
	}


	void Update()
	{
		this.transform.Rotate (Vector3.up, 60 * Time.deltaTime);
		if (!switchi)
		{
			if (EventManager.transform.GetComponent<KeepTrack> ().playerCamTransform) {
				ga.AddComponent<LookAtTransform> ();
				ga.GetComponent<LookAtTransform> ().thingToLook = EventManager.transform.GetComponent<KeepTrack> ().playerCamTransform;
				switchi = true;
			}
		}

	}

	IEnumerator ChangeTexture()
	{
		
		this.GetComponent<MeshRenderer> ().material = m [selection];
		yield return new WaitForSeconds (3);
		if (selection < m.Count-1) {
			selection += 1;
		} else {
			selection = 0;
		}

		StartCoroutine(ChangeTexture ());

	}
	IEnumerator ChangeTexture2()
	{

		this.GetComponent<MeshRenderer> ().material = m2 [selection2];
		yield return new WaitForSeconds (3);
		if (selection2 < m.Count-1) {
			selection2 += 1;
		} else {
			selection2 = 0;
		}

		StartCoroutine(ChangeTexture2 ());

	}

	void MakeCanvasInfo()
	{

		go= new GameObject ("TexForAbilityAndWorldCanvas");
		ga = new GameObject ("ImgBackg");

		ga.transform.position = this.transform.position + new Vector3 (0, heightOfCanvas+ this.GetComponent<MeshRenderer>().bounds.extents.magnitude, 0);
		ga.transform.Rotate (Vector3.up, 180);
		ga.transform.parent = EventManager.transform.GetComponent<KeepTrack> ().worldCanvaz.transform;
		ga.AddComponent<CanvasRenderer>();
		ga.AddComponent<Image> ();
		backgroundColor.a = .5f;
		ga.GetComponent<Image> ().color = backgroundColor;
		ga.GetComponent<RectTransform> ().sizeDelta = rect;
		ga.transform.localScale	= lclSale;










		go.transform.position = ga.transform.position;
		go.transform.parent = ga.transform;
		go.AddComponent<CanvasRenderer>();
		go.AddComponent<Text> ();
		go.GetComponent<Text> ().fontSize = 20;
		go.GetComponent<Text> ().color = textColor;
		go.GetComponent<Text> ().font = myNewFont;
		go.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		go.GetComponent<Text> ().text = firstLine + "\n" + secondLine;
		go.transform.localScale	= lclSale;


	}


}
