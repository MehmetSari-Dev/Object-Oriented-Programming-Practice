using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Singleton instance
     public static LevelManager Instance { get; private set; } // To make sure only one Instance of this Class exists 

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


    #region Game Variables 
    [Header("Game Variables")]
    [SerializeField] private int score ;
    public bool timerOn = false; // Boolean that checks if the timer is on or off, Public so that other scripts can check if the time is on or off
    [SerializeField] private float timer; // Private Timer Variable so that it cant be changed from other scripts, Serialized so that it can be seen in the inspector and change
    [SerializeField] private int level = 1; // Private Time Limit Variable so that it cant be changed from other scripts, Serialized so that it can be seen in the inspector and changed

    #endregion

    #region Inventory Variables
    [Header("Inventory Variables")]
    public InventorySystemManager inventory; // Reference to the Inventory Scriptable Object
    #endregion


    
    // Start is called before the first frame update
    void Start()
    {
       ResetGame(); // Call the OnStart Function
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInputs(); // Checks Player Inputs
        Timer(); // Call the Timer Function
       CheckScore();
    }
    
  



    #region Player Inputs 
    private void CheckPlayerInputs() // Function to Check for Inputs from the Player
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // If the Escape Key is Pressed
        {
            AudioManager.Instance.PlayAudio(AudioManager.AudioType.KeyboardButtonPressSFX); // Play the Ui SFX when the Game is in Paused State
            if (currentGameState == GameState.Playing) // If the Game is in Playing State
            {
                SetGameState(GameState.Paused); // Set the Game State to Paused
            }
            else if (currentGameState == GameState.Paused && UiManager.Instance._OptionsPanelScreen.activeInHierarchy == false ) // If the Game is in Paused State
            {
                SetGameState(GameState.Playing); // Set the Game State to Playing
            }
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            HighscoreDataManager.Instance.ClearHighScoreData();
        }
    }

    #endregion

    #region Game States 
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Win,
        GameStart
    }
    public GameState currentGameState;

    public void SetGameState(GameState gameState) // Function to Change to Different Game States
    {
        currentGameState = gameState;

        switch (currentGameState) // Switch Statement for different Game States and their functions 
        {
            case GameState.Playing:
                PlayState();
                break;
            case GameState.Paused:
                PausedState();
                break;
            case GameState.GameOver:
               GameOverState();
                break;
            case GameState.Win:
               WinState();
                break;
            case GameState.GameStart:
                GameStartState();
                break;
                
        }        
    }

    private void PlayState()
    {
        UiManager.Instance._PausePanelScreen.SetActive(false); // Hides the Pause Panel when the Game is in Playing State
        UiManager.Instance._GameOverPanelScreen.SetActive(false); // Hides the Game Over Panel when the Game is in Playing State
        UiManager.Instance._WinPanelScreen.SetActive(false); // Hides the Win Panel when the Game is in Playing State
        Time.timeScale = 1; // This is Unity's Time Scale, 1 is to play and 0 is to pause the game  
        AudioManager.Instance.musicSource.UnPause(); // UnPause the Music when the Game is in Playing State
        Debug.Log("Game is now in Playing state.");
    }

    private void PausedState()
    {
        UiManager.Instance._PausePanelScreen.SetActive(true); // Shows the Pause Panel when the Game is in Paused State
        AudioManager.Instance.musicSource.Pause(); // Pause the Music when the Game is in Paused State
        Time.timeScale = 0; // This is Unity's Time Scale, 1 is to play and 0 is to pause the game
        Debug.Log("Game is now in Paused state.");
    }
    private void GameOverState()
    {
        HighscoreDataManager.Instance.UpdateHighScoreData(); // Calls the Highscore Function to update the Highscore data 
        UiManager.Instance._GameOverPanelScreen.SetActive(true); // Shows the Game Over Panel when the Game is in Game Over State
        Time.timeScale = 0; // This is Unity's Time Scale, 1 is to play and 0 is to pause the game
        Debug.Log("Game is now in Game Over state.");
        AudioManager.Instance.PlayMusic(AudioManager.AudioType.LoseMusic);
    }
    private void WinState()
    {
        HighscoreDataManager.Instance.UpdateHighScoreData();
        UiManager.Instance._WinPanelScreen.SetActive(true); // Shows the Win Panel when the Game is in Win State
        Time.timeScale = 0; // This is Unity's Time Scale, 1 is to play and 0 is to pause the game
        Debug.Log("Game is now in Win state.");
        AudioManager.Instance.PlayMusic(AudioManager.AudioType.WinMusic);   
      
    }

    private void GameStartState()
    {
        if(UiManager.Instance._HowToPlayPanelScreen.activeInHierarchy == false) // If the How To Play Panel is not active
        {
            UiManager.Instance._HowToPlayPanelScreen.SetActive(true);  // Shows the How To Play Panel when the Game is in Game Start State
        }
        Time.timeScale = 0; // This is Unity's Time Scale, 1 is to play and 0 is to pause the game
        Debug.Log("Game is now in Game Start state.");
    }

    #endregion

    #region GamePlay Functions

    void Timer()
    {
        if(timerOn) // If the Timer is on
        {
            if(timer >= 0) // If the Timer is greater than 0 then it would start the timer
            {
                timer -= Time.deltaTime; // Decreases the Timer 
            }
            else // If the Timer is Less than 0 or equal 
            { 
                timerOn = false;
                timer = 0; // Sets the Timer to 0
                SetGameState(GameState.GameOver); // Sets the Game State to Game Over
            }
            
        }   
    }

    private void ResetGame()
    {
        SetGameState(GameState.GameStart); // Set the Game State to Playing when the Game Starts
        timerOn = true; // Sets the Timer to On
        timer = 300; // Sets the Timer to 60 seconds
        score = 0; // Sets the Score to 0
        level = 1; // Sets the Level to 1
        inventory.Inventory.Clear(); // Clears the Inventory when the Game Starts
        if(AudioManager.Instance.musicSource.isPlaying == false) // If the Music is not playing
        {
            AudioManager.Instance.PlayMusic(AudioManager.AudioType.GameMusic); // Play the Game Music when the Game is in Playing State
        }
        Debug.Log("Level Reset");
    }

    private void CheckScore() // Function that will check the score of the Level
    {
        if(currentGameState == GameState.Playing) // if the game state is playing 
        {
            if(score >= 500) // and the player gets a score of 500
            {
                SetGameState(GameState.Win); // then game state is win 
            }
        }
    }

    

    


    #endregion

    #region Getters for Game Variables
    // Getters for Private Variables
    public float GetTimer() // Getter for the Timer
    {
        return timer; 
    }

    public int GetScore() // Getter for the score  
    {
        return score; 
    }
    public void SetScore(int newScore) // Setter for the score
    {
        score += newScore; 
    }

    public int GetLevel() // Getter for the level
    {
        return level; 
    }

    public void SetLevel(int newLevel) // Setter for the level
    {
        level = newLevel; 
    }

    

    #endregion
}



