using UnityEngine;
using System.Collections;

public class Enemy0Movement : MonoBehaviour {

	public float xSpeed = 2f;		// The speed the enemy moves at.
	public float ySpeed = 0f;

	private float direction = 1f;
	private int rotation = 90;

	private bool xAxis = true;
	private bool yAxis = false;
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;

	private float xStartPosition;
	private float yStartPosition;
	
	private float currentXPos;
	private float currentYPos;

	private int tilesTravelled = 0;

	void Awake () {

		frontCheck = transform.Find("frontCheck").transform;

		xStartPosition = Mathf.RoundToInt (transform.position.x);
		yStartPosition = Mathf.RoundToInt (transform.position.y);
	}

	void FixedUpdate () {

		//Debug.Log (xSpeed);
		Debug.DrawLine (transform.position, frontCheck.position, Color.green, 0.05f);

		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position);
		
		// Check each of the colliders.
		foreach (Collider2D c in frontHits) {
			// If any of the colliders is an Obstacle...
			if (c.tag == "obstacle") {

				if (xAxis ^ yAxis) {
					xSpeed = 0f;
					ySpeed = 2f;
					//direction = direction * -1;
					//direction = Mathf.Round (Random.value);
					yAxis = !yAxis;
				} else {
					xSpeed = 2f;
					ySpeed = 0f;
					direction = direction * -1;
					//direction = Mathf.Round (Random.value);
					xAxis = !xAxis;
				}

				if (direction >= 0) {
					direction = 1f;
				} else {
					direction = -1f;
				}

				transform.Rotate(0, 0, rotation);
				tilesTravelled = 0;
			}
		}
		
		rigidbody2D.velocity = new Vector2(xSpeed * direction, ySpeed * direction);

		currentXPos = Mathf.RoundToInt (transform.position.x) - xStartPosition;
		currentYPos = Mathf.RoundToInt (transform.position.y) - yStartPosition;

		if (currentXPos == 1 || currentYPos == 1 || currentXPos == -1 || currentYPos == -1) {
			//Debug.Log ("0 new tile");
			tilesTravelled += 1;
			xStartPosition = Mathf.RoundToInt (transform.position.x);
           	yStartPosition = Mathf.RoundToInt (transform.position.y);
		}

	}
}
