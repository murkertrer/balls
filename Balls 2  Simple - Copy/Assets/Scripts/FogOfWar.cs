using UnityEngine;
using System.Collections;

public class FogOfWar : MonoBehaviour {
	
	void OnTriggerEnter(Collider col)
	{
		//col.GetComponent<MeshRenderer> ().enabled = true;
	}

	void OnTriggerExit(Collider col)
	{
		//col.GetComponent<MeshRenderer> ().enabled = false;
	}

}
