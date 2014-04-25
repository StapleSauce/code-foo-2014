using UnityEngine;
using System.Collections;

public class Enemy1Movement : MonoBehaviour {

	public float xSpeed = 0f;		// The speed the enemy moves at.
	public float ySpeed = 0f;
	public float direction = 0f;
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform topCheck;
	private Transform bottomCheck;
	private Transform leftCheck;
	private Transform rightCheck;

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

		xStartPosition = Mathf.FloorToInt(transform.position.x);
		yStartPosition = Mathf.FloorToInt(transform.position.y);

		xSpeed = Mathf.Round (Random.value);
		direction = Mathf.Round(Random.value);
	}
	
	void FixedUpdate () {

		//Debug.Log (xSpeed);

		//ceiling = Physics2D.Linecast(transform.position, topCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//ground = Physics2D.Linecast(transform.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//wallRight = Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		//wallLeft = Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		//need to add collision logic
		if (tilesTravelled == 1) {
			xSpeed = Mathf.Round (Random.value);
			direction = Mathf.Round (Random.value);
			tilesTravelled = 0;
		}

		if (xSpeed > 0) {
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
		}

		rigidbody2D.velocity = new Vector2(xSpeed * direction, ySpeed * direction);

		currentXPos = Mathf.FloorToInt (transform.position.x) - xStartPosition;
		currentYPos = Mathf.FloorToInt (transform.position.y) - yStartPosition;
		
		if (currentXPos == 1 || currentYPos == 1 || currentXPos == -1 || currentYPos == -1) {
			//Debug.Log ("1 new tile");
			//Debug.Log(Mathf.FloorToInt(transform.position.x));
			tilesTravelled += 1;
			xStartPosition = Mathf.FloorToInt (transform.position.x);
			yStartPosition = Mathf.FloorToInt (transform.position.y);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "obstacle")
			//Debug.Log ("hit");
			direction = direction * -1;
	}
}
