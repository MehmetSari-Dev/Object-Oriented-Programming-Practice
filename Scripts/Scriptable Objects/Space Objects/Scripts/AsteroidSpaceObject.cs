using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Asteroid", menuName = "Inventory System/Space Objects/Asteroid")]
public class AsteroidSpaceObject : SpaceObjects
{
    void Awake()
    {
        spaceObjectType = SpaceObjectType.Asteroid; // Set the type of the Space Object to Asteroid for the enum 
    }

    
}
