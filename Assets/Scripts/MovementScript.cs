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
	public Transform groundCheck2;
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
	private Animator animator;
	private Transform playerTransform;
	
	public Direction currentlyFacing;
	
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
		PlayerCollider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		playerTransform = GetComponent<Transform>();
	}
	
	void FixedUpdate()
	{
		bool groundedFloor = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround) |
							 Physics2D.OverlapCircle(groundCheck2.position, groundCheckRadius, whatIsGround);
		bool groundedPlatform = false;
		if(!ignoringPlatforms)
		{
			groundedPlatform = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsPlatform) |
							   Physics2D.OverlapCircle(groundCheck2.position, groundCheckRadius, whatIsPlatform);
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
			
			rigidbody.velocity = new Vector2(0.0f, rigidbody.velocity.y);
			
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
				animator.SetTrigger("Jump");
			}
			
			if(Input.GetKeyDown(keyUp) && !grounded && !doublejumped && canDoublejump)
			{
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHeight);
				doublejumped = true;
				animator.SetTrigger("Jump");
			}
			
			if(Input.GetKeyDown(keyDown))
			{
				Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(PLATFORMLAYER));
				ignoringPlatforms = true;
			}
			
			SetAnimations();
		}
	}
	
	void SetAnimations() 
	{
		animator.SetFloat("Speed", rigidbody.velocity.x);
		if(grounded && rigidbody.velocity.y == 0) 
		{
			animator.SetBool("IsGrounded", true);
		}
		else {
			animator.SetBool("IsGrounded", false);
		}
		if(rigidbody.velocity.x > 0)
		{
			playerTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		}
		else if(rigidbody.velocity.x < 0)
		{
			playerTransform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
		}
	}
}
