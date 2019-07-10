using System;
using System.Collections.Generic;
using UnityEngine;

public class RequestMoveEventOnClick : MonoBehaviour {

    public static event Action<string> OnMoveRequested = delegate { };

    public string wayToMove = "no_event";

    private void OnMouseUpAsButton()
    {
        //Debug.Log(wayToMove);
        OnMoveRequested(wayToMove);
    }

}
