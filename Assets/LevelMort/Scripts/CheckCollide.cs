using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CheckCollide : MonoBehaviour
{
   public GameObject hitPoint;
   public GameObject[] spawns;
   public GameObject[] olds;
   public GameObject Player;
   private void Update()
   {
      if (Player.transform.position.x <= (hitPoint.transform.position.x +0.5f) &&
          Player.transform.position.x >= (hitPoint.transform.position.x -0.5f) && 
          Player.transform.position.y <= (hitPoint.transform.position.y +0.5f) && 
          Player.transform.position.y >= (hitPoint.transform.position.y -0.5f))
      {
         spawns[0].SetActive(true);
         spawns[1].SetActive(true);
         spawns[2].SetActive(true);
         olds[0].SetActive(false);
         olds[1].SetActive(false);
         olds[2].SetActive(false);
      }
     
   }
}
