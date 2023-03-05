using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [field: SerializeField]
    public float WinConditionTime { get; private set; } = 60f;

    [field: SerializeField]
    public GameObject WinScreen { get; private set; }

    [field: SerializeField]
    public AudioSource WinAudio;

    [field: SerializeField]
    public GameObject LoseScreen { get; private set; }

    [field: SerializeField]
    public AudioSource LoseAudio;

    [field: SerializeField]
    public AudioSource BackgroundAudio;

    [field: SerializeField]
    public PlayerController PlayerUnitPrefab { get; private set; }
    [field: SerializeField]
    public Transform PlayerUnitSpawnLocation { get; private set; }
    [field: SerializeField]
    public UnitSpawner UnitSpawner { get; private set; }

    [field: SerializeField]
    public TileSpawner TileSpawner { get; private set; }

    [field: SerializeField]
    public InputManager InputManager { get; private set; }

    [field: SerializeField]
    public Timer Timer { get; private set; }
}

