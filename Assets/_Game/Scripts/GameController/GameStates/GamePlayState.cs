using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class GamePlayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GamePlayState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }



    public override void Enter()
    {
        base.Enter();
        Debug.Log("State: Play");
        _controller.PlayerUnitPrefab.playerSpeed = _controller.PlayerUnitPrefab.initialPlayerSpeed;
        _controller.PlayerUnitPrefab.gravity = _controller.PlayerUnitPrefab.initialGravityValue;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
    }

    public override void Tick()
    {
        base.Tick();
        if (_controller.PlayerUnitPrefab.isGameOver == true)
        {
            _controller.LoseScreen.SetActive(true);
            _controller.LoseAudio.Play();

        } else if (StateDuration >= _controller.WinConditionTime){
            _controller.WinScreen.SetActive(true);
            _controller.WinAudio.Play();

        }
        //Debug.Log("Checking for Win Condition");
        //Debug.Log("Checking for Lose Condition");

    }
}
