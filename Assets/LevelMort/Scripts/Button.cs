using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] gosthPoints;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Player.transform.position.x <= (transform.position.x + 1.2f) &&
            Player.transform.position.x >= (transform.position.x - 1.2f) &&
            Player.transform.position.y <= (transform.position.y + 1.2f) &&
            Player.transform.position.y >= (transform.position.y - 1.2f))
        {
            for (int i = 0; i < gosthPoints.Length; i++)
            {
                gosthPoints[i].SetActive(false);
                RoomType.done = true;
            }
            
        }
    }
}
