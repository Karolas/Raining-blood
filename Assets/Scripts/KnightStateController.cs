using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStateController : MonoBehaviour {

	public bool IsAlive = true;
	public bool IsArmorBroken = false;
	public float RespawnTime;
	
	private ParticleSystem deathParticles;
	private MovementScript playerMov;
	private WeaponControl playerWeapon;
	private SpriteRenderer playerSprite;
	private Transform playerTransform;
	private Animator animator;
	private KnightAbilityControll knightAbility;
	
	void Start() {
		deathParticles = GetComponent<ParticleSystem>();
		playerMov = GetComponent<MovementScript>();
		playerWeapon = GetComponent<WeaponControl>();
		playerSprite = GetComponent<SpriteRenderer>();
		playerTransform = GetComponent<Transform>();
		animator = GetComponent<Animator>();
		knightAbility = GetComponent<KnightAbilityControll>();
	}
	
	public void KillPlayer(PlayerStateController killerState) {
		if(IsAlive && !knightAbility.IsDefending) {
			if(IsArmorBroken)
			{
				deathParticles.Play();
				killerState.playerScore++;
				IsAlive = false;
				playerMov.isEnabled = false;
				playerWeapon.isEnabled = false;
				playerSprite.enabled = false;
				
				playerWeapon.DestroyWeapon();
				animator.SetBool("IsSwordTaken", false);
				
				StartCoroutine(Respawn());
			}
			else 
			{
				IsArmorBroken = true;
				playerMov.canDoublejump = true;
				animator.SetBool("IsArmorBroken", true);
			}
		}
	}
	
	IEnumerator Respawn()
	{
		Debug.Log("Respawn started");
		yield return new WaitForSeconds(RespawnTime);
		playerTransform.position = FindRespawnPoint();
		IsAlive = true;
		IsArmorBroken = false;
		playerMov.isEnabled = true;
		playerMov.canDoublejump = false;
		playerWeapon.isEnabled = true;
		playerSprite.enabled = true;
		animator.SetBool("IsArmorBroken", false);
	}
	
	Vector3 FindRespawnPoint() 
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
		
		GameObject selectedRespawn = null;
		float maxDistance = float.MinValue;
		
		foreach(GameObject respawnPoint in respawnPoints)
		{
			float minPlayerDistance = float.MaxValue;
			foreach(GameObject player in players)
			{
				if(player != this.gameObject) 
				{
					float distance = Vector3.Distance(player.GetComponent<Transform>().position, respawnPoint.GetComponent<Transform>().position);
					if(minPlayerDistance > distance) minPlayerDistance = distance;
				}
			}
			
			if(minPlayerDistance > maxDistance)
			{
				maxDistance = minPlayerDistance;
				selectedRespawn = respawnPoint;
			}
		}
		
		return selectedRespawn.GetComponent<Transform>().position;
	}
}
