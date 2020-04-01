using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetRoom : MonoBehaviour
{
    private GameObject Player;
    public GameObject leButton;
    public GameObject[] entries;
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
