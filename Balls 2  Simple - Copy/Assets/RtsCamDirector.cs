using UnityEngine;
using System.Collections;

public class RtsCamDirector : MonoBehaviour {


    bool onlyY = true;
    void LateUpdate()
    {
        Vector3 desiredAngleMimic = new Vector3(0, 0, 0);

        if (onlyY)
        {
            desiredAngleMimic.y = transform.parent.eulerAngles.y;
        }
    }
}
