using UnityEngine;
using System.Collections;

public class InformAbout3dTerrain : MonoBehaviour {

	public Terrain3d t3d;
	Transform planeDirector;
	void Start () {
		//t3d = this.transform.parent.GetComponent<Terrain3d> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider col)
	{
		t3d.OnTriggerEnter2 (col);
	}
	void OnTriggerStay(Collider col)
	{
		t3d.OnTriggerStay2 (col);
	}
	void OnTriggerExit(Collider col)
	{
		t3d.OnTriggerExit2 (col);
	}
}
