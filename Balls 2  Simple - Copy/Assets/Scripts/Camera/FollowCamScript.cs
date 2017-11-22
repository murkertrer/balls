using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowCamScript : MonoBehaviour {

	public float DistanceToPlayer = 10.0f;
	public Transform parents;
	public Material transparent;
	public Transform myBall;
	public bool makeTransparent;

	void Start()
	{
		transparent.color  = new Color (0.0f, 1.0f, 1.0f, 0.1f); 

	}
	void Update()
	{
		if (makeTransparent) {
			RaycastHit[] hits;
			Ray ray = this.GetComponent<Camera> ().ScreenPointToRay (parents.GetComponent<Attributes> ().myBall.transform.position);
			hits = Physics.RaycastAll (this.transform.position, parents.GetComponent<Attributes> ().myBall.transform.position - this.transform.position, 5.5f, ~2);
			foreach (RaycastHit hit in hits) {

				if (hit.collider.name != "Terrain"  && hit.transform.name != "Armor") {
					if (hit.collider.transform.tag != "Player") {
						
						if (hit.collider.GetComponent<AutoTransparent> () == null) {
							AutoTransparent at = hit.transform.gameObject.AddComponent<AutoTransparent> () as AutoTransparent;
							at.BeTransparent (transparent);
						} else {
							hit.collider.GetComponent<AutoTransparent> ().BeTransparent (transparent);
						}
					}
				}
			}
			RaycastHit[] hitForAllReadyTransparent;
			Ray rayz = this.GetComponent<Camera> ().ScreenPointToRay (parents.GetComponent<Attributes> ().myBall.transform.position);
			hitForAllReadyTransparent = Physics.RaycastAll (this.transform.position, parents.GetComponent<Attributes> ().myBall.transform.position - this.transform.position, 5.5f, 2);
			foreach (RaycastHit hit in hitForAllReadyTransparent) {
				if (hit.transform.GetComponent<AutoTransparent>())
				{
					hit.transform.GetComponent<AutoTransparent> ().BeTransparent (transparent);
				}
			}
		}
	}
}
