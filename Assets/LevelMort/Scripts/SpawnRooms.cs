using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;
    void Update()
    {
        Collider2D detection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (detection == null && levelGen.stopGeneration == true)
        {
            int rand = Random.Range(1, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
