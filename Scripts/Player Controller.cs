using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Inventory Variables
    public InventorySystemManager inventory; // Reference to the Inventory Scriptable Object
    #endregion
    #region Game Variables
    // Movement Variables
    [SerializeField] float thrustForce;
    [SerializeField] float slideForce;
    
    // RB
     Rigidbody2D rb;

     #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
            try
            {
                Movement();
                RotateToMouse();
            }
            catch(System.Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
           
            
        
        
    }

    #region Player Movement Methods
    void RotateToMouse()
    {
        Vector3 mousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition); // Gets the Vectors for the position of the mouse relative to the sceen size
        mousePos.z = 0f; // set Z Pos of the mouse to 0 as we're working in 2D
        
        Vector2 direction = (mousePos - transform.position).normalized; // Calculates the mouses position to the position of the Spaceship 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculates the angle of the direction the mouses is facing

        rb.MoveRotation(angle-90f); // Sets the angle of the spaceship to the position of the mouse  

        
    }

    void Movement()
    {
        float thrustInput = Input.GetAxisRaw("Vertical"); // Up OR Down for the movement 

        float slideInput = Input.GetAxisRaw("Horizontal"); // Left or Right for the movement 

        rb.AddForce(transform.up * thrustInput * thrustForce, ForceMode2D.Force); // AddForce Method, applies force to the RB of the spaceship to move it forward

        rb.AddForce(transform.right * slideInput * slideForce, ForceMode2D.Force); // Adds Force to the horizontal movement 

        if(thrustInput != 0 || slideInput != 0)  // if the player is moving then play the audio
        {
            if(AudioManager.Instance.audioSource.isPlaying == false) // If the audio is not playing then play the spaceship thrust SFX
            {
                AudioManager.Instance.PlayAudio(AudioManager.AudioType.SpaceShipSFX); // Play the spaceship thrust SFX
            }
            
        }
        
    }

    #endregion
    #region Inventory Methods

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject; // Gets the Gameobject that the player collided with
        var spaceObjectScript = collidedObject.GetComponent<SpaceObjectsController>(); // Gets the SpaceObjectController script component from the collided object
        if(spaceObjectScript.spaceObject.spaceObjectType ==  SpaceObjectType.SpaceTrash || spaceObjectScript.spaceObject.spaceObjectType == SpaceObjectType.Astronaut ) // If the Gameobject we collided with has the script and is a space Trash then add to inventory
        {
            LevelManager.Instance.SetScore(spaceObjectScript.spaceObject.value); // Sets the score and Updates it
           
            if(collidedObject.gameObject.tag == "Astronaut") // If the space object is an astronaut then play the human voice SFX
            {
                AudioManager.Instance.PlayAudio(AudioManager.AudioType.HumanVoiceSFX); // Play the Astronaut SFX when the player collects the astronaut
            }
            else
            {
                AudioManager.Instance.PlayAudio(AudioManager.AudioType.PointGainSFX); // Play the Space Trash SFX when the player collects the space trash
            }
            inventory.AddItem(spaceObjectScript.spaceObject, 1); // Adds the space object to the inventory
            SpaceObjectSpawnerManager.Instance.DestroyedSpaceTrash(collision.gameObject); // Calls the DestroyedHazardousSpaceObject method from the SpaceObjectSpawnerManager
            Destroy(collision.gameObject, 0.1f); // Destroys the space object after adding it to the inventory
            
        } 
    }

    private void OnApplicationQuit() // Clears the inventory when the player quits the game 
    {
        inventory.Inventory.Clear(); // Clears the inventory  
        Debug.Log("Inventory Cleared"); 
    }

   




    #endregion
}
