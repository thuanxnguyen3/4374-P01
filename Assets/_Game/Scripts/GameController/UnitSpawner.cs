using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public PlayerController Spawn(PlayerController unitPrefab, Transform location)
    {
        // spawn and hold on to the component tyhpe
        PlayerController newUnit = Instantiate(unitPrefab, location.position, location.rotation);

        //TODO do setup here if needed, spawn effects, etc.
        return newUnit;
    }
}
