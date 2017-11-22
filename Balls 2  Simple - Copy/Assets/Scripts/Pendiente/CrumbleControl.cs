using UnityEngine;
using System.Collections;

public class CrumbleControl : MonoBehaviour {
	public int numberOfCrumz;
	public int maxCurmz;
	public Transform masterControler;
	public void RegisterCrumz()
	{
		numberOfCrumz += 1;
		maxCurmz = numberOfCrumz;
	}
	public void DecreaseCrumz()
	{
		numberOfCrumz -= 1;

	}

	void Update()
	{

		if (numberOfCrumz < Mathf.Round (maxCurmz / 2)) {
			//BroadcastMessage ("ParentSaysEnough");
			//print ("it");
			masterControler.GetComponent<Powers>().shieldOnePlaced = false;

			//Destroy(this.gameObject);
		}
	}
}
