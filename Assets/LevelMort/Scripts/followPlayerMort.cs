using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerMort : MonoBehaviour
{
    public bool vie = true;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if (player && !vie)
        {
            transform.position = player.transform.position;
        }
    
    }

    public void swap()
    {
        vie = !vie;
    }
}
