using UnityEngine;
using System.Collections;

public class HumanAction : MonoBehaviour {
	//Allows the human controlled player character to interact with levers and switches needed to progress
	CameraMovement humMove;
	Animator PlayerAnim;
	PlayerMovement PlayerMove;

	void Start() {
		PlayerMove = GetComponent <PlayerMovement> ();
		humMove = GetComponentInChildren <CameraMovement> ();
		PlayerAnim = GetComponent<Animator>();
	}

	void Update() {
		if (humMove.currentCharacter != "Human") {
			PlayerAnim.SetBool("Walking",false);
			PlayerAnim.SetBool("Jumping",false);
		}
		if (Input.GetAxis ("Horizontal") == 0 && !PlayerMove.inAir && !PlayerMove.isCarried) {
			PlayerAnim.Play("HumanIdle");
			PlayerAnim.SetBool("Walking",false);
		}
	}

	//Checks to see if there is an interactable object in front of the human, if so the action state for the objecyt is toggled.
	void OnTriggerStay2D (Collider2D interactableObject) {
		if (Input.GetButtonDown ("Action") && humMove.currentCharacter == "Human") {
			if (interactableObject.tag == "Interactable") {
				interactableObject.GetComponentInChildren<LeverActionToggle>().Invoke("Activated",0);
			}
		}
	}
}