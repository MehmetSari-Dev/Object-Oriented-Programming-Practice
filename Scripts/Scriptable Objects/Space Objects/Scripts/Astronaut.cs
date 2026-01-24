using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Astronaut", menuName = "Inventory System/Space Objects/Space Astronaut")]
public class Astronaut : SpaceObjects
{
    
    void Awake()
    {
        spaceObjectType = SpaceObjectType.Astronaut; // Set the type of the Space Object to Astronaut for the enum
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
