using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public GameObject Player;

    public bool trouve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trouve)
        {
            Debug.Log("trouve");

            transform.position = Player.transform.position;
        }
        else
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        
    }
}
