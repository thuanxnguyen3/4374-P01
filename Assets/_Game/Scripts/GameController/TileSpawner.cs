using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    public int tileStartCount = 10;
    [SerializeField]
    public int minimumStraightTiles = 3;
    [SerializeField]
    public int maximumStraightTiles = 15;
    [SerializeField]
    public GameObject startingTile;
    [SerializeField]
    public List<GameObject> turnTiles;
    [SerializeField]
    public List<GameObject> obstacles;

    private Vector3 currentTileLocation = Vector3.zero;
    private Vector3 currentTileDirection = Vector3.forward;
    private GameObject prevTile;

    private List<GameObject> currentTiles;
    private List<GameObject> currentObstacles;

    private void Start()
    {
        currentTiles = new List<GameObject>();
        currentObstacles = new List<GameObject>();

        Random.InitState(System.DateTime.Now.Millisecond);

        

        //SpawnTile();
    }

    public Tile Spawn(Tile tile, bool spawnObstacle)
    {
        Tile newTile = Instantiate(tile, currentTileLocation, Quaternion.identity);
        prevTile = newTile.gameObject;
        currentTiles.Add(prevTile);
        currentTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentTileDirection);

        return newTile;
    }

}
