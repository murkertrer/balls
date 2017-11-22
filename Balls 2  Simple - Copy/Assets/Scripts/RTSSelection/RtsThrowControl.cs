using UnityEngine;
using System.Collections;

public class RtsThrowControl : MonoBehaviour {

    Attributes at;
    GenericThrowControl gtc;

	void Start () {
        at = GetComponent<Attributes>();
        gtc = GetComponent<GenericThrowControl>();
	}

    // Update is called once per frame
    void Update() {

		if (at.isSelected && !Input.GetKey(KeyCode.CapsLock)) {
			if (gtc.genericExpel == 3) { 
				//gtc.MachineGun ();
				return;
			}
			if (gtc.genericExpel == 1) {
				//gtc.ChargesShotFct ();
				return;
			}
		}
    }
}
