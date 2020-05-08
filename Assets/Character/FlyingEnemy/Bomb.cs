using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
  public class Bomb : MonoBehaviour
  {
    private void OnCollisionEnter2D(Collision2D other)
    {
      Destroy(gameObject);
    }
  }
}
