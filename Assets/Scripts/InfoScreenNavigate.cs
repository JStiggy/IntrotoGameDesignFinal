using UnityEngine;
using System.Collections;

public class InfoScreenNavigate : MonoBehaviour {

	public string levelToLoad;
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Action")) {
			if (levelToLoad == "Exit") {
				Application.Quit();
			} else {
			Application.LoadLevel(levelToLoad);
			}
		}
	}
}
