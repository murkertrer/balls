using UnityEngine;
using System.Collections;

public class CreateTriggerFor3dBaseOnObject : MonoBehaviour {

	void Start () {

		GameObject go = new GameObject ("Trigger hi");
		go.transform.parent = this.transform;
		go.transform.localScale = new Vector3 (1, 1, 1);
		go.transform.position = this.transform.position + this.transform.up*2;
		go.AddComponent<BoxCollider> ();
		go.GetComponent<BoxCollider> ().isTrigger = true;
		go.AddComponent<InformAbout3dTerrain> ();
		go.GetComponent<InformAbout3dTerrain> ().t3d = this.GetComponent<Terrain3d> ();
		go.transform.rotation = this.transform.rotation;
		go.layer = LayerMask.NameToLayer ("TransparentFX");

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
