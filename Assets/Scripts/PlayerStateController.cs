using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {

	public bool IsAlive = true;
	public float RespawnTime;
	
	private ParticleSystem deathParticles;
	private MovementScript playerMov;
	private WeaponControl playerWeapon;
	private SpriteRenderer playerSprite;
	
	void Start() {
		deathParticles = GetComponent<ParticleSystem>();
		playerMov = GetComponent<MovementScript>();
		playerWeapon = GetComponent<WeaponControl>();
		playerSprite = GetComponent<SpriteRenderer>();
	}
	
	public void KillPlayer() {
		if(IsAlive) {
			deathParticles.Play();
			IsAlive = false;
			playerMov.isEnabled = false;
			playerWeapon.isEnabled = false;
			playerSprite.enabled = false;
			StartCoroutine(Respawn());
		}
	}
	
	IEnumerator Respawn()
	{
		Debug.Log("Respawn started");
		yield return new WaitForSeconds(RespawnTime);
		IsAlive = true;
		playerMov.isEnabled = true;
		playerWeapon.isEnabled = true;
		playerSprite.enabled = true;
	}
}
