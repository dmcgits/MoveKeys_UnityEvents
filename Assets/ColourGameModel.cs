using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ColourGameModel : MonoBehaviour
{
  public static event Action<Dictionary<string, float>> OnSquarePositionsChanged = delegate { };

  // Game is clicking to move squares up and down.
  // model hears about clicks, and sets square positions.
  // squares hear that the model updated, and they move.
  Dictionary<string, float> _squarePositions;
  // red, blue, green, purple
  private float _moveDistance = 0.25f;

  void Awake()
  {
    //Debug.Log("Model Awake");
    _squarePositions = new Dictionary<string, float>() {  { "red", 1.5f },
                                                          { "blue", 0.5f },
                                                          { "green", 3.0f },
                                                          { "purple", -0.5f } };


    RequestMoveEventOnClick.OnMoveRequested += MoveRequestedHandler;
    
  }
  void Start()
  {
    OnSquarePositionsChanged(_squarePositions);
  }

  private void MoveRequestedHandler( string direction )
  {
    // Move the positions in the direction a given value
    //Debug.Log("Model heard the new direction is " + direction);
    float distance = _moveDistance;

    if (direction == "down") distance *= -1;

    //_squarePositions["red"] += distance;
    //_squarePositions["blue"] += distance;
    //_squarePositions["purple"] += distance;
    //_squarePositions["green"] += distance;

    foreach ( string key in _squarePositions.Keys.ToList())
    {
      _squarePositions[key] += distance;
    }
    Debug.Log("_squarePositions[\"purple\"]: " + _squarePositions["purple"]);    

    OnSquarePositionsChanged(_squarePositions);
  }
}
