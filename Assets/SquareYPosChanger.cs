using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareYPosChanger : MonoBehaviour {

    private void Awake()
    {
        MovingGameModel.OnPositionsUpdated += PositionsUpdatedHandler;
    }
    // Use this for initialization
    void Start ()
    {
        //RequestMoveEventOnClick.OnMoveRequested += MoveRequestedHandler;
        
    }

    private void PositionsUpdatedHandler (Dictionary<string, float> positions)
    {
        Debug.Log(gameObject.name + " heard about new position: " + positions[gameObject.name]);
        
        // Determine my new y
        float myNewY = positions[gameObject.name];
        // store my existing x and z;
        float myX = transform.localPosition.x;
        float myZ = transform.localPosition.z;

        // my position = my x, the new y, my z;
        transform.localPosition = new Vector3(myX, myNewY, myZ);

    }

    /* private void MoveRequestedHandler( Dictionary<string, float> positions )
     {

       Debug.Log("I should move: " + positions[gameObject.name]);

       //transform.position.Set(transform.position.x, positions[gameObject.name], transform.position.z);
       transform.localPosition = new Vector3(transform.position.x, positions[gameObject.name], transform.position.z);
     }
     */

    private void MoveRequestedHandler(string wayToMove)
  {
      //Debug.Log(gameObject.name + " " + wayToMove);
      switch(wayToMove)
      {
          case "up":
              transform.position += transform.up;
              break;
          case "down":
              transform.position -= transform.up;
              break;
      }

  }
}
