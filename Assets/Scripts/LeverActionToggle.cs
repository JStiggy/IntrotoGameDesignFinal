using UnityEngine;
using System.Collections;

public class LeverActionToggle : MonoBehaviour {
	//This script controls the state of a gate or moving platform.

	//Most of these variables are used for controlling wether and how far moving platforms move, this script also controls gates
	[HideInInspector]
	public bool itemActive = false;
	//Determines the platforms directional movement
	public bool canMoveVert = false;
	public bool canMoveHoriz = false;
	//Decides if the platform moves up or down first, right or left first
	bool movingUp = true;
	bool movingRight = true;
	//How far in each direction the platforms can move
	public float xMaxMove = 0;
	public float xMinMove = 0;
	public float yMaxMove = 0;
	public float yMinMove = 0;
	//Used for determiing when to switch directions
	float vertCount = 0;
	float horizCount = 0;
	//Starting position of the gate or moving platform
	float xPos;
	float yPos;
	[HideInInspector]
	public bool isPressureControlled = false;
	Transform leverControlledObject;
	Animator leverControl;

	void Start () {
		leverControl = GetComponentInParent<Animator>();
		leverControlledObject = GetComponent<Transform> ();
		xPos = leverControlledObject.localPosition.x;
		yPos = leverControlledObject.localPosition.y;
	}

	void FixedUpdate(){
		//Fixed update is only used with moving platforms
		if (gameObject.tag == "Interactable/MovingPlatform" && itemActive)
		{
			if (canMoveVert)
			{
				if (movingUp){
					vertCount = (float)(vertCount + .01);
					if (vertCount >= yMaxMove){
						movingUp = false;
					}
				} else {
					vertCount = (float)(vertCount - .01);
					if (vertCount <= yMinMove){
						movingUp = true;
					}
				}
			}

			if (canMoveHoriz)
			{
				if (movingRight){
					horizCount = (float)(horizCount + .01);
					if (horizCount >= xMaxMove){
						movingRight = false;
					}
				} else {
					horizCount = (float)(horizCount - .01);
					if (horizCount <= xMinMove){
						movingRight = true;
					}
				}
			}
			leverControlledObject.localPosition = new Vector3((float)(xPos+horizCount),(float)(yPos + vertCount),0);
		}
	}

	void Activated(){
		//gates simply move up or down to facilitate openeing or closing
		if (!isPressureControlled) {
			itemActive = !itemActive;
			leverControl.SetBool ("Active",itemActive);
		}
		if (gameObject.tag == "Interactable/Gate") {
			if (itemActive){
				leverControlledObject.localPosition = new Vector3(xPos,(float)(yPos +1),0);
			} else {
				leverControlledObject.localPosition = new Vector3(xPos,yPos,0);
			}
		}
	}

	void Deactivated(){
		itemActive = false;
	}
}