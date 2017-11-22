using UnityEngine;
using System.Collections;

public class RtsAiming : MonoBehaviour {

    Vector3 target;
    Attributes at;
    KeyCode shifter;
    float flipSpeed = 300;
    float rotationTime = 2;
    public bool aimLerp;
    public bool canShoot;

    public LineRenderer aimLineRenderer;
    public LineRenderer aimLineRenderer2;
    public float widthOfDoubleShoot = 13;
    public bool onlyRight;
    public bool onlyLeft;
    public float lerpTimeLookAtPoint = .2f;

	bool angleShotinit;
	bool angle;
	bool height;
	public LineRenderer angleLR;
	AimingSystemSimpleDouble aSSD;
	float y;
	float x = 0;
	Vector3 baseOfAngle;
	Vector3 heightOfAngle;
	bool finishedEstablishingAngle;
	Vector3 forceIndicator;
	Vector3 offsetForPlayer;
	float definiteAimHeight;


    void Start()
    {
        at = GetComponent<Attributes>();
        shifter = at.shifter;
        aimLerp = at.aSSD.aimLerp;
        widthOfDoubleShoot = at.aSSD.widthOfDoubleShoot;
        lerpTimeLookAtPoint = at.aSSD.lerpTimeLookAtPoint;
		angleLR = at.angleLineRenderer;
		aSSD = at.aSSD;
}
    void Update()
    { 

        if (Input.GetKey(at.shifter))
	    {
            LookIfHit(target);
        }

		if (Input.GetKey(KeyCode.LeftControl))
		{
			AngleShot ();		
		}
		if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			//ResetAngleShot ();		
		}
    }

    public void LookAtPoint(Vector3 ito)
    {
        target = ito;
        at.rotator.transform.LookAt(ito);
        LookIfHit(ito);

        //StartCoroutine(Rotate());
    }
         //Fix Rotation; 
    IEnumerator Rotate()
    {   
        float curAngleY = at.rotator.eulerAngles.y;
        Quaternion start = at.rotator.transform.localRotation;
        Vector3 targetDir = target - at.rotator.position;        
        float angle = Vector3.Angle(targetDir, at.rotator.forward);
        //print(angle);
        //float elapsedTime = 0;         
        //float amount = curAngleY - angle;
        while (Mathf.Abs(curAngleY - angle) > 0.0001f)
        {
            Debug.DrawRay(at.rotator.position, targetDir * 5, Color.red);
            curAngleY = Mathf.MoveTowards(curAngleY, angle, Time.deltaTime * flipSpeed/2);
            //curAngleY = Mathf.Lerp (curAngleY, angle, Time.deltaTime * flipSpeed / 300);            
            //at.rotator.transform.localRotation = Quaternion.AngleAxis(curAngleY, at.rotator.up) * start;
            Quaternion transferToEuler = Quaternion.AngleAxis(curAngleY, at.rotator.up) * start;
            Vector3 eulered = transferToEuler.eulerAngles;
           at.rotator.transform.localEulerAngles = eulered;

            yield return null;
        }        
    }
    void LookIfHit(Vector3 ito)
    {
        Ray ray = new Ray(at.myCam.transform.position, (ito - at.myCam.transform.position));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000, ~2))
        {
            if (hit.transform != at.myBall)
            {
                //lookat
                if (!Input.GetMouseButton(1))
                {
                    if (this.GetComponent<Powers>().midAirRot == false)
                    {
                        Vector3 direction = hit.point - at.rotator.transform.position;
                        Quaternion totalrot = Quaternion.FromToRotation(at.rotator.forward, direction);
                        Quaternion justRot = Quaternion.LookRotation(direction);
                        if (at.cc.armorChild)
                        {
                            //rotWhenAiming= Quaternion.Slerp (at.rotator.transform.localRotation, justRot, Time.deltaTime*2);
                            at.rotator.transform.localRotation = Quaternion.Slerp(at.rotator.transform.localRotation, justRot, Time.deltaTime * 2);
                        }
                        else
                        {
                            if (this.GetComponent<Powers>().midAirRot == false) { }
                            /*
                            if (lookRotatorWhereHit)
                            {
                                //at.rotator.transform.localRotation = Quaternion.Slerp (at.rotator.transform.localRotation, justRot, Time.deltaTime * 1.2f);
                            }
                            */
                        }
                    }
                }
                Vector3 toTarget = (hit.point - at.forwardReference.position).normalized;
                if (Vector3.Dot(toTarget, at.forwardReference.forward) > 0)
                {
                    canShoot = true;
                    Vector3 directionToTarget = at.myBall.transform.position - hit.point;
                    float angle = Vector3.Angle(at.myBall.parent.GetComponent<Attributes>().rotator.right, directionToTarget);
                    if (Mathf.Abs(angle) > 90 + (widthOfDoubleShoot / 2))
                    {
                        DoTheAimThing(hit.point, 1);
                        onlyRight = true;
                        //StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm2, restingRot));
                        aimLineRenderer2.enabled = false;
                        print("R ok");
                        //Right Shoot
                    }
                    if (Mathf.Abs(angle) < 90 - (widthOfDoubleShoot / 2))
                    {
                        DoTheAimThing(hit.point, 2);
                        onlyLeft = true;
                        aimLineRenderer.enabled = false;
                        //StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm, restingRot));
                        //Left shoot
                    }
                    if (Mathf.Abs(angle) < 90 + (widthOfDoubleShoot / 2) && Mathf.Abs(angle) > 80 - (widthOfDoubleShoot / 2))
                    {
                        DoTheAimThing(hit.point, 3);
                        onlyLeft = false;
                        onlyRight = false;
                        //Both shhoot
                    }
                }
                else
                {
                    //StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm, restingRot));
                    //StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp*2, at.myArm2, restingRot));
                    at.aSSD.TurnOffBothLR();
                    canShoot = false;
                    //Behind Player
                }
            }
            else
            {
                // Hit Myself

                //StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp/3, at.myArm, originalRotR));
                //StartCoroutine (LookUpIfNoUse (lerpTimeArmLookUp/3, at.myArm2, originalRotL));
            }
        }
        else
        {
            at.aSSD.TurnOffBothLR();
        }
    }
    void DoTheAimThing(Vector3 it, int caseOfArm)
    {
        if (caseOfArm == 1)
        {
            Vector3 relativePosz = it - at.myArm.position;
            Quaternion desiredRotx = Quaternion.LookRotation(relativePosz);

            Quaternion desiredRotz = Quaternion.AngleAxis(at.rotator.localEulerAngles.y, at.forwardReference.up) * desiredRotx;
            if (!aimLerp)
            {
                at.myArm.transform.LookAt(it);
            }
            if (aimLerp)
            {
                Vector3 relativePos = it - at.myArm.position;
                Quaternion desiredRot = Quaternion.LookRotation(relativePos);
                at.myArm.transform.rotation = Quaternion.Lerp(at.myArm.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
            }
            if (Input.GetMouseButtonDown(0))
            {
                aimLineRenderer.enabled = true;
            }
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit2;
                Vector3 fwd = at.myArm.transform.TransformDirection(Vector3.forward);
                //Debug.DrawRay (at.myArm.position, at.myArm.forward, Color.red);
                if (Physics.Raycast(at.myArm.position, fwd, out hit2, 10000, ~2))
                {
                    aimLineRenderer.SetPosition(0, at.myArm.transform.position);
                    aimLineRenderer.SetPosition(1, hit2.point);
                }
                //aimLineRenderer.SetPosition (0, myArm.transform.position);
                //aimLineRenderer.SetPosition (1, it);
            }
            if (Input.GetMouseButtonUp(0))
            {
                aimLineRenderer.enabled = false;
            }
        }

        if (caseOfArm == 2)
        {
            Vector3 relativePosz = it - at.myArm2.position;
            Quaternion desiredRotz = Quaternion.LookRotation(relativePosz);
            /*
			bool checkY = CheckArmRotationY (desiredRotz.eulerAngles, "left");
			bool checkX = CheckArmRotationX (desiredRotz.eulerAngles);
			if (checkX && checkY) {	}
			*/
            if (!aimLerp)
            {
                at.myArm2.transform.LookAt(it);
            }
            if (aimLerp)
            {
                Vector3 relativePos = it - at.myArm2.position;
                Quaternion desiredRot = Quaternion.LookRotation(relativePos);
                at.myArm2.transform.rotation = Quaternion.Lerp(at.myArm2.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
            }
            if (Input.GetMouseButtonDown(0))
            {
                aimLineRenderer2.enabled = true;
            }
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit2;
                Vector3 fwd = at.myArm2.transform.TransformDirection(Vector3.forward);
                if (Physics.Raycast(at.myArm2.position, fwd, out hit2, 10000, ~2))
                {
                    aimLineRenderer2.SetPosition(0, at.myArm2.transform.position);
                    aimLineRenderer2.SetPosition(1, hit2.point);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                aimLineRenderer2.enabled = false;
            }

        }
        if (caseOfArm == 3)
        {
            if (!aimLerp)
            {
                at.myArm.transform.LookAt(it);
                at.myArm2.transform.LookAt(it);
            }
            if (aimLerp)
            {
                Vector3 relativePos = it - at.myArm.position;
                Quaternion desiredRot = Quaternion.LookRotation(relativePos);
                at.myArm.transform.rotation = Quaternion.Lerp(at.myArm.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
                Vector3 relativePos2 = it - at.myArm2.position;
                Quaternion desiredRot2 = Quaternion.LookRotation(relativePos);
                at.myArm2.transform.rotation = Quaternion.Lerp(at.myArm2.transform.rotation, desiredRot, Time.deltaTime * lerpTimeLookAtPoint);
            }

            if (Input.GetMouseButton(0))
            {
                aimLineRenderer.enabled = true;
                aimLineRenderer2.enabled = true;

                //Fix Here so that AimLineRenderer Does not Get blocked By One´s projectiles. 

                RaycastHit hit21;
                Vector3 fwd = at.myArm.transform.TransformDirection(Vector3.forward);
                if (Physics.Raycast(at.myArm.position, fwd, out hit21, 10000, ~2))
                {
                    aimLineRenderer.SetPosition(0, at.myArm.transform.position);
                    aimLineRenderer.SetPosition(1, hit21.point);
                }


                RaycastHit hit2;
                Vector3 fwd1 = at.myArm2.transform.TransformDirection(Vector3.forward);
                if (Physics.Raycast(at.myArm2.position, fwd1, out hit2, 10000, ~2))
                {
                    aimLineRenderer2.SetPosition(0, at.myArm2.transform.position);
                    aimLineRenderer2.SetPosition(1, hit2.point);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                aimLineRenderer.enabled = false;
                aimLineRenderer2.enabled = false;
            }
        }
    }
	void LookTowards( Vector3 ito)
	{
		Ray ray = new Ray(at.myCam.transform.position, (ito - at.myCam.transform.position));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 10000, ~2)) {
			at.rotator.LookAt (hit.point);
			//at.myArm.LookAt (hit.point);
		}
	}
	void AimTowards(Vector3 it)
	{
		at.myArm.transform.LookAt(it);

	}
	void AngleShot()
	{
		if (at.myRTSCam) {
			if (!angle && !finishedEstablishingAngle) {
				if (Input.GetMouseButtonDown (0)) {
					angleShotinit = true;
					angleLR.enabled = true;
					angleLR.SetVertexCount (2);			
				}
				if (Input.GetMouseButton(0)) {
					angleLR.SetPosition (0, at.myBall.transform.position);
					RaycastHit hit;
					Ray ray = at.myRTSCam.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out hit, 100.0f)) {
						//LookIfHit (hit.point);
						//Vector3 direction = (hit.point - at.myBall.transform.position).normalized;
						Vector3 direction = (hit.point - at.myBall.transform.position);
						//baseOfAngle = at.myBall.transform.position + (direction * 3);
						baseOfAngle = at.myBall.transform.position + (direction);
						offsetForPlayer = baseOfAngle - at.myBall.transform.position;
						//baseOfAngle = hit.point;
						angleLR.SetPosition (1, baseOfAngle);
						//LookIfHit (baseOfAngle);
						LookTowards (baseOfAngle);
					}	
				}
				if (Input.GetMouseButtonUp (0)) {
					angle = true;
					//angleLR.SetVertexCount (3);
					y = 0;
				}
			} else {
				/*
				y += Input.GetAxis ("Mouse Y") * .05f;
				heightOfAngle.y += y;

				if (heightOfAngle.y < at.myBall.transform.position.y) {
					heightOfAngle.y = at.myBall.transform.position.y;					
				}
				//Fix here arm angle
				at.myArm.LookAt (heightOfAngle);
				at.myArm2.LookAt (heightOfAngle);
				angleLR.SetPosition (2, heightOfAngle);	
				*/
				y += Input.GetAxis ("Mouse Y") * .01f;
				//int slices = 10;
			
				//int yQ = GetQuantized (y, slices);
				//angleLR.SetPosition (0, at.myBall.transform.position);
				//angleLR.SetPosition (1, baseOfAngle);

				int yQ = 10;			
				angleLR.SetVertexCount (2 + yQ);
				Vector3 center = at.transform.position;
				Vector3 direction = center - baseOfAngle;
				float radius = Vector3.Distance (center, baseOfAngle);
				for (int i = 0; i < yQ; i++) {
					Vector3 pos = Circle(center,radius, yQ, i, direction);
					angleLR.SetPosition (i + 1, pos);
				}


				/*
				for (int i = 0; i < yQ; i++){
					Vector3 pos = RandomCircle(center,radius, yQ);
					//Quaternion rot = Quaternion.FromToRotation(at.rotator.forward, center-pos);
					//Instantiate(prefab, pos, rot);
					angleLR.SetPosition (i+1, pos);	
				}
				*/


		
				if (Input.GetMouseButtonDown (0)) {

					print ("now");
					definiteAimHeight = heightOfAngle.y;
					angle = false;
					finishedEstablishingAngle = true;
					angleLR.SetVertexCount (4);	

				}				
			}
		}
	}

	Vector3 Circle (Vector3 center ,  float radius, int slices, int step, Vector3 direction)
	{
		float ang = 10*step;
		Vector3 pos;
		pos.x = baseOfAngle.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad)*-1;
		pos.y = baseOfAngle.y + radius * Mathf.Cos(ang*-1 * Mathf.Deg2Rad);
		pos.z = baseOfAngle.z;
		// How to twist this in the direction of the player?
		Vector3 total = pos;

		return total;
	}

	//total = Quaternion.AngleAxis (90, Vector3.right) * total;

	/*
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad)*-1;
		pos.y = center.y + radius * Mathf.Cos(ang*-1 * Mathf.Deg2Rad);
		pos.z = center.z;
		*/

	//total = Quaternion.AngleAxis (360-at.rotator.eulerAngles.y, Vector3.up)*total;
	int GetQuantized(float it, int slices)
	{
		if (it < .1) {
			return 1;
		}
		if (it < .2) {
			return 2;
		}
		if (it < .3) {
			return 3;
		}
		if (it < .4) {
			return 4;
		}
		if (it < .5) {
			return 5;
		}
		if (it < .6) {
			return 6;
		}
		if (it < .7) {
			return 7;
		}
		if (it < .8) {
			return 8;
		}
		return 9;




	}



	/*
	void Start() {
		Vector3 center = transform.position;
		for (int i = 0; i < numObjects; i++){
			Vector3 pos = RandomCircle(center, 5.0f);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
			Instantiate(prefab, pos, rot);
		}
	}
	*/









	void LateUpdate()
	{

		if (angleShotinit) {
			angleLR.SetPosition (0, at.myBall.transform.position);
			angleLR.SetPosition (1, at.myBall.transform.position+offsetForPlayer);
			if (angle) {
				//angleLR.SetPosition (2, heightOfAngle);	

				//angleLR.SetPosition (2, at.myBall.transform.position+offsetForPlayer+heightOfAngle);
			}
			if (finishedEstablishingAngle) {
				//angleLR.SetPosition (2, heightOfAngle);
				Vector3 theHeight  = at.myBall.transform.position+offsetForPlayer + new Vector3(0,definiteAimHeight,0);
				angleLR.SetPosition (2, theHeight);
				//angleLR.SetPosition (3, at.myBall.transform.position);
				LookIfHit (theHeight);

				if (Input.GetMouseButtonDown (0)) {				
					forceIndicator = theHeight;
				}
				if (Input.GetMouseButton(0))
				{
					//at.gTC.ChargesShotFct ();
					print ("getting");
					x += .1f;

					float totalDir = Vector3.Distance (at.myBall.position, theHeight);
					//float relation = (at.gTC.totalShot * totalDir) / at.gTC.maxChargedShot;
					Vector3 direction = at.myBall.transform.position - theHeight;
					//Vector3 newSpot = theHeight + (direction.normalized *relation);
					//angleLR.SetPosition (3,newSpot);
				}
				if (Input.GetMouseButtonUp (0)) {
					//at.gTC.ChargesShotFct ();
					x = 0;
					ResetAngleShot ();
				}
			}
		}
	}

	void ResetAngleShot()
	{
		print ("reseting");
		angle = false;
		height = false;
		angleLR.enabled = false;
		finishedEstablishingAngle = false;
	}


	public int numObjects = 10;
	public GameObject prefab;



}
