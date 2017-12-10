using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour 
{
	public enum Direction {Left, Right};
	
	private const string PLATFORMLAYER = "Platform";
	
	public bool isEnabled = true;
	
	public float movementSpeed;
	public float jumpHeight;
	public bool canDoublejump;
	private bool doublejumped;
	
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public LayerMask whatIsPlatform;
	private bool grounded = false;
	private bool ignoringPlatforms = false;
	
	public KeyCode keyUp;
	public KeyCode keyDown;
	public KeyCode keyLeft;
	public KeyCode keyRight;
	
	private Rigidbody2D rigidbody;
	private BoxCollider2D PlayerCollider;
	
	public Direction currentlyFacing;
	
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
		PlayerCollider = GetComponent<BoxCollider2D>();
	}
	
	void FixedUpdate()
	{
		bool groundedFloor = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		bool groundedPlatform = false;
		if(!ignoringPlatforms)
		{
			groundedPlatform = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsPlatform);
		}
		grounded = groundedFloor | groundedPlatform;
		
		BoxCollider2D PlayerCollider = GetComponent<BoxCollider2D>();
		bool overlapPlatform = Physics2D.OverlapBox(PlayerCollider.bounds.center, PlayerCollider.bounds.size - new Vector3(0.05f, 0.05f, 0.0f), 0.0f, whatIsPlatform);
		if(!overlapPlatform)
		{
			ignoringPlatforms = false;
			if(!Input.GetKey(keyDown))
			{
				Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(PLATFORMLAYER), false);
			}
		}
		else
		{
			ignoringPlatforms = true;
		}
	}
	
	void Update () 
	{	
		if(isEnabled)
		{
			if(grounded)
			{
				doublejumped = false;
			}
			
			if(Input.GetKey(keyRight))
			{
				rigidbody.velocity = new Vector2(movementSpeed, rigidbody.velocity.y);
				currentlyFacing = Direction.Right;
			}
			
			if(Input.GetKey(keyLeft))
			{
				rigidbody.velocity = new Vector2(-movementSpeed, rigidbody.velocity.y);
				currentlyFacing = Direction.Left;
			}
			
			if(Input.GetKeyDown(keyUp) && grounded)
			{
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHeight);
			}
			
			if(Input.GetKeyDown(keyUp) && !grounded && !doublejumped && canDoublejump)
			{
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHeight);
				doublejumped = true;
			}
			
			if(Input.GetKeyDown(keyDown))
			{
				Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(PLATFORMLAYER));
				ignoringPlatforms = true;
			}
		}
	}
}
