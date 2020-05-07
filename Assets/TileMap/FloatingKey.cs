using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloatingKey : MonoBehaviour
{
  private float up = 0f;
  private float direction = 0.5f;
    private int newNbKeys;
   private Text keytext;
        private void Start()
    {
        keytext = GameObject.FindGameObjectWithTag("KeyNumber").GetComponent<Text>();
        
    }
    void Update()
  {
        newNbKeys = int.Parse(GameObject.FindGameObjectWithTag("KeyNumber").GetComponent<Text>().text);
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
        {
            newNbKeys += 1;
            if(newNbKeys >= 2)
            {
                SceneManager.LoadScene("Menu Win");
            }
                keytext.text = ""+newNbKeys;
                Destroy(this.gameObject);
            
           
        }
           
        

  }
}
