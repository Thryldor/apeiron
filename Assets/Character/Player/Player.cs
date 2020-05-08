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
        public Sprite takenBeacon;

        public AudioClip attackSound;
        public AudioClip jumpSound;
        public AudioClip landSound;
        public AudioClip hurtSound;
        public AudioClip deathSound;
        public AudioClip reviveSound;
        public AudioClip gameoverSound;
        public AudioClip[] biteSounds;

        private Slider _lifebar;
        private Slider _soulbar;
        private Image _backUI;
        private CanvasGroup _lifebarCG;
        private CanvasGroup _soulbarCG;
        private CanvasGroup _backUICG;
        private CanvasGroup _pauseUICG;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        private float _horizontalMove;
        private float _speed = 40F;
        private bool _jumping;
        private WalkDirection _walk = WalkDirection.Right;
        private AudioSource _audioSource;
        private AudioSource _globalAudioSource;
        private bool _isGameover = false;
        private bool _isWalking = false;
        private bool _isInAir = true;
        public bool vie = true;
        public Vector2? lastCheckpoint = null;
        private bool _inPause = false;

        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int TakeOff = Animator.StringToHash("TakeOff");
        private static readonly int InTheAir = Animator.StringToHash("InTheAir");
        private static readonly int Fall = Animator.StringToHash("Fall");


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioSource = transform.Find("Player Vie").GetComponent<AudioSource>();
            _globalAudioSource = GetComponent<AudioSource>();
            _lifebar = GameObject.Find("Lifebar").GetComponent<Slider>();
            _soulbar = GameObject.Find("Soulbar").GetComponent<Slider>();
            _backUI = GameObject.Find("BackgroundUI").GetComponent<Image>();
            _lifebarCG = _lifebar.GetComponent<CanvasGroup>();
            _soulbarCG = _soulbar.GetComponent<CanvasGroup>();
            _backUICG = _backUI.GetComponent<CanvasGroup>();
            _pauseUICG = GameObject.Find("menuPause").GetComponent<CanvasGroup>();
            _audioSource.volume = PlayerPrefs.GetFloat("soundVol",1f);
            _globalAudioSource.volume = PlayerPrefs.GetFloat("soundVol",1f);
        }

        // Update is called once per frame
        private void Update()
        {
            _horizontalMove = _direction.x * _speed;
            if (_jumping)
              animator.SetBool(Jumping, true);
            if (_isInAir)
              animator.SetBool(InTheAir, true);
            if (!_jumping && _horizontalMove != 0 && !_isWalking) {
              _globalAudioSource.Play();
              _isWalking = true;
            } else if (_isWalking && (_horizontalMove == 0 || _jumping)) {
              _globalAudioSource.Stop();
              _isWalking = false;
            }
            if (_rigidbody2D.velocity.y < 0)
              _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
        }

        private void FixedUpdate()
        {
            UpdateMove();

            if (vie && _soulbar.value < 1f)
            {
              _soulbar.value += 0.0002f;
              _lifebarCG.alpha = 1f;
              _backUICG.alpha = 1f;
              if (_soulbar.value >= 1f)
                _soulbarCG.alpha = 0f;
            }
            if (!vie && _soulbar.value > 0)
            {
              _lifebarCG.alpha = 0f;
              _backUICG.alpha = 0f;
              _soulbar.value -= 0.0004f;
              _soulbarCG.alpha = 1f;
            }
            if (_soulbar.value <= 0 && _isGameover == false) {
              _isGameover = true;
              _globalAudioSource.PlayOneShot(gameoverSound, 1.0F);
              StartCoroutine(GameOverScene(gameoverSound));
            }
        }

        private void UpdateMove()
        {
            Vector3 targetVelocity = new Vector2(_horizontalMove * Time.fixedDeltaTime * 10f, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = targetVelocity;
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (!_inPause)
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
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (!_inPause)
            {
                if (vie)
                {
                    if (_jumping) return;
                    //_rigidbody2D.AddForce(new Vector2(0f, 400f));
                    _rigidbody2D.velocity = Vector2.up * 8f;
                    animator.SetTrigger(TakeOff);
                    _jumping = true;
                    _audioSource.PlayOneShot(jumpSound, 1.0F);
                }
                else
                {
                    _rigidbody2D.AddForce(new Vector2(0f, 150f));
                }
            }
           // _jumping = true;

        }

        public void OnPause(InputAction.CallbackContext ctx)
        {
            if (!_inPause)
            {
                Time.timeScale = 0f;
                _inPause = true;
                _pauseUICG.alpha = 1;
            }
            else
            {
                _pauseUICG.alpha = 0;
                Time.timeScale = 1f;
                _inPause = false;
            }
        }

        private IEnumerator GameOverScene(AudioClip sound)
        {
          yield return new WaitForSeconds(sound.length);
          SceneManager.LoadScene("Menu GameOver");
        }

        private IEnumerator PlaySound(AudioClip sound)
        {
          yield return new WaitForSeconds(0.5f);
          _audioSource.PlayOneShot(sound, 1F);
        }

        private IEnumerator KillEnemy(GameObject enemy)
        {
          yield return new WaitForSeconds(0.5f);
          if (enemy != null)
          {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (distance <= 3f)
            {
              Character.Enemy.Enemy script = enemy.GetComponent<Character.Enemy.Enemy>();

              script.die();
              playHurtSound();
              //Destroy(enemy);
            }
          }
        }

        private IEnumerator KillFlyingEnemy(GameObject enemy)
        {
          yield return new WaitForSeconds(0.5f);
          if (enemy != null)
          {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (distance <= 3f)
            {
              Character.Enemy.FlyingEnemy script = enemy.GetComponent<Character.Enemy.FlyingEnemy>();

              script.die();
              playHurtSound();
              //Destroy(enemy);
            }
          }
        }



        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (!_inPause)
            {
            animator.SetTrigger(Attack);
            StartCoroutine(PlaySound(attackSound));

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] flyingEnemies = GameObject.FindGameObjectsWithTag("FlyingEnemy");

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

                foreach(GameObject enemy in flyingEnemies)
                {
                  float distance = Vector2.Distance(enemy.transform.position, transform.position);
                  if (distance <= 3f)
                  {
                    if (_walk == WalkDirection.Right)
                    {
                      if (enemy.transform.position.x >= transform.position.x)
                        StartCoroutine(KillFlyingEnemy(enemy));
                    }
                    else
                    {
                      if (enemy.transform.position.x <= transform.position.x)
                        StartCoroutine(KillFlyingEnemy(enemy));
                    }
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

        private void OnCollisionExit2D(Collision2D other)
        {
          if (other.collider.CompareTag("Ground"))
          {
            _isInAir = true;
            if (!_jumping)
              animator.SetTrigger(Fall);
          }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Vector2 normal = other.contacts[0].normal;

            if (other.collider.CompareTag("Ground") && Convert.ToInt32(normal.y) == 1)
            {
              if (_jumping == true)
                _audioSource.PlayOneShot(landSound, 1.0F);
              _isInAir = false;
              _jumping = false;
              animator.SetBool(InTheAir, false);
              animator.SetBool(Jumping, false);
            }

            if (other.gameObject.tag == "Spike")
            {
                isDead();
            }

            if (other.gameObject.tag == "Enemy")
            {
              if (other.gameObject.GetComponent<Character.Enemy.Enemy>().isAlive)
                isDead();
            }

            if (other.gameObject.tag == "FlyingEnemy")
            {
              Debug.Log("test");
              isDead();
            }

            if (other.gameObject.tag == "Bomb")
            {
              playHurtSound();
              _lifebar.value -= 0.5f;
              if (_lifebar.value <= 0f)
                isDead();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
          if (other.gameObject.tag == "Beacon")
          {
            other.gameObject.GetComponent<SpriteRenderer>().sprite = takenBeacon;
            lastCheckpoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
          }
        }

        public void playHurtSound()
        {
          _audioSource.PlayOneShot(hurtSound, 1.0F);
        }

        public void isDead()
        {
          _globalAudioSource.PlayOneShot(deathSound, 2.0F);
          vie = false;
        }

        public void playReviveSound()
        {
          _globalAudioSource.PlayOneShot(reviveSound, 2.0F);
        }

        public void playBiteSound()
        {
          System.Random random = new System.Random();
          int randomIdx = random.Next(0, biteSounds.Length - 1);

          _audioSource.PlayOneShot(biteSounds[randomIdx], 1.0F);
        }
    }
}
