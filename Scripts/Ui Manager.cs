using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // For Scene Management




public class UiManager : MonoBehaviour
{
    // Singleton instance
    public static UiManager Instance { get; private set; } // To make sure only one Instance of this Class exists | When Referencing this called use UiManager.instance

    #region Ui Variables
    [Header("Ui Variables for Game Panels")]
    public GameObject _PausePanelScreen; 
    public GameObject _GameOverPanelScreen;
    public GameObject _WinPanelScreen;
    public GameObject _OptionsPanelScreen;
    public GameObject _HowToPlayPanelScreen;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] public TextMeshProUGUI gameOverStateCurrentScoreText;
    [SerializeField] public TextMeshProUGUI gameOverStateHighScoreText;
    [SerializeField] public TextMeshProUGUI winStateCurrentScoreText;
    [SerializeField] public TextMeshProUGUI winStateHighScoreText;
    
    
 


    #endregion

    #region Inventory Variables
    public InventorySystemManager inventory; // Reference to the Inventory Scriptable Object
    [SerializeField] GameObject inventoryUiPanel; // The Inventory UI Panel
    Dictionary<InventorySpaceManager, GameObject> inventoryDisplayUI = new Dictionary<InventorySpaceManager, GameObject >(); // Dictionary to store the inventory items and their amounts
    

    #endregion
    void Awake()
    {
        if (Instance == null) // If there is no instance of this Script/Class 
        {
            Instance = this; // The Instance this script
            
        }
        else
        {
            Destroy(gameObject); // If there is a intance of this script, then destroy this script 
        }

         
    }
     
    // Start is called before the first frame update
    void Start()
    {
        try
        {
           DisplayInventoryUi(); 
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error in LevelManager: " + ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventoryUi(); // Update the Inventory UI
        UpdateHUDElements(); // Update the HUD Elements
    }

    #region UI Button Functions 
    
    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false; // To Exit the Editor when in Play Mode 
        Debug.Log("QUIT");
    }

    public void LoadLevel(string SceneName) // For the Menu Main Button 
    {
        SceneManager.LoadScene(SceneName); // Load the Scene
    }

    public void ContinueGame() // For the Continue Button
    {
       LevelManager.Instance.SetGameState(LevelManager.GameState.Playing); // Set the Game State to Playing
    }
    #endregion
   
    #region Inventory Methods

    private void DisplayInventoryUi()
    {
        for(int i = 0; i < inventory.Inventory.Count; i++) // For Loop essentially Checks for all the iventory of the player
        {
            GameObject item = Instantiate(inventory.Inventory[i].item.spaceObjectPrefab, inventoryUiPanel.transform); // Instantiate the item ui prefab that attached to the Sapace object prefabs
            inventoryDisplayUI.Add(inventory.Inventory[i], item); // Add the item to the UI
            item.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Inventory[i].amount.ToString("n0"); // Set the amount of the item in the UI
        }
            
    }

    private void UpdateInventoryUi()
    {
        for(int i = 0; i < inventory.Inventory.Count; i++) // For Loop essentially Checks for all the iventory of the player
        {
            if(inventoryDisplayUI.ContainsKey(inventory.Inventory[i])) // Check if the space object is already in the inventory if so
            {
                inventoryDisplayUI[inventory.Inventory[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Inventory[i].amount.ToString(); // Update the amount of the item thats already in the inventory
            }
            else // if it's not in the inventory then it would create it 
            {
                GameObject item = Instantiate(inventory.Inventory[i].item.spaceObjectPrefab, inventoryUiPanel.transform); // Instantiate the item ui prefab that attached to the Sapace object prefabs
                item.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Inventory[i].amount.ToString(); // Set the amount of the item in the UI
                inventoryDisplayUI.Add(inventory.Inventory[i], item); // Add the item to the UI
                
            }
        }
        
    }


    #endregion

    #region HUD UI Functions

    public void UpdateHUDElements() // Update the Score
    {
        score.text = "Score: " + LevelManager.Instance.GetScore().ToString("n0"); // Set the score text to the score value
        timer.text = "Timer: " + LevelManager.Instance.GetTimer().ToString("n0"); // Set the timer text to the timer value
        level.text = "Level: " + LevelManager.Instance.GetLevel().ToString("n0"); // Set the level text to the level value
    }


    #endregion
}
