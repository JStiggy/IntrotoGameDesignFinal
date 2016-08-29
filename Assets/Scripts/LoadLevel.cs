using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public string levelToLoad;
	

	void OnTriggerStay2D(Collider2D obj) {
		if (obj.name == "Human") {
			Application.LoadLevel (levelToLoad);
		}
	}
}
