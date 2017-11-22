using UnityEngine;
using System.Collections;

public class SpaceManipulation : MonoBehaviour {

	public FauxGravityAttractor attractor;
	Transform forceThatAttracts = null;
	public Transform armorFlip = null;
	public bool artificialGravity;
	public bool planetaryGravity;
	public bool planeGravity;
	Attributes at;
	Transform armor;
	Transform ball;
	float lerpSpeed = 3;
	PlanetPower pp = null;
	public Transform currentPlanet;
	bool currentlyRotating;
	public bool planeOrientation;
	public Transform planeReference;
	public Vector3 currentNormal = Vector3.zero;
    public Vector3  currentAtracctionNormal  = Vector3.zero;
	public bool rotating;
	public float anglex;
	public float objFaceUp;
	public float objFaceFront;
	public Vector3 backLeft;
	public Vector3 backRight;
	public Vector3 frontLeft;
	public Vector3 frontRight;
	public RaycastHit lr;
	public RaycastHit rr;
	public RaycastHit lf;
	public RaycastHit rf;
	public Vector3 upDir;
    Quaternion desiredRot = Quaternion.identity;
    float maxDistanceForRaycastAttraction = 5;
    public float incorporationTime = .6f;
    bool rotatingToFitWorld;
    void OnEnable()
	{
		at = this.GetComponent<Attributes> ();
		ball = at.myBall;
		armor = at.myArmor;
	}
	public void ActivatePlanetaryGravity(Transform planet)
	{
		//currentPlanet = planet;
		// = it;
		///forceThatAttracts = planet;
		//ball.GetComponent<Rigidbody>().useGravity = false;
		//planetaryGravity = true;
	}	
	public void ActivatePlaneGravity( Transform reference)
	{
        if (!planeOrientation)
        { 
		    //StartCoroutine(RotateToPlane (reference, 2));
		    planeReference = reference;
		    planeOrientation = true;
		    at.myBall.GetComponent<Rigidbody> ().useGravity = false;		   
        }
        RaycastHit hit;
        Vector3 direction = (planeReference.position - at.myBall.transform.position);
        Debug.DrawRay(at.myBall.transform.position, direction * 10, Color.yellow);
        if (Physics.Raycast(at.myBall.transform.position, direction, out hit, 20))
        {
            if (hit.normal != currentNormal)
            {
                if (!rotating)
                {
                    // ResetPlanes();
                    currentNormal = hit.normal;
                    // StartCoroutine (RotateToPlane (currentNormal,  1.2f, hit.transform));
                }
            }
        }
        desiredRot = Quaternion.FromToRotation(at.myArmor.up, hit.normal);
    }
    public void DeActivateArtificialGravity()
    {
        attractor = null;
        artificialGravity = false;
        ball.GetComponent<Rigidbody>().useGravity = true;
        planetaryGravity = false;
        artificialGravity = false;
        planeOrientation = false;
    }
    IEnumerator RotateToPlane(float time)
    {
        rotating = true;
        float elapsedTime = 0;
        Quaternion newRot = Quaternion.identity;
        newRot = Quaternion.FromToRotation(at.myArmor.up, currentNormal);
        while (elapsedTime < time)
        {
            at.myArmor.localRotation = Quaternion.Lerp(at.myArmor.localRotation, newRot, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rotating = false;
    }
    IEnumerator RotateToPlane(Transform referenced, float time)
    {
        Quaternion desiredRot = Quaternion.FromToRotation(at.myArmor.up, currentNormal.normalized);
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            //armor.localRotation = Quaternion.Slerp (armor.localRotation, desiredRot, (elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        print("should have new rot");
        planeOrientation = true;
    }


	public void ActivateArtificialGravity(Transform planet, string side)
	{
		attractor = planet.GetComponent<FauxGravityAttractor> ();
		ball.GetComponent<Rigidbody>().useGravity = false;
		StartCoroutine (RotationNewPlane (attractor.transform, 1.5f, side));
		planeGravity = true;
		artificialGravity = true;
	}
	IEnumerator RotationNewPlane(Transform newReference, float time, string side)
	{
		float elapsedTime = 0;
		//Quaternion startingRot = armor.transform.rotation;
		//armor.transform.rotation = attractor.transform.rotation * Quaternion.AngleAxis (180, Vector3.forward);
		//newRot = newRot *Quaternion.AngleAxis(180, Vector3.forward);

		Quaternion targetRotation = Quaternion.FromToRotation (armor.transform.up, newReference.up*-1);
		Vector3 v = newReference.transform.rotation.eulerAngles;

		if (side == "right" || side == "left") {

			Vector3 relativePos = newReference.transform.position - ball.transform.position;
			Quaternion rot1 = newReference.transform.rotation;
			Quaternion rot2 =Quaternion.LookRotation(newReference.GetComponent<Renderer>().bounds.center);
			Quaternion it = Quaternion.FromToRotation(rot1.eulerAngles, rot2.eulerAngles);

			/*
			Quaternion r = Quaternion.Euler (v.x, armor.transform.eulerAngles.y, armor.transform.eulerAngles.z);
			*/
			while (elapsedTime < time) {
				armor.transform.localRotation= Quaternion.Lerp (armor.transform.rotation, it, (elapsedTime / time));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}

		if (side == "back" ) {
			Quaternion r = Quaternion.Euler (90, 90, v.z);
			while (elapsedTime < time) {
				armor.transform.localRotation= Quaternion.Lerp (armor.transform.rotation, r, (elapsedTime / time));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
	}
	public void RotateToWorld ()
	{
        rotatingToFitWorld = true;
        StartCoroutine (RotateToFitWorld (incorporationTime));
	}
	public IEnumerator RotateToFitWorld(float time)
	{
		at.myBall.GetComponent<Rigidbody> ().useGravity = true;
        Quaternion desiredRot = Quaternion.identity;
		float elapsedTime = 0;
		while (elapsedTime < time) {      
			armor.localRotation = Quaternion.Slerp (armor.localRotation, desiredRot, (elapsedTime/time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
        rotatingToFitWorld = false;
    }
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DeActivateArtificialGravity();
            StartCoroutine(RotateToFitWorld(incorporationTime));
        }
        //  float angleX = Mathf.LerpAngle(armor.transform.eulerAngles.x, attractor.transform.eulerAngles.x, Time.deltaTime * lerpSpeed);
        //  armor.transform.localEulerAngles = new Vector3(angleX, armor.transform.eulerAngles.y, armor.transform.eulerAngles.z);
    }
    void FixedUpdate()
    {
        if (artificialGravity)
        {
            if (attractor && planetaryGravity)
            {
                Vector3 gravityUp = (armor.transform.position - attractor.transform.position).normalized;
                Vector3 localUp = armor.transform.up;
                ball.GetComponent<Rigidbody>().AddForce(attractor.transform.up * 9.8f);
            } 
        }

        if (planeOrientation)
        {
            RaycastHit hit;
            Vector3 direction = (planeReference.position - at.myBall.transform.position);
            if (Physics.Raycast(at.myBall.transform.position, direction, out hit, 100))
            {
                at.myArmor.localRotation = Quaternion.Lerp(at.myArmor.localRotation, desiredRot, Time.deltaTime * 5);
                ball.GetComponent<Rigidbody>().AddForce(hit.normal * -9.8f);
            }
        }
        if (planetaryGravity)
        {
            if (!currentlyRotating)
            {
                /*
				Vector3 direction = (currentPlanet.transform.position - at.myBall.transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation (direction);
				at.myArmor.transform.rotation = lookRotation;
				*/
                //currentPlanet.GetComponent<PlanetPower> ().Attract(at.myBall);
            }
        }
    }
}


