using UnityEngine;
using System.Collections;

public class BoxDestroy : MonoBehaviour {
	void DespawnBox() {
		gameObject.transform.parent = null;
		Destroy(gameObject);
	}
}
