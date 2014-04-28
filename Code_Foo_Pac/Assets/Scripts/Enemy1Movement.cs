using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy1Movement : MonoBehaviour {

	public float xSpeed = 0f;		// The speed the enemy moves at.
	public float ySpeed = 0f;
	public float direction = 0f;
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform topCheck;
	private Transform bottomCheck;
	private Transform leftCheck;
	private Transform rightCheck;

	//private bool collision = false;

	private bool ceiling = false;
	private bool ground = false;
	private bool wallRight = false;
	private bool wallLeft = false;

	private float xStartPosition;
	private float yStartPosition;

	private float currentXPos;
	private float currentYPos;

	private int tilesTravelled = 0;

	void Awake()
	{
		// Setting up the references.
		//ren = transform.Find("body").GetComponent<SpriteRenderer>();

		topCheck = transform.Find("topCheck").transform;
		bottomCheck = transform.Find("bottomCheck").transform;
		leftCheck = transform.Find("leftCheck").transform;
		rightCheck = transform.Find("rightCheck").transform;

		xStartPosition = Mathf.FloorToInt(transform.position.x);
		yStartPosition = Mathf.FloorToInt(transform.position.y);

		Move();

		//xSpeed = Mathf.Round (Random.value);
		//direction = Mathf.Round(Random.value);
	}
	
	void FixedUpdate () {

		if (tilesTravelled == 1) {
			Move();
			tilesTravelled = 0;
		}

		currentXPos = Mathf.FloorToInt (transform.position.x) - xStartPosition;
		currentYPos = Mathf.FloorToInt (transform.position.y) - yStartPosition;
		
		if (Mathf.Abs(currentXPos) == 1 || Mathf.Abs(currentYPos) == 1) {
			//Debug.Log ("1 new tile");
			//Debug.Log(Mathf.FloorToInt(transform.position.x));
			tilesTravelled += 1;
			xStartPosition = Mathf.FloorToInt (transform.position.x);
			yStartPosition = Mathf.FloorToInt (transform.position.y);
		}
	}

	void Move() {

		int randomMove = 0;
		List<string> moves;
		moves = new List<string>();

		ceiling = Physics2D.Linecast(transform.position, topCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, topCheck.position, 1 << LayerMask.NameToLayer("Wall"));
		ground = Physics2D.Linecast(transform.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Wall"));
		wallRight = Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Wall")) || Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		wallLeft = Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Wall")) || Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		
		if ((!ceiling) && (ySpeed >= 0)) {
			moves.Add("moveUp");
		}
		if ((!ground) && (ySpeed <= 0)) {
			moves.Add("moveDown");
		}
		if ((!wallRight) && (xSpeed >= 0)) {
			moves.Add("moveRight");
		}
		if ((!wallLeft) && (xSpeed <= 0)) {
			moves.Add("moveLeft");
		}

		//Debug.Log (moves.Count);

		randomMove = Mathf.RoundToInt(Random.Range (0, (moves.Count)));

		switch (moves[randomMove]) {
			case "moveUp":
				xSpeed = 0f;
				ySpeed = 2f;
				break;
			case "moveDown":
				xSpeed = 0f;
				ySpeed = -2f;
				break;
			case "moveRight":
				xSpeed = 2f;
				ySpeed = 0f;
				break;
			case "moveLeft":
				xSpeed = -2f;
				ySpeed = 0f;
				break;
		}

		/*if (xSpeed > 0) {
			xSpeed = 2f;
			ySpeed = 0f;
		} else {
			xSpeed = 0f;
			ySpeed = 2f;
		}
		
		if (direction > 0) {
			direction = 1f;
		} else {
			direction = -1f;
		}*/
		
		rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);

	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "obstacle") {
			//Debug.Log ("hit");
			direction = direction * -1;
			//collision = true;
		}

		//Debug.Log (direction);
	}
}
