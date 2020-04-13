using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Character.Enemy
{
  public class Enemy : MonoBehaviour
  {
    public bool directionLeft = false;
    public float speed = 0.5f;

    private Animator animator;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private GameObject player;
    private Player.Player script;
    private Slider lifebar;
    private float attackCooldown = 0f;

    void Start()
    {
      animator = GetComponent<Animator>();
      lifebar = GameObject.Find("Lifebar").GetComponent<Slider>();
      if (directionLeft == true)
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private IEnumerator AttackPlayer()
    {
      yield return new WaitForSeconds(1f);
      if (Math.Abs(player.transform.position.x - transform.position.x) < 3.5f &&
          Math.Abs(player.transform.position.y - transform.position.y) < 0.5f)
      {
        lifebar.value -= 0.5f;
        if (lifebar.value <= 0f)
          script.vie = false;
      }
    }

    void Update()
    {
      Vector3 p = transform.position;

      if (directionLeft)
        p.x -= speed * Time.deltaTime;
      else
        p.x += speed * Time.deltaTime;

      if (attackCooldown >= 4f)
      {
        if (directionLeft)
          p.x -= speed * 2 * Time.deltaTime;
        else
          p.x += speed * 2 * Time.deltaTime;
      }

      transform.position = p;

      if (attackCooldown > 0)
        attackCooldown -= Time.deltaTime;

      if (player == null && GameObject.FindGameObjectsWithTag("Player").Length > 0)
      {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        script = player.GetComponent<Player.Player>();
      }
      if (player != null && attackCooldown <= 0f)
      {
        if (Math.Abs(player.transform.position.x - transform.position.x) < 3.5f &&
            Math.Abs(player.transform.position.y - transform.position.y) < 0.5f)
        {
          if (directionLeft && player.transform.position.x <= transform.position.x)
          {
            attackCooldown = 5f;
            animator.SetTrigger(Attack);
            StartCoroutine(AttackPlayer());
          }
          else if (!directionLeft && player.transform.position.x >= transform.position.x)
          {
            attackCooldown = 5f;
            animator.SetTrigger(Attack);
            StartCoroutine(AttackPlayer());
          }
        }
      }
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
