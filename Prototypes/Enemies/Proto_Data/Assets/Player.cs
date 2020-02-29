using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float speed = 5.0f;
  public float jumpForce = 7.0f;
  public bool isGrounded = false;

  void Start() {
    gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

    PlayerPrefs.DeleteAll ();
    Screen.SetResolution (1280, 720, false);
  }

  void Update() {
    Jump();
    var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    transform.position += move * speed * Time.deltaTime;
 }

 void Jump() {
   if(Input.GetButtonDown("Jump") && isGrounded) {
     gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
   }
 }

 void OnCollisionEnter(Collision collision) {
   if(gameObject.CompareTag("Block")) {  // or if(gameObject.CompareTag("YourWallTag")){
     gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
   }
 }
}
