using UnityEngine;
using System.Collections;

public class BearAction : MonoBehaviour {
	//This script allows for the bear to attack and damage objcets in the environment or to attack indiviual enemies
	bool allowAction = false;
	bool attacking = false;
	CameraMovement bearMove;
	Animator PlayerAnimation;
	PlayerMovement PlayerMove;

	void Start() {
		PlayerMove = GetComponent <PlayerMovement> ();
		PlayerAnimation = GetComponent<Animator> ();
		bearMove = GetComponentInChildren <CameraMovement> ();
		allowAction = true;
	}

	void Update () {
		if (bearMove.currentCharacter != "Bear") {
			PlayerAnimation.SetBool("Walking",false);
		}
		if (Input.GetAxis ("Horizontal") == 0 && !attacking && !PlayerMove.isCarried) {
			PlayerAnimation.Play("BearIdle");
			PlayerAnimation.SetBool("Walking",false);
		}
	}

	void endedAttack() {
		attacking = false;
	}

	void OnTriggerStay2D (Collider2D destructableObject) {
		//When in control of the bear the action button performs a vertical swipe in front of the bear, destroying objects or damaging enemies.
		if (allowAction){
			if (Input.GetButtonDown ("Action") && bearMove.currentCharacter == "Bear") {
				PlayerAnimation.Play("BearSwipe");
				attacking = true;
				if (destructableObject.tag == "Destructable" || destructableObject.tag == "Pick Up/Destructable") {
					BoxDestroy boxDes = destructableObject.GetComponent <BoxDestroy> ();
					boxDes.Invoke("DespawnBox",1);
				}
				if (destructableObject.tag == "Enemy"){
					destructableObject.transform.parent = null;
					EnemyAction enemyAct = destructableObject.GetComponent <EnemyAction>();
					enemyAct.health = enemyAct.health - 10;
					enemyAct.Invoke("DespawnEnemy",1);
				}
			}
		}
	}
}