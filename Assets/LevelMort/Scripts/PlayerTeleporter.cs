using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    public GameObject playerVie;
    public GameObject playerMort;
    public Transform player;
    public Transform receiverVie;
    public Transform receiverMort;
    public GameObject bottom;
    public bool vie;
    public Player.Player playerTransform;
    public Camera mc;
    //private LevelGeneration levelGen;
    private void Start()
    {
       // receiverMort = levelGen.getStart();
    }

    public void stateChange()
    {
        if (vie)
        {
            player.position = receiverMort.position;
            vie = !vie;
            playerVie.SetActive(false);
            playerMort.SetActive(true);
            playerTransform.vie = false;
        }
        else
        {
            player.position = receiverVie.position;
            vie = !vie;
            playerVie.SetActive(true);
            playerMort.SetActive(false);
            playerTransform.vie = true;
        }
    }

    private void Update()
    {
        if (!vie && player.transform.position.y - bottom.transform.position.y <= -16f)
        {
            stateChange();
        }

        if (vie && player.transform.position.y < -11)
        {
            stateChange();
        }
    }
}
