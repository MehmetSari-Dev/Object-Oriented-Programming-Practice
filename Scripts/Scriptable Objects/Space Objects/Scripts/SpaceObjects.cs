using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SpaceObjectType // Enum for different types of Space Objects
{
    Asteroid,
    Meteor,
    SpaceTrash,
    Astronaut
    
}
public abstract class SpaceObjects : ScriptableObject// Abstract class as there are going to be multiple different Space Objects 
{
    public GameObject spaceObjectPrefab; // Prefab of the Space Object
    public SpaceObjectType spaceObjectType; // Type of Space Object / Goes along with the enum 
    public int value;
    public string spaceObjectName; // Name of the Space Object
    [TextArea(15, 20)] // Text area for the description of the Space Object
    public string spaceObjectDescription; // Description of the Space Object

    

}
