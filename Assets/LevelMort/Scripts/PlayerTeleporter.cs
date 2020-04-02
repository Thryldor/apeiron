using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiverVie;
    public Transform receiverMort;
    public GameObject bottom;
    public bool vie;
    public void stateChange()
    {
        if (vie)
        {
            player.position = receiverMort.position;
            vie = !vie;
        }
        else
        {
            player.position = receiverVie.position;
            vie = !vie;
        }
    }

    private void Update()
    {
        if (!vie && player.transform.position.y < bottom.transform.position.y - 18.5f)
        {
            stateChange();
        }
    }
}
