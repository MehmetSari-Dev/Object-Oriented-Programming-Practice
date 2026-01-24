using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectSpawnerManager : MonoBehaviour
{
    public static SpaceObjectSpawnerManager Instance { get; private set; } // To make sure only one Instance of this Class exists | When Referencing this called use UiManager.instance

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

    [Header("Hazardous(Asteroids & Meteors) Space Object variables")]
     Rigidbody2D rb;
    [SerializeField] GameObject target;
    [SerializeField]  float minSpawnDis;
    [SerializeField]  float maxSpawnDis;
    [SerializeField] int currentNumberOfHazardousSpaceObjects;
    [SerializeField]  GameObject[] EnemySpaceObjectPrefab; 
    [SerializeField] public List<GameObject> spawnedHazardousSpaceObject = new List<GameObject>(); // List of spawned Hazardous space objects
    private float randomSpeed;

    [Header("Space Trash Variables")]
    [SerializeField] GameObject[] spaceTrashPrefab;
    [SerializeField] GameObject[] spaceTrashSpawnPoints;
    [SerializeField] public List<GameObject> spawnedSpaceTrash = new List<GameObject>(); 

  



    
    // Start is called before the first frame update
    void Start()
    {   
        try 
        {
            SpawnHazardousSpaceObject(); 
            SpawnSpaceTrash(); 
            currentNumberOfHazardousSpaceObjects = spawnedHazardousSpaceObject.Count; // Sets the current number of hazardous space objects would equal to the number of spawned Space oject based on the Level
        }
        catch(System.Exception ex)
        {
            Debug.LogError("Error in Space Object" + ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        
    }

    private void SpawnHazardousSpaceObject()
    {
        for(int i = 0; i < LevelManager.Instance.GetLevel(); i++) // It would spawn asteroids and/or meteors based on the level 
        {
            float randomSpawnDistance = Random.Range(minSpawnDis, maxSpawnDis);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            randomSpeed  = Random.Range(0.1f, 0.5f);
            Vector2 spawnPos = (Vector2)target.transform.position + randomDirection * randomSpawnDistance; // Spawns the Space Object relative to the position of the target (which can be the player or the spaceships) and at a random distance
            int randomIndex = Random.Range(0, EnemySpaceObjectPrefab.Length); // Spawns a random Space ship or Asteroid or meteor from the Array of GameObjects
            GameObject spaceObject = Instantiate(EnemySpaceObjectPrefab[randomIndex], spawnPos, Quaternion.identity);
            rb = spaceObject.GetComponent<Rigidbody2D>(); // Gets the Rb of the Instantiated SpaceObject 
            rb.angularVelocity = Random.Range(-50f, 50f); // Randomizes the rotation of the object
            rb.velocity = randomDirection * randomSpeed;
            spawnedHazardousSpaceObject.Add(spaceObject); // Adds the spawned space object to the list of spawned space objects
            //Debug.Log($"Spawned {gameObject.name}");
        }
    }

    public void DestroyedHazardousSpaceObject(GameObject destroyedgameObject)
    {
        currentNumberOfHazardousSpaceObjects--; // Decrements the number of hazardous space objects
        spawnedHazardousSpaceObject.Remove(destroyedgameObject); // Remove the object from the list of spawned objects
        
    }

    public void DestroyedSpaceTrash(GameObject destroyedSpaceTrash)
    {
        spawnedSpaceTrash.Remove(destroyedSpaceTrash); // Remove the object from the list of spawned objects
       // var spaceTrash =  destroyedSpaceTrash.GetComponent<SpaceObjectsController>();
        //LevelManager.Instance.SetScore(spaceTrash.spaceObject.spaceObjectType.); 
        if(spawnedSpaceTrash.Count <=0) // If current numbver of hazardous space Obj is less than or equal to 0 then Next Level Function is called 
        {
            NextLevel();
        }
    }


    private void SpawnSpaceTrash()
    {
        for(int i = 0; i < LevelManager.Instance.GetLevel(); i++)  // This for loop, loops through the level and spawns the space trash 
        {
            
                int randomIndex = Random.Range(0, spaceTrashSpawnPoints.Length); // Gets a random index from the array of spawn points
                Vector2 spawnpos = spaceTrashSpawnPoints[randomIndex].transform.position; // Spawn position ofr the space station, an empty gameobject would be put around the scene and those will be the spawn points 
                int randomTrashIndex = Random.Range(0, spaceTrashPrefab.Length); // Gets a random index from the array of space trash prefabs
                GameObject spaceTrash = Instantiate(spaceTrashPrefab[randomTrashIndex], spawnpos, Quaternion.identity); // This should spawn in the space stations that are in the array 
                spawnedSpaceTrash.Add(spaceTrash); // Adds the spawned space station to the list of spawned space stations

                foreach(GameObject trash in spawnedSpaceTrash) // For each space trash in the list of spawned space trash
                {
                    Rigidbody2D rb = trash.GetComponent<Rigidbody2D>(); // Gets the Rb of the Instantiated SpaceObject
                    rb.angularVelocity = Random.Range(-50f, 50f); // Randomizes the rotation of the object
                    Vector2 randomDirection = Random.insideUnitCircle.normalized; // Gets a random direction for the object to move in
                    randomSpeed  = Random.Range(0.1f, 0.5f); // Gets a random speed for the object to move at
                    rb.velocity = randomDirection * randomSpeed; // Sets the velocity of the object
                }
            
         }
        
    }

    private void NextLevel() // Method for next level 
    {
        LevelManager.Instance.SetLevel(LevelManager.Instance.GetLevel() + 1); // Increments the level by 1
        SpawnHazardousSpaceObject(); // Spawns the next level of hazardous space objects
        SpawnSpaceTrash(); // Spawns the next level of space trash
        currentNumberOfHazardousSpaceObjects = spawnedHazardousSpaceObject.Count; // Sets the current number of hazardous space objects would equal to the number of spawned Space oject based on the Level 
        AudioManager.Instance.PlayAudio(AudioManager.AudioType.LevelCompleteSFX); // Plays the next level sound effect
    }


    
}
