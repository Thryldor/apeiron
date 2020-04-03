using System;
using UnityEngine;
using UnityEngine.InputSystem;

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


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            _horizontalMove = _direction.x * _speed;
        }

        private void FixedUpdate()
        {
            UpdateMove();
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
                _rigidbody2D.AddForce(new Vector2(0f, 300f));
                animator.SetBool(Jumping, true);
                _jumping = true;
               
            }
            else
            {
                _rigidbody2D.AddForce(new Vector2(0f, 150f));
               // animator.SetBool(Jumping, true);
            }
           
           // _jumping = true;
           
        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            animator.SetTrigger(Attack);
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
            if (other.collider.CompareTag("Ground"))
            {
                _jumping = false;
            }
        }
    }
}
