using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GTCBackUPxxxx : MonoBehaviour {
	public bool automaticArm;
	public List<IEnumerator> automaticShotsPending = new List<IEnumerator> ();
	public int numberOfShotsPerHommingReset;
	public int numberShotsReset = 2;
	public float automaticShootTime =1;
	public bool absoluteVelocity;
	public float relativeVelocity = 3000;
	public float fixedShotSUltra = 8;
	public float fixedShotStrenght = 15;
	public float maxFixedShot = 60;	
	public float minFixedShot = 10;	
	public float forceMultiplier = 2;
	public float initialFixedShotStrenght;
	public float shotForce;
	public float strenghtMultiplier = 10;
	public int genericCurrentAmmo;
	public int genericMaxAmmo;
	public float genericNextFire;
	public float genericCurrentShotType;
	public int genericExpel;
	public float genericFireRate;
	public float totalShot;
	public float maxChargedShot = 50;
	public float genericNextFireLeft;
	public float genericNextFireRight;

	float startOfShot = 0;
	float endOfShot;
	bool mouseDownOnTime;

	float disto = 5;
	float withdo = 2;
	float timeo = 3;
	float yForBoomerang;

	Attributes at;
	//Homing Missle Info;
	public bool targetLocked;
	Transform target;
	float timeToInitiateHoming;
	Vector3  targetVector3 = Vector3.zero;
	AimingSystemSimpleDouble assd;
	GameObject currentHommingTracker;

	//Boomerang
	public float maxBoomerangWidth = 160;
	public float minBoomerangWidth = 20;
	public float inclinationSide;
	public float withBoomerang;

	//Homing
	bool gotAPlayer;
	Vector3 hommingDirectionDestination;
	Transform hommingDirectionPlayer;
	public GameObject trackerPrefab;
	public GameObject trackerPrefabInst;
	public float HomingDelayTime;
	public float  minHomDelay = .3f;
	public float maxHomDelay = 2;

	public Transform ballGravityExplosionShot;
	public Transform ballShotStraightExplosion;
	public Transform shieldRotate;
	public Transform BallGravityImplosion;
	public Transform SmallBouncyBallGravityNoExplosion;
	public Transform orangeNoGrav;
	public Transform Grenade;
	public Transform homingMissle;
	public Transform boomerang;
	public Transform myThrowPoint;
	public Transform shieldRotateDummy;
	ParticleSystem shootExplosion;
	public Transform currentShot;


	bool AimingSystemDouble;
	bool RTSAimingSystem;
	bool alternator;


	public delegate void ExpelDelegate ();
	ExpelDelegate expelDelegate;
	bool leftInvaders;
	bool rightInvaders;
	bool rightsTurn;
	bool leftsTurn;


	public List<MonoBehaviour> Throws = new List<MonoBehaviour> ();


	void OnEnable()
	{
		PolymorphicTest pt = gameObject.AddComponent<PolymorphicTest> ();
		Throws.Add (pt);



		at = GetComponent<Attributes> ();
		assd = GetComponent<AimingSystemSimpleDouble> ();
		initialFixedShotStrenght = fixedShotStrenght;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.P)) {



		}

		/*
		if (at.isSelected && !at.subordination) {
			if (at.commandingUnits) {
				if (!Input.GetMouseButton (1)) {
					//ActivateExpelMethood ();
					//Activate Expel Methood for subordinations
				}
			} 
			else 
			{	
				if (!Input.GetKey(at.commander)) {
					ActivateExpelMethood ();
				}
				if (Input.GetKey(at.commander) && Input.GetKey(at.controller)) {
					ActivateExpelMethood ();
				}
			}
		}
		if (at.subordination) {
			if (Input.GetMouseButton (1)) {
				ActivateExpelMethoodSubordination ();
			}
		}
		*/
		if (at.isSelected) {
			//Divide REsponsability From Selected to, Commanded
			ForceChanger ();
			ActivateExpelMethood ();
		}
	}
	void ForceChanger()
	{
		if (at.iAmFPSPlayer) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				fixedShotStrenght += 1;
				UpdateUI (fixedShotStrenght, maxFixedShot);
				if (fixedShotStrenght >= maxFixedShot) {
					fixedShotStrenght = maxFixedShot;
					fixedShotStrenght -= 1;
				}
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				fixedShotStrenght -= 1;
				UpdateUI (fixedShotStrenght, maxFixedShot);
				if (fixedShotStrenght <= minFixedShot) {
					fixedShotStrenght = minFixedShot;
					fixedShotStrenght += 1;
				}
			}
		}
	}
	void ActivateExpelMethood(){
		if (!at.pH.isFlying) {}
		if (!at.inRTS) {
			if (assd.canShoot) {
				if (expelDelegate != null) {
					expelDelegate ();
				}
			}
		}
	}


	//Shoot Preparation, Working With Ability Selection
	public void GetCurrentAmmoDetails(int type,int curA,int maxA, float nxtF, int expel, float fireR, string name)
	{
		print ("getting" + type + expel);

		EstablishCurrentShot (type);
		genericCurrentAmmo = curA;
		genericMaxAmmo = maxA;
		genericNextFire = nxtF;
		genericExpel = expel;
		genericFireRate = fireR;
		UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
		CancelOtherAbilitiesInProgress ();
		UpdateAmmoNameInUI (name, fireR);

		if (expel == 3) {
			fixedShotStrenght = (maxFixedShot + minFixedShot) / 2;
			UpdateUI (fixedShotStrenght, maxFixedShot);
		}
	}

	void UpdateAmmoNameInUI(string name, float fireR)
	{
		at.UIC.UpdateTheAmmoUIName (name,fireR);
	}
	void EstablishCurrentShot(int it)
	{
		genericCurrentShotType = it;
		if (it== 1)
		{currentShot = at.ballGravityExplosionShot;	
			;}

		if (it == 2)
		{currentShot = at.ballShotStraight;
			UpdateUI (fixedShotStrenght, maxFixedShot);
			;}

		if (it == 3)
		{	currentShot = shieldRotate;
			UpdateUI (0, maxFixedShot);
			;
		}
		if (it == 4)
		{	currentShot = BallGravityImplosion;
			;
		}
		if (it == 5) {
			currentShot = SmallBouncyBallGravityNoExplosion;
		}
		if (it == 6) {
			currentShot = orangeNoGrav;;
			;
		}
		if (it == 7) {
			currentShot = homingMissle;
			genericExpel = 7;
			at.kT.BoomerangInfo.color = Color.cyan;
			;
		}
		if (it == 8) {
			currentShot = Grenade;
			;
		}
		if (it == 10) {
			currentShot = boomerang;
			at.kT.BoomerangInfo.color = Color.green;
			;
		}
		if (it == 11) {
			currentShot = at.ballTrazer;
			;
		}
		if (it == 12) {
			currentShot = at.ballRegular;
			;
		}
		if (it == 13) {
			currentShot = at.likeBullet;
			//fixedShotStrenght = fixedShotSUltra;
			;
		}
		if (it == 14) {
			currentShot = at.impactBall;
			//fixedShotStrenght = fixedShotSUltra;
			;
		}

		if (it == 16) {
			currentShot = at.planetMaker;
		}

		//Bulllets
		if (it == 18) {
			currentShot = at.kT.LikeBulletNoGravDestroyImpact;
		}
		if (it == 19) {
			currentShot = at.kT.LikeBulletNoGravConstant;
		}
		if (it == 20) {
			currentShot = at.kT.LikeBulletNoGrav;
		}
		if (it == 21) {
			currentShot = at.kT.LikeBullet;
		}
		if (it == 22) {
			currentShot = at.kT.LikeBulletGravDestroyImpact;
		}


		//Modifiers
		if (it == 30) {
			currentShot = at.kinematicBomb;
		}

		EstablishDelegateExpelMethood ();



		print ("this number is not assignable");
	}

	void EstablishDelegateExpelMethood()
	{
		if (genericExpel == 1) {
			expelDelegate = ChargesShotFct;
			return;
		}
		if (genericExpel == 3) {
			expelDelegate = MachineGun;
			return;
		}
		if (genericExpel == 8) {
			expelDelegate = AutomaticShotPrep;
			return;
		}

		//If not specified
		expelDelegate = MachineGun;

		/*

		if (genericExpel == 2) {
			//FixedShotFct ();
			return;
		}


				if (genericExpel == 4) {
					ShieldPlace ();
					return;
				}
			

		if (genericExpel == 5) {
			//PlaceAndCarry ();
			return;
		}
		if (genericExpel == 6) {
			Boomeranger ();
			return;
		}
		if (genericExpel == 7) {
			HomingMissleThrow ();
			return;
		}
		*/
	}





	//Automatic Shot
	void AutomaticShotPrep()
	{

		if (Input.GetMouseButton (0)) {			
			at.aSSD.MoveAimObj ();
		}
		if(Input.GetMouseButtonDown(0))
		{			
			currentShot = at.impactBall;
			Ray ray = new Ray (at.myCam.transform.position, (at.aimObj.transform.position - at.myCam.transform.position));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000, ~2)) {	

				IEnumerator tryit = SequentialAutomatic (hit.point);
				if (!automaticArm) {
					StartCoroutine (tryit);
				} else {
					automaticShotsPending.Add (tryit);			
				}
			}
		}
	}
	IEnumerator SequentialAutomatic( Vector3 target)
	{
		automaticArm = true;
		float distance = Vector3.Distance(at.myBall.position, target);
		float time2Target = distance/20;
		var yyy = calculateBestThrowSpeed (at.myThrowPoint.position, target,time2Target);
		float force = yyy.magnitude;

		Quaternion it =Quaternion.LookRotation (yyy);
		startOfShot = Time.time;
		float elapsedTime = 0;
		while (force > totalShot) {
			at.myArm.transform.rotation = Quaternion.Lerp(at.myArm.transform.rotation, it, (totalShot/force) );
			endOfShot = Time.time;
			totalShot = (endOfShot - startOfShot) * strenghtMultiplier*2;
			UpdateUI (totalShot, maxChargedShot);
			yield return null;
		}
		totalShot = 0;
		startOfShot = 0;
		ReduceAmmo ();

		Rigidbody flyThing2 = Instantiate (currentShot.GetComponent<Rigidbody>(),
			at.myThrowPoint.transform.position, 
			at.myThrowPoint.transform.rotation) as Rigidbody;

		Rigidbody rb = flyThing2.GetComponent<Rigidbody> ();
		rb.velocity = yyy;
		rb.useGravity = true;
		if (automaticShotsPending.Count > 1) {
			StartCoroutine (automaticShotsPending[1]);
			automaticShotsPending.RemoveAt(0);
		}
		automaticArm = false;
	}		
	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) {
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;

		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;

		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;

		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)

		return result;
	}



	void HandleAlternationAndCoolDowns(float force)
	{
		if (!assd.onlyLeft && !assd.onlyRight) {
			if (genericNextFire < Time.time) {
				if (alternator) {
					if (at.pH.R) {
						ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 1);
					}
				}
				if (!alternator) {
					if (at.pH.L) {
						ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 2);
					}
				}
				alternator = !alternator;
				//For when boh arms are active
				GenericNextFireforDouble (3);
			}
		}
		if (assd.onlyRight) {
			ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 1);
		}
		if (assd.onlyLeft) {
			if (at.pH.L) {
				ThrowItDouble (currentShot.GetComponent<Rigidbody> (), force, 2);
			}
		}
	}


	void ThrowItDouble(Rigidbody rb, float vel, int typeOfThrow)
	{
		if (at.pH.R) {
			if (typeOfThrow == 1) {
				if (!at.zoneOfDetectR.GetComponent<InformOfShootAreaInvaders> ().haveStilOnTrigger) {
					if (genericNextFireRight < Time.time) {
						//R
						Rigidbody flyThing2 = Instantiate (rb, this.GetComponent<Attributes> ().myThrowPoint.transform.position, this.GetComponent<Attributes> ().myThrowPoint.transform.rotation) as Rigidbody;
						AssignProyectileThingAttributes (flyThing2);

						if (absoluteVelocity) {
							flyThing2.GetComponent<Rigidbody> ().velocity = at.myThrowPoint.transform.forward * vel * forceMultiplier;
						}
						if (!absoluteVelocity) {
							flyThing2.GetComponent<Rigidbody> ().AddForce(at.myThrowPoint.transform.forward * vel * relativeVelocity);
						}
						GenericNextFireforDouble (1);
						ReduceAmmo ();
					}
				}
			}
		}
		if (at.pH.L) {
			if (typeOfThrow == 2) {
				if (!at.zoneOfDetectL.GetComponent<InformOfShootAreaInvaders> ().haveStilOnTrigger) {
					if (genericNextFireLeft < Time.time) {				

						Rigidbody flyThing2 = Instantiate (rb, at.myThrowPoint2.transform.position, at.myThrowPoint2.transform.rotation) as Rigidbody;
						AssignProyectileThingAttributes (flyThing2);

						if (absoluteVelocity) {
							flyThing2.GetComponent<Rigidbody> ().velocity = at	.myThrowPoint2.transform.forward * vel * forceMultiplier;
						}
						if (!absoluteVelocity) {
							flyThing2.GetComponent<Rigidbody> ().AddForce(at.myThrowPoint.transform.forward * vel * relativeVelocity);
						}
						ReduceAmmo ();
						GenericNextFireforDouble (2);
					}
				}	
			}
		}
		//Simultaneus throw somehow
		if (typeOfThrow == 3) {
			//R & L;
			ReduceAmmo ();
		}
		UpdateUI (fixedShotStrenght, maxFixedShot);
	}
	public void GenericNextFireforDouble(int shootCorrespondance)
	{
		if (shootCorrespondance == 1) {
			genericNextFireRight = Time.time + (genericFireRate*2);

		}
		if (shootCorrespondance == 2) {
			genericNextFireLeft = Time.time + (genericFireRate * 2);

		}

		//for both arms
		if (shootCorrespondance == 3) {
			genericNextFire = Time.time + genericFireRate;
		}

	}
	void AssignProyectileThingAttributes(Rigidbody rb)
	{
		ThingAttributes ta;
		ta = rb.transform.GetComponent<ThingAttributes> ();
		ta.team = at.team;
		ta.fromBallPlayer = at.myBall;

		if (genericExpel == 7) {
			GiveHommingInfo (rb);
		}

	}



	public void GenericNextFire()
	{
		genericNextFire = Time.time + genericFireRate;
	}
	public void ReduceAmmo()
	{
		GenericNextFire ();
		genericCurrentAmmo -= 1;
		this.GetComponent<AbilitySelection> ().UpdateAmmoSpecificCooldowns ();
		UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
		//MakeSound ();
	}
	public void UpdateTheAmmoUIInUiControll(int curAmmo, int maxAmmo)
	{
		at.UIC.UpdateTheAmmoUI (curAmmo, maxAmmo);
	}
	public void MakeSound()
	{
		AudioSource.PlayClipAtPoint (at.kT.expelSound, myThrowPoint.transform.position);
	}
	public void UpdateUI(float it, float max)
	{
		at.UIC.UpdatePowerUI(it, max);
	}


	public void assignGenericAmmo(int curA, int maxA)
	{
		genericCurrentAmmo = curA;
		genericMaxAmmo = maxA;
		UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
	}
	public void EmptyCurrentGenericAmmo()
	{
		genericCurrentAmmo = 0;
		genericMaxAmmo = 0;
		UpdateTheAmmoUIInUiControll (genericCurrentAmmo, genericMaxAmmo);
		fixedShotStrenght = initialFixedShotStrenght;
	}
	void CancelOtherAbilitiesInProgress()
	{
		shieldRotateDummy.gameObject.SetActive (false);
		genericNextFire = 0;

	}

	//Machine GUn
	public void MachineGun()
	{
		if (Input.GetMouseButton (0)) {
			HandleAlternationAndCoolDowns (fixedShotStrenght);
		}
	}
	//Charged FCT
	public void ChargesShotFct()
	{
		if (genericNextFire < Time.time && genericCurrentAmmo > 0)
		{
			if (Input.GetMouseButtonDown (0)) {
				startOfShot = Time.time;
				mouseDownOnTime = true;
			}
			if (Input.GetMouseButton (0)) {
				endOfShot = Time.time;
				totalShot = (endOfShot - startOfShot) * strenghtMultiplier;
				if (totalShot > maxChargedShot) {
					totalShot = maxChargedShot;
				}
				//Make a Generic MaxShot
				UpdateUI (totalShot, maxChargedShot);
			}
			if (Input.GetMouseButtonUp (0) && mouseDownOnTime ) 
			{
				endOfShot = Time.time;
				totalShot = (endOfShot - startOfShot);
				//ThrowIt (currentShot.GetComponent<Rigidbody> (), totalShot);
				HandleAlternationAndCoolDowns(totalShot);
				totalShot = 0;
				startOfShot = 0;
				ReduceAmmo ();
				UpdateUI (totalShot, maxChargedShot);
				mouseDownOnTime = false;
			}
		}
	}

	//Homing Missle
	void HomingMissleThrow ()
	{
		if (targetLocked)
		{
			if (Input.GetMouseButton(0) && !Input.GetKey(at.shifter)) {
				HandleAlternationAndCoolDowns (fixedShotStrenght);
			}
		}
		HomingDelayTime += Input.GetAxis("Mouse ScrollWheel");
		if (HomingDelayTime < minHomDelay) {
			HomingDelayTime = minHomDelay;
		}
		if (HomingDelayTime > maxHomDelay) {
			HomingDelayTime = maxHomDelay;
		}
		this.GetComponent<UiCoordination> ().HandleBoomerangInfo (HomingDelayTime, 90);

		if (targetLocked) {
			if (Input.GetKeyDown (at.shifter)) {
				targetLocked = false;
			}
		}
		if (!targetLocked) {
			GetComponent<AimingSystemSimpleDouble> ().aimingforHomming = true;
			GetComponent<AimingSystemSimpleDouble> ().AimForHomming ();

			if (Input.GetMouseButtonDown (0) && Input.GetKey(at.shifter)) {
				if (trackerPrefabInst) {
					Destroy (trackerPrefabInst);
				}
				this.GetComponent<AimingSystemSimpleDouble> ().endAimForHomming ();
				Ray ray = new Ray (this.GetComponent<Attributes> ().myCam.transform.position, (at.aimObj.transform.position - this.GetComponent<Attributes> ().myCam.transform.position));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 1000, ~2)) {	
					if (hit.transform != at.myBall) {
						if (hit.transform.tag == "Player") {
							gotAPlayer = true;
							//hommingDirectionPlayer = hit.transform;
							targetLocked = true;
						} else {
							gotAPlayer = false;
							targetVector3 = hit.point;
							targetLocked = true;
						}
					}
					GameObject go = Instantiate (trackerPrefab, hit.point, Quaternion.identity) as GameObject;
					trackerPrefabInst = go;
					return;
				}
			}
		}
	}		
	void GiveHommingInfo(Rigidbody rb)
	{
		HomingMissle hm;
		hm = rb.GetComponent<HomingMissle> ();
		hm.toHoming = 1+.01F;
		timeToInitiateHoming = .5f;
		if (targetLocked) {
			if (gotAPlayer) {
				//hm.TargetIsTransform (hommingDirectionPlayer, timeToInitiateHoming);
			}
			if (!gotAPlayer) {
				hm.TargetIsVector (targetVector3, HomingDelayTime);			
			}
		}
	}
	void ThrowItHoming(Rigidbody rb, float vel,Vector3 tgt, float timeHm, bool player, GameObject tracker, float delay )
	{


		//if (this.GetComponent<AimingSystemSimpleDouble> ().armWithinShooting) {	}
		/*
			Rigidbody flyThing = Instantiate (rb, this.GetComponent<Attributes> ().myThrowPoint.transform.position, this.GetComponent<Attributes> ().myThrowPoint.transform.rotation) as Rigidbody;
			flyThing.GetComponent<ProyectileAttributes> ().TeamOrigin = at.team;
			//fixed shot //use vel 
			flyThing.GetComponent<Rigidbody> ().velocity = this.GetComponent<Attributes> ().myThrowPoint.transform.forward * fixedShotStrenght;
			UpdateUI (0, maxChargedShot);
			*/

	}

	//Boomerang
	void Boomeranger()
	{
		if(Input.GetMouseButton (1))
		{
			inclinationSide+= Input.GetAxis("Mouse X");
			withBoomerang += Input.GetAxis ("Mouse ScrollWheel") *20;
			if (withBoomerang < minBoomerangWidth) {
				withBoomerang = minBoomerangWidth;
			}

			if (withBoomerang > maxBoomerangWidth) {
				withBoomerang = maxBoomerangWidth;
			}
			this.GetComponent<UiCoordination> ().HandleBoomerangInfo (withBoomerang, inclinationSide);
		}

		if (genericNextFire < Time.time && genericCurrentAmmo > 0) {
			if (Input.GetMouseButtonDown (0)) {
				mouseDownOnTime = true;
				startOfShot = Time.time;
			}

			if (Input.GetMouseButton (0)) {
				endOfShot = Time.time;
				totalShot = (endOfShot - startOfShot) * strenghtMultiplier;
				if (totalShot > maxChargedShot) {
					totalShot = maxChargedShot;
				}
				//Make a Generic MaxShot
				UpdateUI (totalShot, maxChargedShot);
			}
			if (Input.GetMouseButton (1)) {
				disto += Input.GetAxis ("Mouse Y") *8;
				withdo += Input.GetAxis ("Mouse X");
			}
		}
		if (Input.GetMouseButtonUp (0) && mouseDownOnTime ) 
		{
			endOfShot = Time.time;
			totalShot = (endOfShot - startOfShot);
			ThrowItBoomernag (totalShot*8, withBoomerang, totalShot, inclinationSide);
			totalShot = 0;
			startOfShot = 0;
			ReduceAmmo ();
			UpdateUI (totalShot, maxChargedShot);
			mouseDownOnTime = false;
		}

		if (Input.GetKey (this.gameObject.GetComponent<Attributes> ().usePowers)) {
			//TiltArm ();
		}
	}
	void ThrowItBoomernag(float d, float w, float t, float inclinationSide2)
	{
		t = t * 2;
		if (t < 1.5) {
			t = 2;
		}
		if (w > 5) {

			t = t * 1.5f;
		}
		GameObject flyThing = Instantiate (currentShot.gameObject, at.myThrowPoint.transform.forward, at.myThrowPoint.transform.rotation) as GameObject;
		flyThing.transform.GetComponent<ProyectileAttributes> ().TeamOrigin = at.team;
		Vector3 it = new Vector3 (at.myThrowPoint.transform.position.x, at.myBall.transform.position.y, at.myThrowPoint.transform.position.z);
		flyThing.transform.position = it;
		flyThing.GetComponent<Boomerang> ().Doit(d, w,t,at.rotator.forward, at.myArm.transform.localEulerAngles.x,inclinationSide2,at.myThrowPoint.transform.position.y, at.myThrowPoint, at.myBall);
		GenericNextFire ();		
	}





	public void ActivateExpelFromACommander()
	{
		ActivateExpelMethoodSubordination ();
	}
	void ActivateExpelMethoodSubordination()
	{

		if (assd.canShoot) {
			if (genericExpel == 3) {
				MachineGun ();
				//SubLookAt ();
				return;
			}
			if (genericExpel == 8) {
				AutomaticShotPrepSub ();
				return;
			}
		}
	}
	void AutomaticShotPrepSub()
	{

		if(Input.GetMouseButtonDown(0))
		{			
			currentShot = at.ballTrazer;
			float distance = Vector3.Distance(at.myBall.position, at.kT.pointInQuestion);
			float time2Target = distance/20;
			var yyy = calculateBestThrowSpeed (at.myThrowPoint.position, at.kT.pointInQuestion,time2Target);
			//StartCoroutine (ShootAutomaticAutomatically (yyy.magnitude, yyy));
		}
	}
}
