using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Space Trash", menuName = "Inventory System/Space Objects/Space Trash")]
public class SpaceTrashSpaceObjects : SpaceObjects
{
    void Awake()
    {
        spaceObjectType = SpaceObjectType.SpaceTrash; // Set the type of the Space Object to Space Trash for the enum
    }



    
}
