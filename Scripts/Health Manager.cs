using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour // This is a Parent Class for that's going to be inherited by Child Classes, Create different Health Managers for Player and Asteroid, and maybe to the space objects
{
    [SerializeField] protected private float  health; // Private variable access as we dont want to change the health of the object else where other than this script
    [SerializeField] protected GameObject healthBarPrefab; // Prefab for the health bar
    [SerializeField] protected Image healthBar;
    
    #region Health System

    public virtual void TakeDamage(float damage)
    {
        DisplayHealthBar();
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        //Debug.Log($"{gameObject.name} has taken {damage} damage");
    }

    public virtual void Die()
    {
        Destroy(gameObject);
       // Debug.Log($"{gameObject.name} has died");
    }
    
    public virtual void DisplayHealthBar() // if the Player or Space Object takes damage instantiate the Health bar Prefab 
    {
        if (healthBarPrefab != null && healthBarPrefab.activeInHierarchy == false) // If there is a health bar object attached
        {
            healthBarPrefab.SetActive(true); 
            healthBar.fillAmount = health;
        }
        
    }
    #endregion

    #region Varaible Getters 
    public virtual float GetHealth() // Use this method to get the health of the gameobject this script is attached to
    {
        return health;
    }

    #endregion

}
