using UnityEngine;
using System.Collections;

public class Terrain3d : MonoBehaviour {

	public string side;
	GameObject objTpSpawn;
	void Update()
	{
		Debug.DrawRay (this.transform.position, this.transform.up * 10, Color.red);
	}

	public void OnTriggerEnter2(Collider col)
	{
		if (col.tag != "Player")
		{
			if (!col.GetComponent<GenericSpaceManipulation> ()) {
				col.gameObject.AddComponent<GenericSpaceManipulation> ();
				col.GetComponent<GenericSpaceManipulation> ().EstablishPlane (this.transform);
			} 
			if (col.GetComponent<GenericSpaceManipulation>())
			{
				col.GetComponent<GenericSpaceManipulation> ().EstablishPlane (this.transform);
			}
		}
		else
		{
			if (col.transform.parent.GetComponent<SpaceManipulation> ()) {
				col.transform.parent.GetComponent<SpaceManipulation> ().ActivateArtificialGravity (this.transform, side);
			}
		}
	}

	public void OnTriggerStay2(Collider col)
	{
		/*
		if (col.tag != "Player")
		{
			if (!col.GetComponent<GenericFauxAttractor> () ) {
				col.transform.gameObject.AddComponent<GenericFauxAttractor> ();

			}
		}
		else
		{

		}
		*/
	}

	public void OnTriggerExit2(Collider col)
	{
		/*
		if (col.tag == "Player") {
			if (col.transform.parent.GetComponent<SpaceManipulation> ()) {
				col.transform.parent.GetComponent<SpaceManipulation> ().DeActivateArtificialGravity ();

			}
		}
		*/
	}
}
