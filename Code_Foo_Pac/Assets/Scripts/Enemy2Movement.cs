using UnityEngine;
using System.Collections;

public class Enemy2Movement : MonoBehaviour {

	public float xSpeed = 0f;		// The speed the enemy moves at.
	public float ySpeed = 2f;
	public float direction = 0f;
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform topCheck;
	private Transform bottomCheck;
	private Transform leftCheck;
	private Transform rightCheck;

	private float playerXPosition;
	private float playerYPosition;
	
	private float xStartPosition;
	private float yStartPosition;
	
	private float currentXPos;
	private float currentYPos;
	
	private int tilesTravelled = 0;
	
	void Awake()
	{
		// Setting up the references.
		//ren = transform.Find("body").GetComponent<SpriteRenderer>();
		
		//topCheck = transform.Find("topCheck").transform;
		//bottomCheck = transform.Find("bottomCheck").transform;
		//leftCheck = transform.Find("leftCheck").transform;
		//rightCheck = transform.Find("rightCheck").transform;

		xStartPosition = Mathf.RoundToInt(transform.position.x);
		yStartPosition = Mathf.RoundToInt(transform.position.y);

		
		playerXPosition = xStartPosition - GameObject.Find("player").transform.position.x;
		playerYPosition = yStartPosition - GameObject.Find("player").transform.position.y;
		
		//xSpeed = Mathf.Round (Random.value);
		//direction = Mathf.Round(Random.value);
	}
	
	void FixedUpdate () {
		
		//ceiling = Physics2D.Linecast(transform.position, topCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//ground = Physics2D.Linecast(transform.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//wallRight = Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//wallLeft = Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//need to add collision logic
		if (tilesTravelled == 1) {

			playerXPosition = GameObject.Find("player").transform.position.x - xStartPosition;
			playerYPosition = GameObject.Find("player").transform.position.y - yStartPosition;

			tilesTravelled = 0;
		}

		//if the x position is greater than the y position
		if (Mathf.Abs(playerXPosition) > Mathf.Abs(playerYPosition)) {
			
			if(playerXPosition > 0) {
				xSpeed = 2f;
				ySpeed = 0f;
			} else {
				xSpeed = -2f;
				ySpeed = 0f;
			}
			
		} else {
			
			if(playerYPosition > 0) {
				xSpeed = 0f;
				ySpeed = 2f;
			} else {
				xSpeed = 0f;
				ySpeed = -2f;
			}
		}

		//Debug.Log (xSpeed);

		rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);

		currentXPos = Mathf.RoundToInt (transform.position.x) - xStartPosition;
		currentYPos = Mathf.RoundToInt (transform.position.y) - yStartPosition;

		//Debug.Log (currentXPos);
		
		if (currentXPos == 1 || currentYPos == 1 || currentXPos == -1 || currentYPos == -1) {
			//Debug.Log ("1 new tile");
			tilesTravelled += 1;
			xStartPosition = Mathf.RoundToInt(transform.position.x);
			yStartPosition = Mathf.RoundToInt(transform.position.y);
		}
	}
}
