using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptions : MonoBehaviour {

	public static PlayerStateController.PlayerClass player1 = PlayerStateController.PlayerClass.BasicGuy;
	public static PlayerStateController.PlayerClass player2 = PlayerStateController.PlayerClass.BasicGuy;
	public static PlayerStateController.PlayerClass player3 = PlayerStateController.PlayerClass.None;
	public static PlayerStateController.PlayerClass player4 = PlayerStateController.PlayerClass.None;
	
	public static float musicVolume = 1;
	public static float soundVolume = 1;
}
