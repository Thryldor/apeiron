using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
  public class FlyingEnemy : MonoBehaviour
  {
    public float speed = 10.0f;
    public float fallingSpeed = 0.5f;
    public GameObject projectile;

    private float timer = 0f;
    private GameObject player;
    private Vector2? target = null;
    private bool isLeft;

    private Animator animator;
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Die = Animator.StringToHash("Die");

    void Start()
    {
      animator = GetComponent<Animator>();
    }

    private IEnumerator setNewTarget(Vector2 newTarget)
    {
      yield return new WaitForSeconds(0.7f);
      target = newTarget;
    }

    void Update()
    {
      timer += Time.deltaTime;

      Vector2 pos = transform.position;

      if (player == null && GameObject.FindGameObjectsWithTag("Player").Length > 0)
        player = GameObject.FindGameObjectsWithTag("Player")[0];

      if (timer > 8f && player != null)
      {
        //transform.LookAt(player.transform);
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y - 2f, 0);
        Vector2 direction = (targetPos - transform.position).normalized * 5f;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);
      }

      if ((timer > 8f && player && pos.y - player.transform.position.y < 10) ||
          (player && pos.y <= player.transform.position.y))
      {
        if (player != null && transform.position.x < player.transform.position.x)
        {
          //Droite
          isLeft = false;
          animator.SetTrigger(Right);
          StartCoroutine(setNewTarget(new Vector2(transform.position.x + 1, transform.position.y + 3)));
        }
        else if (player != null)
        {
          //Gauche
          isLeft = true;
          animator.SetTrigger(Left);
          StartCoroutine(setNewTarget(new Vector2(transform.position.x - 1, transform.position.y + 3)));
        }
        timer = 0;
      }
      if (target != null)
      {
        pos.y += Time.deltaTime * speed;
        if (isLeft)
        {
          pos.x -= Time.deltaTime * speed / 3;
          if (pos.x <= ((Vector2) target).x && pos.y >= ((Vector2) target).y)
            target = null;
        }
        else
        {
          pos.x += Time.deltaTime * speed / 3;
          if (pos.x >= ((Vector2) target).x && pos.y >= ((Vector2) target).y)
            target = null;
        }
        transform.position = pos;
      }
      else
      {
        pos.y -= Time.deltaTime * fallingSpeed;
        transform.position = pos;
      }
    }

    public void die()
    {
      animator.SetTrigger(Die);
      Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
  }
}
