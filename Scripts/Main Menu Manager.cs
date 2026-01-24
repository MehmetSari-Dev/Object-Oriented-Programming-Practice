using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Toggle audioMuteToggle;
    [SerializeField] AudioSource mainMenuMusic;
    
    

    #region Main Menu Functions 
    // FOR MAIN MENU 

    public void PlayGame()
    {
        SceneManager.LoadScene("Cosmic Express");
    }

    public void QuitGame()
    {
        Application.Quit();
       // EditorApplication.isPlaying = false; // To Exit the Editor when in Play Mode 
        Debug.Log("QUIT");
    }

    public void LoadMainMenu() // For the Menu Main Button 
    {
        SceneManager.LoadScene("Main Menu"); 
    }

    public void MuteMainMenuMusic() // For the Mute Button 
    {
        if(audioMuteToggle.isOn) // If the Toggle is On
        {
           mainMenuMusic.mute = true; // Mute the Audio Source
        }
        else
        {
            mainMenuMusic.mute = false; // UnMute the Audio Source
        }
        
    }


    #endregion
}
