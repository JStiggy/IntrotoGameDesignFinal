using UnityEngine;
using System.Collections;

public class WolfAction : MonoBehaviour {
	//Allows the human controlled player character to interact with levers and switches needed to progress
	CameraMovement wolfMove;
	Animator PlayerAnim;
	PlayerMovement PlayerMove;
	
	void Start() {
		PlayerMove = GetComponent <PlayerMovement> ();
		wolfMove = GetComponentInChildren <CameraMovement> ();
		PlayerAnim = GetComponent<Animator>();
	}
	
	void Update() {
		if (wolfMove.currentCharacter != "Wolf") {
			PlayerAnim.SetBool("Walking",false);
			PlayerAnim.SetBool("Jumping",false);
		}
		if (Input.GetAxis ("Horizontal") == 0 && !PlayerMove.inAir  && !PlayerMove.isCarried) {
			PlayerAnim.Play("WolfIdle");
			PlayerAnim.SetBool("Walking",false);
		}
	}
}
