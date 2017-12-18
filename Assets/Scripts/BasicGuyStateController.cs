using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGuyStateController : MonoBehaviour {
	public bool IsAlive = true;
	public float RespawnTime;
	
	private ParticleSystem deathParticles;
	private MovementScript playerMov;
	private WeaponControl playerWeapon;
	private SpriteRenderer playerSprite;
	private Transform playerTransform;
	
	void Start() {
		deathParticles = GetComponent<ParticleSystem>();
		playerMov = GetComponent<MovementScript>();
		playerWeapon = GetComponent<WeaponControl>();
		playerSprite = GetComponent<SpriteRenderer>();
		playerTransform = GetComponent<Transform>();
	}
	
	public void KillPlayer(PlayerStateController killerState) {
		if(IsAlive) {
			deathParticles.Play();
			IsAlive = false;
			killerState.playerScore++;
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
		playerTransform.position = FindRespawnPoint();
		IsAlive = true;
		playerMov.isEnabled = true;
		playerWeapon.isEnabled = true;
		playerSprite.enabled = true;
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
