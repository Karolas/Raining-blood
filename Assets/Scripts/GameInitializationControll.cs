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
		
		if(GameOptions.player1 != PlayerStateController.PlayerClass.None)
		{
			InitializePlayer("Player1 up", "Player1 down", "Player1 left", "Player1 right",
						 "Player1 hit", "Player1 action1", "Player1 action2",
						 "Player1", GameOptions.player1);
		}
		
		if(GameOptions.player2 != PlayerStateController.PlayerClass.None)
		{
			InitializePlayer("Player2 up", "Player2 down", "Player2 left", "Player2 right",
						 "Player2 hit", "Player2 action1", "Player2 action2",
						 "Player2", GameOptions.player2);
		}
						 
		//InitializePlayer(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
		//				 KeyCode.Keypad0, KeyCode.Keypad2, KeyCode.Keypad3,
		//				 LayerMask.NameToLayer("Player3"), PlayerStateController.PlayerClass.BasicGuy);
	}
	
	void InitializePlayer(string keyUp, string keyDown, string keyLeft, string keyRight,
							string keyHit, string keyAction1, string keyAction2,
							string playerLayerName, PlayerStateController.PlayerClass playerClass)
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
		
		player.layer = LayerMask.NameToLayer(playerLayerName);
		
		GameObject.Find(playerLayerName + "Score").GetComponent<ScoreController>().player = player;
	}
	
	Vector3 RandomSpawn()
	{
		int randIndex = Random.Range(0, RespawnPoints.Count);
		Vector3 spawnPoint = RespawnPoints[randIndex].GetComponent<Transform>().position;
		RespawnPoints.Remove(RespawnPoints[randIndex]);
		return spawnPoint;
	}
}
