using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// The MovingGameModel (a model of a game where you move things up and down)
// is the man-in-the-middle of user input and screen output
// 1) move button Sprite is clicked   
//        =>  2) Model (in middle) hears, changes virtual square positions, 
//                  => 3) square sprite hears 
                  
public class MovingGameModel : MonoBehaviour {
    // Define an event we will throw whenever new positions are set for squares. We will pass the dictionary
    // of square names and positions out to anyone listening.
    public static event Action<Dictionary<string, float>> OnPositionsUpdated = delegate { };

    // A list of Y positions for our squares, accessible by name (named by colour here, red/green etc)
    protected Dictionary<string, float> _squaresY;
    
    protected float _distanceToMove = 0.25f; // How far to move each time

    // Awake is where we do our earliest, internal setup
    private void Awake()
    {
       // Fill our dictionary with some square names and y positions.
        _squaresY = new Dictionary<string, float>() { {"red", 0.5f },
                                                     {"blue", 3.0f },
                                                     {"green", 1.0f },
                                                     {"purple", 0.0f }};
    }

    // Start is where we do setup that requires other objects to be awake and set up.
    void Start()
    {
        // Announce starting positions for squares.
        OnPositionsUpdated(_squaresY);

        
        // When a sprite with the RequestMoveEventOnClick component is clicked, it calls any
        // functions that are registered to handle the OnMoveRequested event.
        // We give it the name of our "MoveRequestedHandler" function, it will handle the event.
        RequestMoveEventOnClick.OnMoveRequested += MoveRequestedHandler;
    }

    // When RequestMoveEventOnClick throws the OnMoveRequested event, it passes an argument to any
    // handlers. It's defined in Action<type>, and for this event it was a string.
    // It's entered in the inspector on any sprite with the TMEOC component, and it should be "up" or "down" 
    private void MoveRequestedHandler (string direction)
    {
        // set up a local temporary copy of the distance to move
        float moveDistance = _distanceToMove;
        // Make that copy negative if direction == "down" 
        if (direction == "down") moveDistance *= -1;

        // This trick lets us loop through a dictionary. Requires `using System.Linq` at file start
        foreach (string key in _squaresY.Keys.ToList<string>())
        {
            // the key for each item in dictionary is used to access it and add the move distance
            _squaresY[key] += moveDistance;
        }

        // Announce updated positions for all squares.
        OnPositionsUpdated(_squaresY);
    }

}
