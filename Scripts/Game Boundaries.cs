using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoundaries : MonoBehaviour
{
    private Vector2 gameBounds;
    private float objectWidth;
    private float objectHeight;
    private float minX, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        gameBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); // Gets the camera's boundaries 
        // Gets the GameObjects Size using the Spriterenderer's bound size function
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x ;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForScreenBoundaries();
    }

    void CheckForScreenBoundaries()
    {
        Vector3 objectPos = transform.position; // Gets the current position of the Object
        objectPos.x = Mathf.Clamp(objectPos.x, gameBounds.x * -1 - objectWidth, gameBounds.x + objectWidth ); // Clamps the Objects position to the Screen Boundaries 
        objectPos.y = Mathf.Clamp(objectPos.y, gameBounds.y * -1 - objectHeight, gameBounds.y + objectHeight  );
        transform.position = objectPos; // This is to make the transform of the object to never get out of the screen boundaries 

        // if the object goes out of the screen it changes the position of the obhject back on the other side
        if(objectPos.x > gameBounds.x || objectPos.y > gameBounds.y) // If the player's postion is more than the Max x OR max Y Boundaries then 'spawn' the object on the  other side
        {
            transform.position = -transform.position;
        }
        else if(objectPos.x < gameBounds.x  * -1 || objectPos.y <  gameBounds.y * -1) // Vice Versa of the first Statement, if the player is less than the Min x OR Y set the 'spawn' position on the positive side
        {
            transform.position = transform.position * -1; // technically the X or Y cords of the object would be negative so if we multiply a negative with a negative gives us a positive
        }
    }

   
}
