using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour {

	public bool CanUseGuns;
	public bool CanUseMeleeWeapons;
	
	public GameObject Weapon;
	private Attack AttackScript;
	
	public KeyCode KeyAction1;
	
	private Transform playerPos;
	private MovementScript playerMov;

	// Use this for initialization
	void Start () {
		playerPos = GetComponent<Transform>();
		playerMov = GetComponent<MovementScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyAction1))
		{
			Attack();
		}
	}
	
	void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "Gun" && CanUseGuns)
		{
			Weapon = col.gameObject;
			AttackScript = Weapon.GetComponent<Attack>();
		}
    }
	
	void Attack()
	{
		if(AttackScript.Name == "Pistol")
		{
			GameObject bullet;
			if(playerMov.currentlyFacing == MovementScript.Direction.Right)
			{
				bullet = Instantiate(AttackScript.Bullet, playerPos.position + new Vector3(1.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0));
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
			}
			else
			{
				bullet = Instantiate(AttackScript.Bullet, playerPos.position + new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.Euler(0, 180, 0));
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
			}
		}
	}
}
