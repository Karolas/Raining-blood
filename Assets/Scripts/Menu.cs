using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject playMenu;

	public void Play()
    {
		mainMenu.SetActive(false);
		playMenu.SetActive(true);
    }
	
	public void HowToPlay()
    {
        
    }
	
	public void Options()
    {
        
    } 

    public void Quit()
    {
        Application.Quit();
    }
	
	public void PlayToMainMenu()
    {
        mainMenu.SetActive(true);
		playMenu.SetActive(false);
    }
	
	public void PlayGame()
	{
		SceneManager.LoadScene("Game");
	}
}
