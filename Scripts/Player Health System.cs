using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthSystem : HealthManager
{

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.fillAmount = health / 100f; 
        //Debug.Log($"{gameObject.name} has taken {damage} damage");
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject); // Destroy the player gameobject
        LevelManager.Instance.SetGameState(LevelManager.GameState.GameOver); // Set the game state to Game Over
       // Debug.Log($"{gameObject.name} has died");
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        GameObject gameObjectHit = other.gameObject; // Gets reference to the object that the Hits the Player

        if(gameObjectHit.CompareTag("Asteroid")) // If the asteriod hit the player then 
        {
            TakeDamage(10); // Take 10 damage from the asteroid
            AudioManager.Instance.PlayAudio(AudioManager.AudioType.ShipHitSFX); // Play the Player Hit SFX when the player gets hit by the asteroid or meteor
        }
    }
    

}
