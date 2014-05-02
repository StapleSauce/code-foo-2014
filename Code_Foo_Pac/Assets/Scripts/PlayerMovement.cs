using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	public float moveForce = 200f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 8f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

	private Transform wallCheck;
	private bool wall = false;

	private Transform frontCheck;
	private bool facingWall = false;

	private Animator anim;					// Reference to the player's animator component.
	
	private int direction = 1;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		wallCheck = transform.Find("wallCheck");
		frontCheck = transform.Find("frontCheck");
		anim = GetComponent<Animator>();
	}
	
	
	void Update()
	{

		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		wall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Wall"));
		facingWall = Physics2D.Linecast(transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Wall"));

		// If the jump button is pressed and the player is grounded then the player should jump.
		if (Input.GetButtonDown ("Jump") && (grounded || wall))
			jump = true;

		if (grounded)
			anim.SetBool("Jump", false);
	}
	
	
	void FixedUpdate ()
	{
		//need to tweak wall jump

		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		if (Input.GetButton ("Horizontal") || !grounded) {
			// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if (h * rigidbody2D.velocity.x < maxSpeed)
				// ... add a force to the player.
				rigidbody2D.AddForce (Vector2.right * h * moveForce);

			// If the player's horizontal velocity is greater than the maxSpeed...
			if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed)
				// ... set the player's velocity to the maxSpeed in the x axis.
				rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		} else {
			rigidbody2D.AddForce (Vector2.right * 0f);
			rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
			anim.SetFloat("Speed", 0f);
		}

		if(!wall){
			// If the input is moving the player right and the player is facing left...
			if(h > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(h < 0 && facingRight)
				// ... flip the player.
				Flip();
			else if(facingWall && !grounded)
				Flip();
		}

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetBool("Jump", true);

			if(wall) {
				rigidbody2D.AddForce(new Vector2((jumpForce / 1.5f) * direction, jumpForce));
			} else {
				// Add a vertical force to the player.
				rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			}
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		direction = direction * -1;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D other) {
		
		//Debug.Log ("hit");
		if (other.gameObject.tag == "enemy") {
			anim.SetTrigger("Dead");
			Destroy(this.gameObject);
			Time.timeScale = 0;
		}
	}
}
