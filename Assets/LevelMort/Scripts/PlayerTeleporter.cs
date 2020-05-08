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
    public GameObject text;
    private Slider _lifebar;

    //private LevelGeneration levelGen;
    private void Start()
    {
        
      _lifebar = GameObject.Find("Lifebar").GetComponent<Slider>();
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
            playerTransform.playReviveSound();
            player.position = receiverVie.position;
            vie = !vie;
            playerVie.SetActive(true);
            playerMort.SetActive(false);
            playerTransform.vie = true;
            _lifebar.value = 1f;
        }
    }

    private void Update()
    {
        if (!vie && player.transform.position.y - bottom.transform.position.y <= -32.5f)
        {
            text.SetActive(true);
           
            if (Input.GetKeyDown( KeyCode.E))
            {
                stateChange();
                text.SetActive(false);
            }
            
        }

        if (vie && player.transform.position.y < -11)
        {
            stateChange();
        }
        if (vie && playerTransform.vie == false)
          stateChange();
    }
}
