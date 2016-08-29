using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	//This script controlls all player movement for all player movement, individual actions are spread across the individual character scripts 

	//Information used to check for the ability to jump, groundCheck checks all objects not in the player layer comparing
	//them to a circle around GroundCheck of radius groundRadius. The hawk does not use thi set of variables.
	public Transform GroundCheck;
	float groundRadius = .1f;
	public LayerMask ground;
	bool isJumping = false;
	public bool inAir = false;

	//Movement Properties for all player characters
	//The hawk cannot jump and intead uses the vertical axis, the jump button can als move the hawk upward
	//Note: The bear cannot jump(jumpForce = 1f), the human has a weak jump, and the wolf has a strong jump
	Rigidbody2D playerCharRB;
	Transform playerCharTrans;
	public float jumpForce = 10f;
	float horiz;
	float vertical;
	public float moveForce = 10f;
	public float maxSpeed = .5f;
	Animator playerAnimation;
	public bool isCarried;
	Transform BackgroundTransform;

	void Start () {
		BackgroundTransform = GameObject.Find("TempleWallColor").GetComponent<Transform>();
		isCarried = false;
		playerAnimation = GetComponent<Animator> ();
		playerCharTrans = GetComponent<Transform> ();
		playerCharRB = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		//Checks for any player input
		if(Input.GetButtonDown("Jump"))
		{
			isJumping = true;
		}
		horiz = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		BackgroundTransform.position = new Vector3 (playerCharTrans.position.x,playerCharTrans.position.y,100);
	}
	
	void FixedUpdate()
	{
		//Checks to see if the player is on the ground, if they are and the jump button is preesed, the player will jump
		//if jump force is 0 the space bar functions like the vertical positive axis
		playerAnimation.SetBool ("Walking", false);
		if (horiz > 0) {
			playerCharTrans.localScale = new Vector3(1,1,1);
			playerAnimation.SetBool("Walking",true);
		}
		if (horiz < 0) {
			playerAnimation.SetBool("Walking",true);
			playerCharTrans.localScale = new Vector3(-1,1,1);
		}
		
		if (jumpForce > 0){
			//
			inAir = true;
			if (Physics2D.OverlapCircle(GroundCheck.position, groundRadius, ground)) {
				playerAnimation.SetBool("Jumping",false);
				inAir = false;
			}
			//
			if (isJumping && Physics2D.OverlapCircle(GroundCheck.position, groundRadius, ground)) {
				playerCharRB.AddForce (new Vector2 (0, jumpForce));
				isJumping = false;
				}
			if (inAir) {
				playerAnimation.SetBool("Jumping",true);
			}
		} else {
			playerCharRB.AddForce(Vector2.up * moveForce *vertical,0);
			if (maxSpeed < Mathf.Abs (playerCharRB.velocity.y)) {
				playerCharRB.velocity = new Vector2(playerCharRB.velocity.x, Mathf.Sign(playerCharRB.velocity.y)* maxSpeed);
			}
		}

		//Adds a directional force onto the current controlled character, moving them horizontally
		playerCharRB.AddForce(Vector2.right * moveForce *horiz, 0);
		if (maxSpeed < Mathf.Abs (playerCharRB.velocity.x)) {
			playerCharRB.velocity = new Vector2(Mathf.Sign(playerCharRB.velocity.x)* maxSpeed, playerCharRB.velocity.y);
		}
		isJumping = false;
	}
}