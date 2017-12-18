using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializationControll : MonoBehaviour {

	public GameObject BasicGuyObject;
	public GameObject KnightObject;
	private List<GameObject> RespawnPoints = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		RespawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Respawn"));
		
		InitializePlayer(KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L,
						 KeyCode.B, KeyCode.N, KeyCode.M,
						 LayerMask.NameToLayer("Player1"), PlayerStateController.PlayerClass.BasicGuy);
						 
		InitializePlayer(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
						 KeyCode.F, KeyCode.G, KeyCode.H,
						 LayerMask.NameToLayer("Player2"), PlayerStateController.PlayerClass.Knight);
						 
		InitializePlayer(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
						 KeyCode.Keypad0, KeyCode.Keypad2, KeyCode.Keypad3,
						 LayerMask.NameToLayer("Player3"), PlayerStateController.PlayerClass.BasicGuy);
	}
	
	void InitializePlayer(KeyCode keyUp, KeyCode keyDown, KeyCode keyLeft, KeyCode keyRight,
							KeyCode keyHit, KeyCode keyAction1, KeyCode keyAction2,
							int playerLayer, PlayerStateController.PlayerClass playerClass)
	{
		GameObject player = null; 
		
		if(playerClass == PlayerStateController.PlayerClass.BasicGuy)
			player = Instantiate(BasicGuyObject, RandomSpawn(), Quaternion.Euler(0, 0, 0)) as GameObject;
		if(playerClass == PlayerStateController.PlayerClass.Knight)
		{
			player = Instantiate(KnightObject, RandomSpawn(), Quaternion.Euler(0, 0, 0)) as GameObject;
			player.GetComponent<KnightAbilityControll>().Action1 = keyAction1;
			
		}
		
		MovementScript playerMov = player.GetComponent<MovementScript>();
		playerMov.keyUp = keyUp;
		playerMov.keyDown = keyDown;
		playerMov.keyLeft = keyLeft;
		playerMov.keyRight = keyRight;
		
		WeaponControl playerWeapon = player.GetComponent<WeaponControl>();
		playerWeapon.KeyHit = keyHit;
		playerWeapon.KeyAction2 = keyAction2;
		
		player.layer = playerLayer;
	}
	
	Vector3 RandomSpawn()
	{
		int randIndex = Random.Range(0, RespawnPoints.Count);
		Vector3 spawnPoint = RespawnPoints[randIndex].GetComponent<Transform>().position;
		RespawnPoints.Remove(RespawnPoints[randIndex]);
		return spawnPoint;
	}
}
