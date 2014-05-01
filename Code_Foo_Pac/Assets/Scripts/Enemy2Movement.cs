using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy2Movement : MonoBehaviour {

	public int xSpeed = 0;		// The speed the enemy moves at.
	public int ySpeed = 0;
	public int direction = 0;
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform topCheck;
	private Transform bottomCheck;
	private Transform leftCheck;
	private Transform rightCheck;

	private bool ceiling = false;
	private bool ground = false;
	private bool wallRight = false;
	private bool wallLeft = false;

	private float playerXPosition;
	private float playerYPosition;
	
	private float xStartPosition;
	private float yStartPosition;
	
	private float xDistance;
	private float yDistance;
	
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

		
		playerXPosition = GameObject.Find("player").transform.position.x - xStartPosition + 1;
		playerYPosition = GameObject.Find("player").transform.position.y - yStartPosition + 1;

		Move();
		
		//xSpeed = Mathf.Round (Random.value);
		//direction = Mathf.Round(Random.value);
	}
	
	void FixedUpdate () {

		xDistance = Mathf.FloorToInt (transform.position.x) - xStartPosition;
		yDistance = Mathf.FloorToInt (transform.position.y) - yStartPosition;

		//need to add collision logic
		if (tilesTravelled == 1) {

			playerXPosition = GameObject.Find("player").transform.position.x - xStartPosition;
			playerYPosition = GameObject.Find("player").transform.position.y - yStartPosition;

			//Debug.Log("test");
			Move();

			tilesTravelled = 0;
		}
		
		if (Mathf.Abs(xDistance) == 1 || Mathf.Abs(yDistance) == 1) {
			//Debug.Log ("1 new tile");
			tilesTravelled += 1;
			xStartPosition = Mathf.FloorToInt(transform.position.x);
			yStartPosition = Mathf.FloorToInt(transform.position.y);
		}
	}

	void Move() {

		//int moves = 0;
		int iCount = 0;
		bool bestMove = false;
		List<string> moves;
		moves = new List<string>();

		ceiling = Physics2D.Linecast(transform.position, topCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, topCheck.position, 1 << LayerMask.NameToLayer("Wall"));
		ground = Physics2D.Linecast(transform.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Wall"));;
		wallRight = Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Wall")) || Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Ground"));;
		wallLeft = Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Wall")) || Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Ground"));;

		if ((!ceiling) && (ySpeed >= 0)) {
			moves.Add("moveUp");
		}
		if ((!ground) && (ySpeed <= 0)) {
			moves.Add("moveDown");
			//Debug.Log("test");
		}
		if ((!wallRight) && (xSpeed >= 0)) {
			moves.Add("moveRight");
		}
		if ((!wallLeft) && (xSpeed <= 0)) {
			moves.Add("moveLeft");
		}

		//Debug.Log(moves.Count);

		//only change direction when I am able to
		if (moves.Count > 1) {
			//Debug.Log(ground);
			while (iCount < moves.Count && bestMove == false) {

				if(moves[iCount] == "moveDown") {
					//xSpeed = 0;
					//ySpeed = -2;
					//moves.Remove("moveUp");
					//ceiling = true;
					if (Mathf.Abs(playerYPosition) > Mathf.Abs(playerXPosition)) {	
						if(playerYPosition < 0) {
							//Debug.Log("wrong");
							xSpeed = 0;
							ySpeed = -3;
							bestMove = true;
						}
					} else {
						if(playerYPosition < 0) {
							xSpeed = 0;
							ySpeed = -3;
						}
					}
				}
				if(moves[iCount] == "moveUp") {
					//xSpeed = 0;
					//ySpeed = 2;
					//moves.Remove("moveDown");
					//ground = true;
					if (Mathf.Abs(playerYPosition) > Mathf.Abs(playerXPosition)) {
						if(playerYPosition > 0) {
							xSpeed = 0;
							ySpeed = 3;
							bestMove = true;
						}
					} else {
						if(playerYPosition > 0) {
							xSpeed = 0;
							ySpeed = 3;
						}
					}
				}
				if(moves[iCount] == "moveLeft") {
					//xSpeed = -2;
					//ySpeed = 0;
					//moves.Remove("moveRight");
					//wallRight = true;
					if (Mathf.Abs(playerXPosition) > Mathf.Abs(playerYPosition)) {
						if(playerXPosition < 0) {
							xSpeed = -3;
							ySpeed = 0;
							bestMove = true;
						}
					} else {
						if(playerYPosition < 0) {
							xSpeed = -3;
							ySpeed = 0;
						}
					}
				}
				if(moves[iCount] == "moveRight") {
					//xSpeed = 2;
					//ySpeed = 0;
					//moves.Remove("moveLeft");
					//wallLeft = true;
					if (Mathf.Abs(playerXPosition) > Mathf.Abs(playerYPosition)) {
						if(playerXPosition > 0) {
							xSpeed = 3;
							ySpeed = 0;
							bestMove = true;
						}
					} else {
						if(playerYPosition > 0) {
							xSpeed = 3;
							ySpeed = 0;
						}
					}
				}

				iCount ++;
			}
		} else {
			//make the only move you can make
			switch (moves[0]) {
			case "moveUp":
				xSpeed = 0;
				ySpeed = 3;
				break;
			case "moveDown":
				xSpeed = 0;
				ySpeed = -3;
				break;
			case "moveRight":
				xSpeed = 3;
				ySpeed = 0;
				break;
			case "moveLeft":
				xSpeed = -3;
				ySpeed = 0;
				break;
			}
		}

		//if the x position is greater than the y position
		/*if (Mathf.Abs(playerXPosition) > Mathf.Abs(playerYPosition)) {
			
			if(playerXPosition > 0) {
				xSpeed = 2f;
				ySpeed = 0f;
				//wallLeft = true;
			} else {
				xSpeed = -2f;
				ySpeed = 0f;
				//wallRight = true;
			}
		}

		if (Mathf.Abs(playerXPosition) < Mathf.Abs(playerYPosition)) {
			
			if(playerYPosition > 0) {
				xSpeed = 0f;
				ySpeed = 2f;
				//ground = true;
			} else {
				xSpeed = 0f;
				ySpeed = -2f;
				//ceiling = true;
			}
		}

		if (wallRight) {
			if(!ground) {
				xSpeed = 0f;
				ySpeed = -2f;
			}
			if(!ceiling) {
				xSpeed = 0f;
				ySpeed = 2f;
			}
		}
		if (wallLeft) {
			if(!ground) {
				xSpeed = 0f;
				ySpeed = -2f;
			}
			if(!ceiling) {
				xSpeed = 0f;
				ySpeed = 2f;
			}
		}
		if (ceiling) {
			if(!wallLeft) {
				xSpeed = -2f;
				ySpeed = 0f;
			}
			if(!wallRight) {
				xSpeed = 2f;
				ySpeed = 0f;
			}
		}
		if (ground) {
			if(!wallLeft) {
				xSpeed = -2f;
				ySpeed = 0f;
			}
			if(!wallRight) {
				xSpeed = 2f;
				ySpeed = 0f;
			}
		}*/

		//transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
		rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "obstacle")
			//Debug.Log ("hit");
	}
}
