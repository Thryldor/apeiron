using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPisition;
    public GameObject[] rooms;
    public Camera mc;
    public GameObject Player;   
    private int direction;
    public GameObject reviverMort;
    public GameObject reviverVie;
    public float movementAmount;
    private float timeBtwRooms;
    public float startTimeBtwRooms = 0.25f;
    public GameObject minX;
    public GameObject maxX;
    public GameObject minY;
    public bool stopGeneration = false;
    public LayerMask room;
    private int downCounter = 0;
    public Transform start;
    private void Start()
    {
        int randStartPosition = Random.Range(0, startingPisition.Length);
        transform.position = startingPisition[randStartPosition].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        Player.transform.position = startingPisition[randStartPosition].position ;
        reviverMort.transform.position = startingPisition[randStartPosition].position;
        direction = Random.Range(1, 6);
    }

    public void Update()
    {
        if (timeBtwRooms <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRooms = startTimeBtwRooms;
        }
        else
        {
            timeBtwRooms -= Time.deltaTime;
        }
      //  mc.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,-20);
    }

    private void Move()
    {
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX.transform.position.x)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + movementAmount, transform.position.y);
                transform.position = newPos;
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
           
        } else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX.transform.position.x)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - movementAmount, transform.position.y);
                transform.position = newPos;
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else
        {
            downCounter++;
            if (transform.position.y > minY.transform.position.y)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter == 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randBottom = Random.Range(1, 4);
                        if (randBottom == 2)
                        {
                            randBottom = 3;
                        }
                        Instantiate(rooms[randBottom], transform.position, Quaternion.identity);
                    }
                   

                    
                }
                Vector2 newPos = new Vector2(transform.position.x , transform.position.y - movementAmount);
                transform.position = newPos;
                int rand = Random.Range(2, 3);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
                // Instantiate(Player, reviverVie.transform.position, Quaternion.identity);
                mc.transform.position = Player.transform.position;
                mc.transform.Translate(0,0,-20);
            }
           
        }
    }

    private void LateUpdate()
    {
        mc.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,-20);
    }
}
