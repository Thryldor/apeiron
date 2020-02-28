using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class spawnPlayer : MonoBehaviour
{
    public bool vie;
    public GameObject player;
    private bool spawned = false;
    public LevelGeneration levelGen;

    private void Update()
    {
        if (levelGen.stopGeneration && !spawned)
        {
            player.SetActive(true);
            spawned = true;
        }
    }
    public void Switch()
    {
        vie = !vie;
    }
}