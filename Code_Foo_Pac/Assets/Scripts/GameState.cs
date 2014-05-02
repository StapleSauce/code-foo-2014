using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		GameObject pellets = GameObject.Find("pellet(Clone)");
		
		//if all the pellets have been collected stop the game
		if (pellets == null) {
			// Set the score text.
			Time.timeScale = 0;
		}

		if (Input.GetKeyDown (KeyCode.Return)) { 
			Time.timeScale = 1;
			Application.LoadLevel ("Level"); 
		} 

	}

}
