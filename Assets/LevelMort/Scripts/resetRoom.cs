using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetRoom : MonoBehaviour
{
    private GameObject Player;
    public GameObject leButton;
    public GameObject[] entries;
    public GameObject room;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            if ((room.transform.position.x - Player.transform.position.x - (room.transform.position.x - transform.position.x) <= 1) &&
            (room.transform.position.x - Player.transform.position.x - (room.transform.position.x - transform.position.x) >= -1) &&
            (room.transform.position.y - Player.transform.position.y - (room.transform.position.y - transform.position.y) <= -0.8f) &&
            (room.transform.position.y - Player.transform.position.y - (room.transform.position.y - transform.position.y) >= -2.8f)
            )
            {
                RoomType.done = false;
                leButton.SetActive(false);
                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i].SetActive(true);
                }
            }
        }
        
    }
}
