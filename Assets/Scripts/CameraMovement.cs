using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	//Thois script controlls the camer, who its locked onto, which character can currently move, and who can use their special abilites

	//Creates a list of all player controllable objects
	public GameObject[] allPlayerObjects = new GameObject[5];
	//Creates a list to contain all the transform vectors for player controllable objects, minus the camera
	Transform[] allPlayerTransform = new Transform[4];
	PlayerMovement[] allMoveScripts = new PlayerMovement[4];
	//Allows for disabling and enabling individual action scripts
	HawkAction hawkScript;
	WolfAction wolfScript;
	BearAction bearScript;
	HumanAction humanScript;
	[HideInInspector] public string currentCharacter = "Human";
	Transform cameraPosition;

	void Start () {
		cameraPosition = GetComponent<Transform> ();

		//fills the transform list with transform objects
		for (int i=1; i<=4; i++) {
			allPlayerTransform[i-1] =  allPlayerObjects[i].GetComponent<Transform>();
			allMoveScripts[i-1] =  allPlayerObjects[i].GetComponent<PlayerMovement>();
		}
		//Creates ways to eneb]able and disable other player charater scripts
		hawkScript = allPlayerObjects[2].GetComponent<HawkAction> ();
		wolfScript = allPlayerObjects[3].GetComponent<WolfAction> ();
		bearScript = allPlayerObjects[4].GetComponent<BearAction> ();
		hawkScript.enabled = false;
		wolfScript.enabled = false;
		bearScript.enabled = false;

		//Centers the camera onto the players controlled object, at the start this is the human
		cameraPosition.localPosition = new Vector3(0,0,-1);
	}

	void Update () {
		//Centers the camera onto the player and then checks to see if the player wants to change characters through a button press
		//This changes the camera's parent to one of the other player characters, then locks the camera onto this character.
		cameraPosition.localPosition = new Vector3(0,0,-1);

		if (Input.GetButtonDown ("Change Hawk")) {
			DisableScripts();
			currentCharacter = "Hawk";
			cameraPosition.parent = null;
			cameraPosition.parent = allPlayerTransform[1];
			hawkScript.enabled = true;
			allMoveScripts[1].enabled = true;
		}

		if (Input.GetButtonDown ("Change Bear")) {
			DisableScripts();
			currentCharacter = "Bear";
			cameraPosition.parent = null;
			cameraPosition.parent = allPlayerTransform[3];
			bearScript.enabled = true;
			allMoveScripts[3].enabled = true;
		}

		if (Input.GetButtonDown ("Change Wolf")) {
			DisableScripts();
			currentCharacter = "Wolf";
			cameraPosition.parent = null;
			cameraPosition.parent = allPlayerTransform[2];
			wolfScript.enabled = true;
			allMoveScripts[2].enabled = true;
		}

		if (Input.GetButtonDown ("Change Human")) {
			DisableScripts();
			currentCharacter = "Human";
			cameraPosition.parent = null;
			cameraPosition.parent = allPlayerTransform[0];
			allMoveScripts[0].enabled = true;
		}
	}

	void DisableScripts()
	{
		for(int i = 0;i<4;i++){
			allMoveScripts[i].enabled = false;
		}
	}
}

