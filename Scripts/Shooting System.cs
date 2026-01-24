using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform spawnOffsetPosition1;
    [SerializeField] Transform spawnOffsetPosition2;

    Rigidbody2D rb;
    [SerializeField] float fireRate;
    [SerializeField] float missileSpeed;
    private float lastFireTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        try
        {
            
            if (Time.time >= lastFireTime + fireRate && (Input.GetButtonDown("Fire1")))
            {
                Fire();
                lastFireTime = Time.time;
            }

            
        
        }
        catch(System.Exception ex)
        {
            Debug.LogError("Error in MisileShooting Script: " + ex.Message);
        }
        
    }

    void Fire()
    {
        AudioManager.Instance.PlayAudio(AudioManager.AudioType.MissileSFX); // Play the missile SFX when the missile is fired
       
            // For The cannon Position 1
            GameObject missile1 = Instantiate(missilePrefab, spawnOffsetPosition1.transform.position, transform.rotation);
            rb = missile1.GetComponent<Rigidbody2D>();
            rb.velocity = missile1.transform.up * missileSpeed;
           // Debug.Log("Missile Fired from Position 1");
        
            // FOr the second cannon
            GameObject missile2 = Instantiate(missilePrefab, spawnOffsetPosition2.transform.position, transform.rotation);
            rb = missile2.GetComponent<Rigidbody2D>();
            rb.velocity = missile2.transform.up * missileSpeed;
           // Debug.Log("Missile Fired from Position 2");

           // Destroy the missiles after 10 seconds
           Destroy(missile1, 5f);
           Destroy(missile2, 5f);
        
       
    }
}
