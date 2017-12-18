using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {
	public enum PlayerClass {None, BasicGuy, Knight};

	public PlayerClass playerClass;
	
	public bool IsAlive
	{
		get 
		{ 
			if(playerClass == PlayerClass.Knight)
			{
				return knightController.IsAlive; 
			}
			else
			{
				return basicGuyController.IsAlive; 
			}
		}
		set 
		{ 
			if(playerClass == PlayerClass.Knight)
			{
				knightController.IsAlive = value; 
			}
			else
			{
				basicGuyController.IsAlive = value; 
			}
		}
	}
	public float RespawnTime;
	
	private ParticleSystem deathParticles;
	private MovementScript playerMov;
	private WeaponControl playerWeapon;
	private SpriteRenderer playerSprite;
	private Transform playerTransform;
	private KnightStateController knightController;
	private BasicGuyStateController basicGuyController;
	
	void Start() {
		deathParticles = GetComponent<ParticleSystem>();
		playerMov = GetComponent<MovementScript>();
		playerWeapon = GetComponent<WeaponControl>();
		playerSprite = GetComponent<SpriteRenderer>();
		playerTransform = GetComponent<Transform>();
		if(playerClass == PlayerClass.Knight)
		{
			knightController = GetComponent<KnightStateController>();
		}
		if(playerClass == PlayerClass.BasicGuy)
		{
			basicGuyController = GetComponent<BasicGuyStateController>();
		}
	}
	
	public void KillPlayer() {
		if(playerClass == PlayerClass.Knight) {
			knightController.KillPlayer();
		}
		if(playerClass == PlayerClass.BasicGuy) {
			basicGuyController.KillPlayer();
		}
	}
}
