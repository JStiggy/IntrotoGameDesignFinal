using UnityEngine;
using System.Collections;

public class EnemyAction : MonoBehaviour {
	//This script controls all the movement and action of enemy characters, all enemies are melee range and move slowly, they do not jump
	//All enemies take two hits from the bear or three hits from a wolf dash attack

	//Sets up the stats and data needed to move the enemy and detect player characters
	public float moveSpeed = 4f;
	public float maxSpeed = 4f;
	public float attackRange = 1.7f;
	public int health = 10;
	Transform playerPosition;
	Transform enemyPosition;
	Rigidbody2D enemyCharRB;
	PlayerStats playerHP;
	int attackRecharge;
	Animator enemyAnimation;

	void Start () {
		enemyAnimation = GetComponent<Animator> ();
		playerHP = GameObject.Find ("UI Manager").GetComponent<PlayerStats>();
		enemyCharRB = GetComponent<Rigidbody2D>();
		enemyPosition = GetComponent<Transform>();
		playerPosition = null;
	}

	void OnTriggerEnter2D (Collider2D playerCharacter) {
		//If a player piece enters the detection ranget the player will begin to be targeted
		if (playerCharacter.tag == "Player") {
			playerPosition = playerCharacter.transform;
			playerHP.playerTarget = playerCharacter.gameObject.name;
		}
	}

	void OnTriggerStay2D (Collider2D playerCharacter) {
		//If two player characters are in the detection range and one leaves,
		//this code ensures the enemy will target the other player character
		if (playerPosition == null && playerCharacter.tag == "Player") {
			playerPosition = playerCharacter.transform;
			playerHP.playerTarget = playerCharacter.gameObject.name;
		}
		if (playerCharacter.name == "Human") {
			playerPosition = playerCharacter.transform;
		}
	}
	
	void OnTriggerExit2D (Collider2D playerCharacter) {
		//when theplayer leaves the detection range, the enemy stops searching for the player 
		if (playerPosition != null){
			if (playerCharacter.tag == "Player" && playerPosition.gameObject == playerCharacter.gameObject) {
				playerPosition = null;
				enemyAnimation.SetBool("Walking",false);
			}
		}
	}

	void FixedUpdate () {
		//Code runs if an enemy has been detected
		if (playerPosition != null) {
			enemyAnimation.SetBool("Walking",false);
			if (Vector2.Distance(playerPosition.position,enemyPosition.position) < attackRange)
			{
				attackRecharge++;
				if (playerPosition.gameObject.name == "Human"){
					//If the player character attacked is the human, game over
					if (attackRecharge == 50) {
						Application.LoadLevel(Application.loadedLevel);
					}
				} else {
					//If the player character was a spirit animal, the spirit is attacked and takes damage.
					//Each animal has it's own set level of health, once one animal hits 0, you lose
					if (attackRecharge == 50) {
						enemyAnimation.Play ("EnemySwipe");
						playerHP.Invoke("Damage",0);
						attackRecharge = 0;
					}
				}
			} else {
				enemyAnimation.SetBool("Walking",true);
				if (playerPosition.position.x < enemyPosition.position.x) {
					enemyCharRB.AddForce(-Vector2.right * moveSpeed, 0);
					enemyPosition.localScale = new Vector3(-1,1,1);
					if (maxSpeed < Mathf.Abs (enemyCharRB.velocity.x)) {
						enemyCharRB.velocity = new Vector2(Mathf.Sign(enemyCharRB.velocity.x)* maxSpeed, enemyCharRB.velocity.y);
					}
				}
				if (playerPosition.position.x > enemyPosition.position.x) {
					enemyCharRB.AddForce(Vector2.right * moveSpeed, 0);
					enemyPosition.localScale = new Vector3(1,1,1);
					if (maxSpeed < Mathf.Abs (enemyCharRB.velocity.x)) {
						enemyCharRB.velocity = new Vector2(Mathf.Sign(enemyCharRB.velocity.x)* maxSpeed, enemyCharRB.velocity.y);
					}
				}
			}
		}
	}

	void DespawnEnemy () {
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}