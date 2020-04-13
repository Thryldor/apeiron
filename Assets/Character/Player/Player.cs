using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Player
{
    enum WalkDirection
    {
        Right,
        Left,
    }
    public class Player : MonoBehaviour
    {
        public Animator animator;
        public GameObject groundCheck;

        private Slider _lifebar;
        private Slider _soulbar;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        private float _horizontalMove;
        private float _speed = 40F;
        private bool _jumping;
        private WalkDirection _walk = WalkDirection.Right;
        public bool vie = true;

        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int TakeOff = Animator.StringToHash("TakeOff");


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _lifebar = GameObject.Find("Lifebar").GetComponent<Slider>();
            _soulbar = GameObject.Find("Soulbar").GetComponent<Slider>();
        }

        // Update is called once per frame
        private void Update()
        {
            _horizontalMove = _direction.x * _speed;
            if (_jumping)
              animator.SetBool(Jumping, true);
            if (_rigidbody2D.velocity.y < 0)
              _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
        }

        private void FixedUpdate()
        {
            UpdateMove();

            if (vie && _soulbar.value < 1f)
              _soulbar.value += 0.0002f;
            if (!vie && _soulbar.value > 0)
              _soulbar.value -= 0.0004f;
            if (_soulbar.value <= 0)
              SceneManager.LoadScene("Menu GameOver");
        }

        private void UpdateMove()
        {
            Vector3 targetVelocity = new Vector2(_horizontalMove * Time.fixedDeltaTime * 10f, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = targetVelocity;
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                _direction = ctx.ReadValue<Vector2>();
                if (_walk == WalkDirection.Right && _direction.x < 0 ||
                    _walk == WalkDirection.Left && _direction.x > 0)
                {
                    Flip();
                }

                animator.SetFloat(Speed, 40.0f);
                _speed = 40.0f;
            }
            if (!ctx.canceled) return;
            animator.SetFloat(Speed, 0f);
            _speed = 0f;
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (vie)
            {
                if (_jumping) return;
                //_rigidbody2D.AddForce(new Vector2(0f, 400f));
                _rigidbody2D.velocity = Vector2.up * 8f;
                animator.SetTrigger(TakeOff);
                _jumping = true;
            }
            else
            {
                _rigidbody2D.AddForce(new Vector2(0f, 150f));
            }

           // _jumping = true;

        }

        private IEnumerator KillEnemy(GameObject enemy)
        {
          yield return new WaitForSeconds(0.5f);
          if (enemy != null)
          {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (distance <= 3f)
            {
              Destroy(enemy);
            }
          }
        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            animator.SetTrigger(Attack);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(GameObject enemy in enemies)
            {
              float distance = Vector2.Distance(enemy.transform.position, transform.position);
              if (distance <= 3f)
              {
                if (_walk == WalkDirection.Right)
                {
                  if (enemy.transform.position.x >= transform.position.x)
                    StartCoroutine(KillEnemy(enemy));
                }
                else
                {
                  if (enemy.transform.position.x <= transform.position.x)
                    StartCoroutine(KillEnemy(enemy));
                }
              }
            }
        }

        private void Flip()
        {
            _walk = _walk == WalkDirection.Right ? WalkDirection.Left : WalkDirection.Right;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Spike")
            {
                vie = false;
            }

            if (other.collider.CompareTag("Ground"))
            {
                _jumping = false;
                animator.SetBool(Jumping, false);
            }

            if (other.gameObject.tag == "Enemy")
            {
              vie = false;
            }
        }
    }
}
