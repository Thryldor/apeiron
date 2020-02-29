using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
  GameObject Player;

  void Start() {
    Player = gameObject.transform.parent.gameObject;
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if(collision.collider.tag == "Block") {
      Player.GetComponent<Player>().isGrounded = true;
    }
  }

  private void OnCollisionExit2D(Collision2D collision) {
    if(collision.collider.tag == "Block") {
      Player.GetComponent<Player>().isGrounded = false;
    }
  }
}
