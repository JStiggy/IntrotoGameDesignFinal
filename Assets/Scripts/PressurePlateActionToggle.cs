using UnityEngine;
using System.Collections;

public class PressurePlateActionToggle : MonoBehaviour {
	LeverActionToggle activatedObject;
	GameObject triggeringObject;

	// Use this for initialization
	void Start () {
		activatedObject = GetComponentInChildren<LeverActionToggle> ();
		activatedObject.isPressureControlled = true;
	}

	void Update () {
		if (triggeringObject == null) {
			activatedObject.itemActive = false;
			activatedObject.Invoke ("Activated", 0);
		}
	}

	void OnTriggerStay2D (Collider2D physObj) {
		if ((physObj.gameObject.tag == "Pick Up" || physObj.gameObject.tag == "Pick Up/Destructable" || physObj.gameObject.tag == "Player") && physObj.gameObject.name != "Hawk") {
			Debug.Log(physObj);
			triggeringObject = physObj.gameObject;
			activatedObject.itemActive = true;
			activatedObject.Invoke ("Activated", 0);
		}
	}

	void OnTriggerExit2D (Collider2D physObj) {
		if ((physObj.gameObject.tag == "Pick Up" || physObj.gameObject.tag == "Pick Up/Destructable" || physObj.gameObject.tag == "Player") && physObj.gameObject.name != "Hawk") {
			activatedObject.itemActive = false;
			activatedObject.Invoke ("Activated", 0);
		}
	}
}
