using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingKey : MonoBehaviour
{
  private float up = 0f;
  private float direction = 0.5f;

  void Update()
  {
    Vector2 p = transform.position;

    p.y += Time.deltaTime * direction;
    up += Time.deltaTime * direction;

    transform.position = p;

    if (up >= 0.4f)
      direction = -0.5f;
    if (up <= 0)
      direction = 0.5f;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
        SceneManager.LoadScene("Menu Win");
  }
}
