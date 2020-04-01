using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiverVie;
    public Transform receiverMort;
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
    
}
