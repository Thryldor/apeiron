using UnityEngine;

namespace Character.Enemy
{
  public class Enemy : MonoBehaviour
  {
    public bool directionLeft = false;
    public float speed = 0.5f;

    void Start()
    {
      if (directionLeft == true)
        transform.localRotation = Quaternion.Euler(0, 180, 0);
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
}
