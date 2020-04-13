using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] gosthPoints;
    public GameObject room;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if ((room.transform.position.x - Player.transform.position.x - (room.transform.position.x - transform.position.x ) <= 1 ) &&
            (room.transform.position.x - Player.transform.position.x - (room.transform.position.x - transform.position.x ) >=-1 ) &&
            (room.transform.position.y - Player.transform.position.y - (room.transform.position.y - transform.position.y ) <= -0.8f )&&
            (room.transform.position.y - Player.transform.position.y - (room.transform.position.y - transform.position.y ) >=-2.8f ))
        {
            for (int i = 0; i < gosthPoints.Length; i++)
            {
                gosthPoints[i].SetActive(false);
                RoomType.done = true;
            }
            
        }
    }
}
