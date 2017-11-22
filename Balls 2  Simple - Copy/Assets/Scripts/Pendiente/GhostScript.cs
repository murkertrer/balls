using UnityEngine;
using System.Collections;

public class GhostScript : MonoBehaviour {
	// Use this for initialization
	public Transform player;
	MeshRenderer render;

	void Start () {

		render = gameObject.GetComponent<MeshRenderer>();

	}          
	void Update () {

		render.sharedMaterial.SetVector ("_PlayerPosition", player.position);

	}
}