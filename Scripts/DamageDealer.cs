using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter2D(Collider2D collider) // If the missile hits something
    {
        GameObject gameObjectHit = collider.gameObject; // Gets reference to the object that the missile hit
        HealthManager healthManager = gameObjectHit.GetComponent<HealthManager>(); // gets the HealthSystem component of the object that the missile hit
        
        
        try{
            if (healthManager != null  && !gameObjectHit.CompareTag("Player")) // If the object that the missile hit a gameobjetc that has a HealthManager component and is not the player
            {
                healthManager.TakeDamage(damage);
                Destroy(gameObject); // Destory the Missile after hitting something
            }
            
        }
        catch(System.Exception ex)
        {
            Debug.LogError("Error in DamageSystem Script: " + ex.Message);
        }
    }
}
