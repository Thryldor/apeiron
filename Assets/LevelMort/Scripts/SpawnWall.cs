using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnWall : MonoBehaviour
{
    public GameObject[] entries;
    public GameObject[] gosthWall;
    private GameObject Player;
    public GameObject[] exits;
    public GameObject theButton;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {


            if (Player.transform.position.x <= (transform.position.x + 1f) &&
                Player.transform.position.x >= (transform.position.x - 1f) &&
                Player.transform.position.y <= (transform.position.y + 1f) &&
                Player.transform.position.y >= (transform.position.y - 1f)
            )
            {
                if (!RoomType.done)
                {
                    foreach (var wall in gosthWall)
                    {
                        wall.SetActive(true);
                    }
                    theButton.SetActive(true);

                    foreach (var entry in entries)
                    {
                      entry.SetActive(false);
                    }

                    foreach (var exit in exits)
                    {
                        exit.SetActive(true);
                    }
                }
            }
        }

    }
}
