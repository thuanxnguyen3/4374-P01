using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;

    // state variables here

    private void Awake()
    {
        _controller = GetComponent<GameController>();
        // state instantiation here
    }
}
