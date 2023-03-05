using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class GamePlayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private float _timer = 60f;
    private float _timerCountdown;
    public GamePlayState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("State: Play");
        _controller.BackgroundAudio.Play();
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
            _controller.BackgroundAudio.Stop();
            _controller.LoseScreen.SetActive(true);
            _controller.LoseAudio.Play();
            _controller.PlayerUnitPrefab.gameObject.SetActive(false);

        }
        else if (StateDuration >= _controller.WinConditionTime){
            _controller.BackgroundAudio.Stop();
            _controller.WinScreen.SetActive(true);
            _controller.WinAudio.Play();
            _controller.PlayerUnitPrefab.gameObject.SetActive(false);
            

        }
        _timerCountdown = _timer - StateDuration;
        _controller.Timer.DisplayTimer(_timerCountdown);
        //Debug.Log("Checking for Win Condition");
        //Debug.Log("Checking for Lose Condition");

    }
}
