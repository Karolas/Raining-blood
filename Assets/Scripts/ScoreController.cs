using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	
	private string initialString;
	public GameObject player;
	
	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
		
		initialString = scoreText.text;
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
		{
			scoreText.text = initialString + player.GetComponent<PlayerStateController>().playerScore;
		}
	}
}
