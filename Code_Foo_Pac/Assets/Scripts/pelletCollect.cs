using UnityEngine;
using System.Collections;

public class pelletCollect : MonoBehaviour {
	
	void Start () {
	
	}

	void FixedUpdate () {

	}

	void OnTriggerEnter2D(Collider2D other) {

		//Debug.Log ("hit");
		if (other.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
	}
}
