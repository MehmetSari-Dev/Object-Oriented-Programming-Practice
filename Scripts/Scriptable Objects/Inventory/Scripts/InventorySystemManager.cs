using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory System", menuName = "Inventory System/Inventory")]
public class InventorySystemManager : ScriptableObject
{
    public List<InventorySpaceManager> Inventory = new List<InventorySpaceManager>(); // List of Space Objects in the inventory Creates a New Memory slot that would store a list of space objects that we would want to add to the inventory

    public void AddItem(SpaceObjects item, int amountToAdd) // Method to add an item to the inventory
    {
        bool itemCollected = false; // A boolean to check if the item already exists in the inventory
        for(int i = 0; i < Inventory.Count; i++) // This for loop checks the Inventory 
        {
            if (Inventory[i].item == item) // If Statement essentially checks if the same space object already exists in the inven
            {
                Inventory[i].AddItem(amountToAdd); // Adds the amount of the space object to the inventory
                itemCollected = true; // Sets the boolean to true
                break; // Breaks out of the loop
            }
            
        }
        if(itemCollected == false)
        {
            Inventory.Add(new InventorySpaceManager(item, amountToAdd)); // And if the space object is not collected then it adds it to the inventory
        }
                
        
       
        Debug.Log("Added " + item.name + " to inventory."); 
    }
}

[System.Serializable] // To see this class in the inspector
public class InventorySpaceManager // A Class that would manage the Inventory whenever space objects are collected
{
   public SpaceObjects item; // The space Object that is being managed
   public int amount; // How many of that space objects is collected 

    public InventorySpaceManager(SpaceObjects spaceObjectItem, int amountToAdd) // Constructor to initialize the space object and the amount
    {
        item = spaceObjectItem; // Sets the space object
        amount = amountToAdd; // Sets the amount of the space object
    }

    public void AddItem(int amountToAdd) // Method to add an item to the inventory
    {
        amount += amountToAdd; // Adds the amount of the space object
        Debug.Log("Added " + amountToAdd + " " + item.name + " to inventory."); 
    }
}
