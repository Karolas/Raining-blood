using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour {

	private const float SWORDRAD = 1.0f;
	
	public bool isEnabled = true;

	public bool CanUseGuns;
	public bool CanUseMeleeWeapons;
	
	public GameObject Weapon;
	private Attack AttackScript;
	private int Durability;
	
	public string KeyHit;
	public string KeyAction2;
	
	private Transform playerPos;
	private MovementScript playerMov;
	private BoxCollider2D playerCol;
	private PlayerStateController playerState;
	private Animator animator;
	
	private Collider2D colWeaponSpawn;

	// Use this for initialization
	void Start () {
		playerPos = GetComponent<Transform>();
		playerMov = GetComponent<MovementScript>();
		playerCol = GetComponent<BoxCollider2D>();
		playerState = GetComponent<PlayerStateController>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isEnabled)
		{
			if(Input.GetButtonDown(KeyHit) && Weapon != null)
			{
				Attack();
			}
			if(colWeaponSpawn != null && colWeaponSpawn.gameObject.tag == "Gun" && CanUseGuns && Input.GetButtonDown(KeyAction2))
			{
				Attack attackScriptTemp = colWeaponSpawn.gameObject.GetComponent<Attack>();
				if(attackScriptTemp.available)
				{
					Weapon = attackScriptTemp.TakeGun();
					AttackScript = attackScriptTemp;
					Durability = AttackScript.Durability;
				}
			}
			if(colWeaponSpawn != null && colWeaponSpawn.gameObject.tag == "Sword" && CanUseMeleeWeapons && Input.GetButtonDown(KeyAction2))
			{
				Attack attackScriptTemp = colWeaponSpawn.gameObject.GetComponent<Attack>();
				if(attackScriptTemp.available)
				{
					Weapon = attackScriptTemp.TakeGun();
					AttackScript = attackScriptTemp;
					Durability = AttackScript.Durability;
					animator.SetBool("IsSwordTaken", true);
				}
			}
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(isEnabled)
		{
			if(col.gameObject.tag == "Gun")
			{
				colWeaponSpawn = col;
			}
			if(col.gameObject.tag == "Sword")
			{
				colWeaponSpawn = col;
			}
		}
    }
	
	void OnTriggerExit2D(Collider2D col) {
		if(colWeaponSpawn != null && colWeaponSpawn.gameObject.tag == "Gun")
		{
			colWeaponSpawn = null;
		}
		if(colWeaponSpawn != null && colWeaponSpawn.gameObject.tag == "Sword")
		{
			colWeaponSpawn = null;
		}
    }
	
	void Attack()
	{
		if(AttackScript.Name == "Pistol")
		{
			GameObject bullet;
			if(playerMov.currentlyFacing == MovementScript.Direction.Right)
			{
				bullet = Instantiate(AttackScript.Bullet, playerPos.position + new Vector3(1.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
			}
			else
			{
				bullet = Instantiate(AttackScript.Bullet, playerPos.position + new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.Euler(0, 180, 0)) as GameObject;
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
			}
			
			Durability--;
			if(Durability == 0)
			{
				Weapon = null;
				AttackScript = null;
			}
		}
		else if(AttackScript.Name == "Sword")
		{
			animator.SetTrigger("Attack");
			bool playerDamaged = false;
			Collider2D[] collidingPlayers = Physics2D.OverlapCircleAll(playerCol.bounds.center, SWORDRAD, LayerMask.GetMask("Player1", "Player2", "Player3", "Player4"));
			foreach(Collider2D enemyCollider in collidingPlayers) {
				if(playerMov.currentlyFacing == MovementScript.Direction.Left && enemyCollider.bounds.center.x < playerCol.bounds.center.x){
					Debug.Log("Attack to left");
					enemyCollider.gameObject.GetComponent<PlayerStateController>().KillPlayer();
					playerDamaged = true;
				}
				else if(playerMov.currentlyFacing == MovementScript.Direction.Right && enemyCollider.bounds.center.x > playerCol.bounds.center.x) {
					Debug.Log("Attack to right");
					enemyCollider.gameObject.GetComponent<PlayerStateController>().KillPlayer();
					playerDamaged = true;
				}
			}
			if(playerDamaged)
			{
				Durability--;
				if(Durability == 0)
				{
					Weapon = null;
					AttackScript = null;
					animator.SetBool("IsSwordTaken", false);
				}
			}
		}
	}
	
	public void DestroyWeapon() 
	{
		Durability = 0;
		Weapon = null;
		AttackScript = null;
	}
}
