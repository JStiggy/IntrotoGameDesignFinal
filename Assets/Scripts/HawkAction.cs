using UnityEngine;
using System.Collections;

public class HawkAction : MonoBehaviour {
	//Allows the hawk to grab and fly around with objects, and later drop them 
	//Gets all the information needed to allow the hawk to pick up friendly or neutral objects
	CameraMovement hawkMove;
	Transform hawkTransform;
	Transform objectTransform;
	PlayerMovement objectMovement;
	float objectSize;
	bool hasItem = false;
	bool allowAction = false;
	Animator hawkAnimator;
	Animator objectAnimator = null;
	
	void Start () {
		allowAction = true;
		hawkTransform = GetComponent <Transform> ();
		hawkAnimator = GetComponent <Animator> ();
		hawkMove = GetComponentInChildren <CameraMovement> ();
	}

	void FixedUpdate () {
		//Constantly updates the item to be a certain position under the hawk while it is carrying the object
		//if th eitem is destroyed while the hawk carries it, the else statement resets the hawks carrying ability
		if (objectTransform) {
			if (Input.GetButtonDown ("Action") && hasItem) {
				hawkAnimator.SetBool("Carrying",false);
				if (objectTransform.tag == "Player") {
					objectMovement.isCarried = false;
					objectAnimator.SetBool("Carried",false);
				}
				hasItem = false;
				objectTransform.parent = null;
			}
			if (hasItem) {
				objectTransform.localPosition = new Vector3 (0, (float)(-objectSize / 2 - .1), 0);
			}
		} else {
			hasItem = false;
			hawkAnimator.SetBool("Carrying",false);
		}
	}
	
	void OnTriggerStay2D(Collider2D pickUp){
		//If an object is in the hawks pickup zone and meets all requirements for being grabbed, the object will be picked up
		//If the hawk has an item, it will be dropped and deparented form the hawk
		if (allowAction) {
			if ((pickUp.gameObject.tag == "Pick Up" || pickUp.gameObject.tag == "Pick Up/Destructable" || pickUp.gameObject.tag == "Player") && hawkMove.currentCharacter == "Hawk") {
				if (!hasItem && Input.GetButtonDown ("Action")) {
					hawkAnimator.SetBool("Carrying",true);
					objectTransform = pickUp.transform;
					if (pickUp.tag == "Player") {
						objectMovement = pickUp.GetComponent<PlayerMovement> ();
						objectMovement.isCarried = true;
						objectAnimator = pickUp.GetComponent<Animator> ();
						objectAnimator.SetBool("Carried",true);
					}
					pickUp.transform.localScale = new Vector3(hawkTransform.localScale.x,hawkTransform.localScale.y,hawkTransform.localScale.z);
					pickUp.transform.parent = hawkTransform;
					objectSize = pickUp.transform.lossyScale.y;
					hasItem = true;
					return;
				}
			}
		}
	}
}