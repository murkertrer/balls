using UnityEngine;
using System.Collections;

public class RtsConditions : MonoBehaviour {
    KeepTrack  kT;

    Transform targetPlayer;
    float distanceToLastPlayer;
    public Transform camDir;

	void OnEnable () {
        kT = GameObject.Find("EventManager").GetComponent<KeepTrack>();
        //gameObject.AddComponent<RtsScroll>();
        gameObject.AddComponent<RtsCameraControll>();
        gameObject.AddComponent<RtsTransparency>();
        gameObject.AddComponent<RtsOrders>();
        gameObject.AddComponent<Camera>();
		kT.kingCamera = gameObject.GetComponent<Camera>();
        gameObject.AddComponent<UnitSelectionComponent>();
        gameObject.GetComponent<UnitSelectionComponent>().selectionCirclePrefab = kT.SelectionCirclePre;
    }

    public void SetOrientationFromLastPlayer(Transform target, Vector3 pos, Material mat)
    {
        targetPlayer = target;
        this.transform.LookAt(target);
        this.transform.position = pos;
        float distanceToPlayer = Vector3.Distance(this.transform.position, target.position);
        gameObject.GetComponent<RtsTransparency>().GetLastDistanceToPlayer(distanceToPlayer);
        gameObject.GetComponent<RtsTransparency>().transparent = mat;
        this.transform.LookAt(targetPlayer);
    }
    public void SetCamDir(Transform it)
    {
        gameObject.GetComponent<RtsCameraControll>().GetCameraDirector(it);
		kT.RTSCamDirector = it;

		//kT.kingCamera = it.GetComponent<Camera>();
    }
    public void SelectedObjectsWhenSpawn(SelectableUnitComponent it)
    {
        gameObject.GetComponent<UnitSelectionComponent>().GetWakeUpUnits(it);
    }
}
