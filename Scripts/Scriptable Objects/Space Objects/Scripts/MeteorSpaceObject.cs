using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Meteor", menuName = "Inventory System/Space Objects/Meteor")]
public class MeteorSpaceObject : SpaceObjects // This Scriptable Object Class inherits from the Space Object class and implements the IDamageable interface
{
    void Awake()
    {
        spaceObjectType = SpaceObjectType.Meteor; // Set the type of the Space Object to Meteor for the enum
    }
    
}
