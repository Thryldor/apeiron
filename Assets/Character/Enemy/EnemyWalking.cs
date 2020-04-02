using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
  bool directionLeft = false;
  public float speed = 0.5f;

  void Start()
  {

  }

  void Update()
  {
    Vector3 p = transform.position;

    if (directionLeft)
      p.x -= speed * Time.deltaTime;
    else
      p.x += speed * Time.deltaTime;
    transform.position = p;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Wall")
    {
      directionLeft = !directionLeft;
      if (directionLeft)
        transform.localRotation = Quaternion.Euler(0, 180, 0);
      else
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
  }
}
