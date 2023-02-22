using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    // this is our 'constructor', called when this state is created
    public GameSetupState(GameFSM stateMachine, GameController controller)
    {
        // hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("State: Game Setup");
        /*
        Instantiate(_controller.PlayerUnitPrefab, _controller.PlayerUnitSpawnLocation.position,
        _controller.PlayerUnitSpawnLocation.rotation);
        */
        _controller.UnitSpawner.Spawn(_controller.PlayerUnitPrefab, _controller.PlayerUnitSpawnLocation);

        for (int i = 0; i < _controller.TileSpawner.tileStartCount; i++)
        {
            _controller.TileSpawner.Spawn(_controller.TileSpawner.startingTile.GetComponent<Tile>());
        }

        _controller.TileSpawner.Spawn(_controller.TileSpawner.SelectRandomGameObjectFromList(_controller.TileSpawner.turnTiles).GetComponent<Tile>());
        /*
        _controller.TileSpawner.Spawn(_controller.TileSpawner.turnTiles[0].GetComponent<Tile>());
        _controller.TileSpawner.AddNewDirection(Vector3.left);*/
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
        _stateMachine.ChangeState(_stateMachine.PlayState);
    }
}
