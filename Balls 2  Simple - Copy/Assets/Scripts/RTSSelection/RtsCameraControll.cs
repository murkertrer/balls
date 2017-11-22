using UnityEngine;
using System.Collections;

public class RtsCameraControll : MonoBehaviour {
    float movementSpeed = 0.9f;
    public float rotationSpeed = 4f;
    public float smoothness = 0.85f;
    public Transform camDirector;
    UnitSelectionComponent uSC;

    bool lockedCam;
    Transform lockedPlayer;
    bool canUnlock;

    public bool kamDir = true;
    Vector3 targetPosition;
    Vector3 offsetToTarget;

    public Quaternion targetRotation;
    float targetRotationY;
    float targetRotationX;

    public void GetCameraDirector(Transform it)
    {        
        camDirector = it;

    }

    // Use this for initialization
    void Start()
    {
        uSC = GetComponent<UnitSelectionComponent>();
        targetPosition = transform.position;
        targetRotation = transform.rotation;
        targetRotationY = transform.localRotation.eulerAngles.y;
        targetRotationX = transform.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockedCam)
        {
            CamMovementControll();
        }

        if (!lockedCam)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N))
            {

                if (uSC.selectedObjects.Count > 0)
                {
                    lockedCam = true;
                    lockedPlayer = uSC.selectedObjects[0].transform;
                    offsetToTarget = transform.position - uSC.selectedObjects[0].transform.position;
                }
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N))
        {
			if (uSC.selectedObjects.Count > 0) {
				uSC.selectedObjects [0].transform.root.GetComponent<PlayerHandle> ().TransferFromRTSToFPS ();
				//Proper Actions here

				//****************

				foreach (var selectedObject in uSC.selectedObjects) {
					selectedObject.transform.root.GetComponent<Attributes> ().DestroySelectionCircle ();

				}

				Destroy (this.gameObject);
			} else {
				//notify Here.
			}
        }
    }
    void CamMovementControll()
    {
		if (Input.GetKey (KeyCode.LeftControl)) {

			if (Input.GetKey (KeyCode.W))
				targetPosition += transform.forward * movementSpeed;
			if (Input.GetKey (KeyCode.A))
				targetPosition -= transform.right * movementSpeed;
			if (Input.GetKey (KeyCode.S))
				targetPosition -= transform.forward * movementSpeed;
			if (Input.GetKey (KeyCode.D))
				targetPosition += transform.right * movementSpeed;
			if (Input.GetKey (KeyCode.Q))
				targetPosition -= transform.up * movementSpeed;
			if (Input.GetKey (KeyCode.E))
				targetPosition += transform.up * movementSpeed;

			if (Input.GetMouseButton (1)) {
				if (!Input.GetKey (KeyCode.LeftShift)) {
					Cursor.visible = false;
					targetRotationY += Input.GetAxis ("Mouse X") * rotationSpeed;
					targetRotationX -= Input.GetAxis ("Mouse Y") * rotationSpeed;
					targetRotation = Quaternion.Euler (targetRotationX, targetRotationY, 0.0f);
				}
			} else
				Cursor.visible = true;

			transform.position = Vector3.Lerp (transform.position, targetPosition, (1.0f - smoothness));
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, (1.0f - smoothness));

		} else {

			if (camDirector) {
				if (Input.GetKey (KeyCode.W))
					targetPosition += camDirector.forward * movementSpeed;
				if (Input.GetKey (KeyCode.A))
					targetPosition -= camDirector.right * movementSpeed;
				if (Input.GetKey (KeyCode.S))
					targetPosition -= camDirector.forward * movementSpeed;
				if (Input.GetKey (KeyCode.D))
					targetPosition += camDirector.right * movementSpeed;
				if (Input.GetKey (KeyCode.Q))
					targetPosition -= camDirector.up * movementSpeed;
				if (Input.GetKey (KeyCode.E))
					targetPosition += camDirector.up * movementSpeed;

				if (Input.GetMouseButton (1)) {


				} else
					Cursor.visible = true;

				transform.position = Vector3.Lerp (transform.position, targetPosition, (1.0f - smoothness));
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, (1.0f - smoothness));
			}
		}


		/*
		 * 				if (!Input.GetKey (KeyCode.LeftShift)) {

					Cursor.visible = false;
					targetRotationY += Input.GetAxis ("Mouse X") * rotationSpeed;
					targetRotationX -= Input.GetAxis ("Mouse Y") * rotationSpeed;
					targetRotation = Quaternion.Euler (targetRotationX, targetRotationY, 0.0f);
				}*/

    }
    void LateUpdate()
    {
        if (lockedCam)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {

                if (Input.GetKey(KeyCode.W))
                    targetPosition += transform.forward * movementSpeed;
                if (Input.GetKey(KeyCode.A))
                    targetPosition -= transform.right * movementSpeed;
                if (Input.GetKey(KeyCode.S))
                    targetPosition -= transform.forward * movementSpeed;
                if (Input.GetKey(KeyCode.D))
                    targetPosition += transform.right * movementSpeed;
                if (Input.GetKey(KeyCode.Q))
                    targetPosition -= transform.up * movementSpeed;
                if (Input.GetKey(KeyCode.E))
                    targetPosition += transform.up * movementSpeed;

                if (Input.GetMouseButton(1))
                {
                    Cursor.visible = false;
                    targetRotationY += Input.GetAxis("Mouse X") * rotationSpeed;
                    targetRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;
                    targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
                }
                else
                    Cursor.visible = true;

                transform.position = Vector3.Lerp(lockedPlayer.transform.position + offsetToTarget, targetPosition, (1.0f - smoothness));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (1.0f - smoothness));
            }
            else
            {
                transform.position = lockedPlayer.transform.position + offsetToTarget;
            }


            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
            {
                lockedCam = false;
            }
        }
    }
}

