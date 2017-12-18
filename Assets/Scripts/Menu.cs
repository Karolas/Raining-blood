using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject playMenu;
	public GameObject infoMenu;
	public GameObject howToPlayMenu;
	public GameObject creditsMenu;
	public GameObject optionsMenu;
	public GameObject audioMenu;

	public void Play()
    {
		mainMenu.SetActive(false);
		playMenu.SetActive(true);
    }
	
	public void Info()
    {
        mainMenu.SetActive(false);
		infoMenu.SetActive(true);
    }
	
	public void Options()
    {
        mainMenu.SetActive(false);
		optionsMenu.SetActive(true);
    } 
	
	public void Controls()
    {
        
    } 
	
	public void Audio()
    {
        audioMenu.SetActive(true);
		optionsMenu.SetActive(false);
    } 

    public void Quit()
    {
        Application.Quit();
    }
	
	public void HowToPlay()
	{
		howToPlayMenu.SetActive(true);
		infoMenu.SetActive(false);
	}
	
	public void Credits()
	{
		creditsMenu.SetActive(true);
		infoMenu.SetActive(false);
	}
	
	public void PlayToMainMenu()
    {
        mainMenu.SetActive(true);
		playMenu.SetActive(false);
    }
	
	public void InfoToMainMenu()
    {
        mainMenu.SetActive(true);
		infoMenu.SetActive(false);
    }
	
	public void OptionsToMainMenu()
    {
        mainMenu.SetActive(true);
		optionsMenu.SetActive(false);
    }
	
	public void HowToPlayToInfo()
	{
		howToPlayMenu.SetActive(false);
		infoMenu.SetActive(true);
	}
	
	public void CreditsToInfo()
	{
		creditsMenu.SetActive(false);
		infoMenu.SetActive(true);
	}
	
	public void AudioToOptions()
	{
		audioMenu.SetActive(false);
		optionsMenu.SetActive(true);
	}
	
	public void PlayGame()
	{
		int playerCount = 0;
		
		if(GameOptions.player1 != PlayerStateController.PlayerClass.None) playerCount++;
		if(GameOptions.player2 != PlayerStateController.PlayerClass.None) playerCount++;
		if(GameOptions.player3 != PlayerStateController.PlayerClass.None) playerCount++;
		if(GameOptions.player4 != PlayerStateController.PlayerClass.None) playerCount++;
		
		if(playerCount > 1)
			SceneManager.LoadScene("Game");
	}
	
	public void SetPlayer1(int value)
	{
		if(value == 0)
			GameOptions.player1 = PlayerStateController.PlayerClass.None;
		if(value == 1)
			GameOptions.player1 = PlayerStateController.PlayerClass.BasicGuy;
		if(value == 2)
			GameOptions.player1 = PlayerStateController.PlayerClass.Knight;
	}
	
	public void SetPlayer2(int value)
	{
		Debug.Log(value);
		if(value == 0)
			GameOptions.player2 = PlayerStateController.PlayerClass.None;
		if(value == 1)
			GameOptions.player2 = PlayerStateController.PlayerClass.BasicGuy;
		if(value == 2)
			GameOptions.player2 = PlayerStateController.PlayerClass.Knight;
	}
	
	public void SetPlayer3(int value)
	{
		if(value == 0)
			GameOptions.player3 = PlayerStateController.PlayerClass.None;
		if(value == 1)
			GameOptions.player3 = PlayerStateController.PlayerClass.BasicGuy;
		if(value == 2)
			GameOptions.player3 = PlayerStateController.PlayerClass.Knight;
	}
	
	public void SetPlayer4(int value)
	{
		if(value == 0)
			GameOptions.player4 = PlayerStateController.PlayerClass.None;
		if(value == 1)
			GameOptions.player4 = PlayerStateController.PlayerClass.BasicGuy;
		if(value == 2)
			GameOptions.player4 = PlayerStateController.PlayerClass.Knight;
	}
	
	public void SetMusic(float value)
	{
		GameOptions.musicVolume = value;
	}
	
	public void SetSounds(float value)
	{
		GameOptions.soundVolume = value;
	}
}
