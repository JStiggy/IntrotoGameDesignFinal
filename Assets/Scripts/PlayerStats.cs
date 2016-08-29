using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	//Sets the health for the spirit animals, enemies attack them, draining health over time
	public int bearHealth = 7;
	public int hawkHealth = 2;
	public int wolfHealth = 4;
	Text WolfText;
	Text BearText;
	Text HawkText;
	[HideInInspector]
	public string playerTarget = "Hawk"; 

	void Start () {
		WolfText =GameObject.Find("WolfHP").GetComponent<Text>();
		BearText =GameObject.Find("BearHP").GetComponent<Text>();
		HawkText =GameObject.Find("HawkHP").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		WolfText.text = wolfHealth.ToString();
		BearText.text = bearHealth.ToString();
		HawkText.text = hawkHealth.ToString();
		if (bearHealth <= 0 || hawkHealth <= 0 || wolfHealth <= 0) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void Damage() {
		if (playerTarget == "Hawk") {
			hawkHealth--;
			Debug.Log(hawkHealth);
		}
		if (playerTarget == "Wolf") {
			wolfHealth--;
			Debug.Log(wolfHealth);
		}
		if (playerTarget == "Bear") {
			bearHealth--;
			Debug.Log(bearHealth);
		}
	}
}
