using UnityEngine;
using System.Collections;

public class RtsTransparency : MonoBehaviour {


    public float DistanceToPlayer = 10.0f;
    public Transform parents;
    public Material transparent;
    public Transform myBall;
    public bool makeTransparent = true;
    float lastDistanceToPlayer =0;

   
    void OnEnable()
    {
        transparent = (Material)Resources.Load("Collider", typeof(Material));
    }


    public void GetLastDistanceToPlayer(float it) {
        lastDistanceToPlayer = it;
    }
    void Start()
    {
        //transparent.color = new Color(0.0f, 1.0f, 1.0f, 0.1f);

    }
    void Update()
    {
        if (makeTransparent)
        {
            RaycastHit[] hits;
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(transform.forward);
            hits = Physics.RaycastAll(this.transform.position, this.transform.forward,lastDistanceToPlayer, ~2);
            foreach (RaycastHit hit in hits)
            {


                if (hit.collider.name != "Terrain" && hit.transform.name != "Armor")
                {
                    if (hit.collider.transform.tag != "Player")
                    {

                        if (hit.collider.GetComponent<AutoTransparent>() == null)
                        {
                            AutoTransparent at = hit.transform.gameObject.AddComponent<AutoTransparent>() as AutoTransparent;
                            at.BeTransparent(transparent);
                        }
                        else
                        {
                            print("allready");
                            hit.collider.GetComponent<AutoTransparent>().BeTransparent(transparent);
                        }
                    }
                }
            }

            RaycastHit[] hitForAllReadyTransparent;
            Ray rayz = this.GetComponent<Camera>().ScreenPointToRay(this.transform.forward);
            hitForAllReadyTransparent = Physics.RaycastAll(this.transform.position, this.transform.forward , 50, 2);


            Debug.DrawRay(this.transform.position, this.transform.forward*20, Color.green);
            foreach (RaycastHit hit in hitForAllReadyTransparent)
            {
                if (hit.transform.GetComponent<AutoTransparent>())
                {
                    hit.transform.GetComponent<AutoTransparent>().BeTransparent(transparent);
                }
            }
        }
    }
}
