using UnityEngine;
using System.Collections;
     
public class ProximityXray : MonoBehaviour {
     
    public Transform player;
    Renderer render;
     
    void Start () {
     
        render = gameObject.GetComponent<Renderer>();
     
    }
         
    void Update () {
     
        render.sharedMaterial.SetVector("_PlayerPosition", player.position);
     
    } 
}