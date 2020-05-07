using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public GameObject[] playerSpawn;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        for(int i = 1; i < playerSpawn.Length-1; i++)
        {
            if(player.transform.position.x < playerSpawn[i].transform.position.x && player.transform.position.x > playerSpawn[i - 1].transform.position.x)
            {
                transform.position = playerSpawn[i].transform.position;
            }
        }
    }
}
