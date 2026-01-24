using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectHealthSystem : HealthManager
{

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.fillAmount = health / 150f; 
        //Debug.Log($"{gameObject.name} has taken {damage} damage");
    }
    public override void Die()
    {
        base.Die();
        SpaceObjectSpawnerManager.Instance.DestroyedHazardousSpaceObject(gameObject);
        AudioManager.Instance.PlayAudio(AudioManager.AudioType.ExplosionSFX); // Play the explosion SFX when the asteroid or meteor is destroyed
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        GameObject gameObjectHit = other.gameObject; // Gets reference to the object that the Hits the Player

        if(gameObjectHit.CompareTag("Projectile")) // If the projectile hits the asteroid
        {
            TakeDamage(10); // Take 10 damage from the asteroid
            Debug.Log("Asteroid Hit by Projectile");
            AudioManager.Instance.PlayAudio(AudioManager.AudioType.SpaceObjecImpactSFX); // Play the Player Hit SFX when the player gets hit by the asteroid or meteor
        }
    }
}
