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

    public Tile Spawn(Tile tile, bool spawnObstacle = false)
    {
        Quaternion newTileRotation = tile.gameObject.transform.rotation * Quaternion.LookRotation(currentTileDirection, Vector3.up);

        Tile newTile = Instantiate(tile, currentTileLocation, newTileRotation);
        prevTile = newTile.gameObject;
        currentTiles.Add(prevTile);
        if (tile.type == TileType.STRAIGHT)
        {
            currentTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentTileDirection);

        }

        if (spawnObstacle) SpawnObstacle();

        return newTile;
    }

    public void DeletePreviousTiles()
    {
        while (currentTiles.Count != 2)
        {
            GameObject tile = currentTiles[0];
            currentTiles.RemoveAt(0);
            Destroy(tile);
        }

        while (currentObstacles.Count != 1)
        {
            GameObject obstacle = currentObstacles[0];
            currentObstacles.RemoveAt(0);
            Destroy(obstacle);
        }
    }

    public void AddNewDirection(Vector3 direction)
    {
        currentTileDirection = direction;
        DeletePreviousTiles();

        Vector3 tilePlacementScale;
        if(prevTile.GetComponent<Tile>().type == TileType.SIDEWAYS)
        {
            tilePlacementScale = Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size / 2 + 
                (Vector3.one * startingTile.GetComponent<BoxCollider>().size.z / 2), currentTileDirection);
        } else
        {
            // left or right tiles
            tilePlacementScale = Vector3.Scale((prevTile.GetComponent<Renderer>().bounds.size - (Vector3.one * 2)) +
                (Vector3.one * startingTile.GetComponent<BoxCollider>().size.z / 2), currentTileDirection);
        }

        currentTileLocation += tilePlacementScale;

        int currentPathLength = Random.Range(minimumStraightTiles, maximumStraightTiles);
        for(int i = 0; i < currentPathLength; i++)
        {
            Spawn(startingTile.GetComponent<Tile>(), (i == 0) ? false : true);
        }

        Spawn(SelectRandomGameObjectFromList(turnTiles).GetComponent<Tile>(), false);
    }

    public void SpawnObstacle()
    {
        if (Random.value > 0.2f) return;

        GameObject obstaclePrefab = SelectRandomGameObjectFromList(obstacles);

        Quaternion newObjectRotation = obstaclePrefab.gameObject.transform.rotation * Quaternion.LookRotation(currentTileDirection, Vector3.up);

        GameObject obstacle = Instantiate(obstaclePrefab, currentTileLocation, newObjectRotation);
        currentObstacles.Add(obstacle);
    }

    public GameObject SelectRandomGameObjectFromList(List<GameObject> list)
    {
        if (list.Count == 0) return null;

        return list[Random.Range(0, list.Count)];
    }

}
