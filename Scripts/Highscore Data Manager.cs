using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreDataManager : MonoBehaviour
{
    public static HighscoreDataManager Instance { get; private set; } // To make sure only one Instance of this Class exists 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void UpdateHighScoreData() // Calls this method in the game state functions = GameOver and Win
    {
        if(PlayerPrefs.HasKey("CurrentHighScore")) // If there is a  Saved player prefs with the key  Current High score which holds an interger, which is the score
        {
            if(LevelManager.Instance.GetScore() > PlayerPrefs.GetInt("CurrentHighScore")) // if the current game sessions score is higher than the saved highscore then...
            {
                PlayerPrefs.SetInt("CurrentHighScore", LevelManager.Instance.GetScore()); // Set the current session's score as the Current Highscore
            }
        }
        else
        {
            PlayerPrefs.SetInt("CurrentHighScore", LevelManager.Instance.GetScore()); // If there is no saved Highscore in player prefs then set the current score as the highscore
        }

        // Updates the Ui Text with the current score and the higscore 
        UiManager.Instance.gameOverStateCurrentScoreText.text = "Your Score: " + LevelManager.Instance.GetScore().ToString();
        UiManager.Instance.winStateCurrentScoreText.text = "Your Score: " + LevelManager.Instance.GetScore().ToString();
        UiManager.Instance.gameOverStateHighScoreText.text = "Highscore: " +  PlayerPrefs.GetInt("CurrentHighScore").ToString();
        UiManager.Instance.winStateHighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("CurrentHighScore").ToString(); 

       

    } 

    public void ClearHighScoreData() // Method for clearing the data for player prefs
    {
        if(PlayerPrefs.HasKey("CurrentHighScore"))
        {
            PlayerPrefs.DeleteKey("CurrentHighScore");
        }
    }

   

}
