using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class Character : MonoBehaviour
    {
        public float moveSpeed;

        [SerializeField]
        private float jumpForce = 15.0F;

        [HideInInspector]
        public bool isSpeedUpBuff;
        [HideInInspector]
        public bool isSizeIncreased;
        [HideInInspector]
        public bool isSpeedDown;
        [HideInInspector]
        public bool isGrounded = false;

        new private Rigidbody2D rigidbody;
        private SpriteRenderer sprite;
        private Animator animator;
        public AudioClip jumpSound;
        [HideInInspector]
        public Vector3 defaultPosition;

        public CharacterState State
        {
            get { return (CharacterState)animator.GetInteger("State"); }
            set { animator.SetInteger("State", (int)value); }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Start()
        {
            defaultPosition = transform.position;

        }

        void Update()
        {
            if (isGrounded)
            {
                State = CharacterState.Idle;
            }

            if (Input.GetButton("Horizontal"))
            {
                Run();
            }
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        private IEnumerator Restart()
        {
            yield return new WaitForSeconds(1);
            transform.position = defaultPosition;
        }

        public void RestartPosition()
        {
            StartCoroutine(Restart());
        }

        public void SpeedUp()
        {
            if (!isSpeedUpBuff)
            {
                moveSpeed *= 2;
                isSpeedUpBuff = true;
                StartCoroutine(StopSpeedUp());
            }
        }

        private IEnumerator StopSpeedUp()
        {
            yield return new WaitForSeconds(10);
            StopSpeed();
        }

        private void StopSpeed()
        {
            if (isSpeedUpBuff)
            {
                moveSpeed /= 2;
                isSpeedUpBuff = false;
            }
        }

        public void SpeedDown()
        {
            if (!isSpeedDown)
            {
                moveSpeed /= 2;
                isSpeedDown = true;
                StartCoroutine(StopSpeedDown());
            }
        }

        private IEnumerator StopSpeedDown()
        {
            yield return new WaitForSeconds(10);
            SpeedDownStop();
        }

        private void SpeedDownStop()
        {
            if (isSpeedDown)
            {
                moveSpeed *= 2;
                isSpeedDown = false;
            }
        }

        public void IncreaseSize()
        {
            transform.DOScale(new Vector3(2, 2, 0), 2);
            NormalSize();
        }

        public void DegreaseSize()
        {
            transform.DOScale(new Vector3(0.5f, 0.5f, 0), 2);
            NormalSize();
        }

        public void NozmalSize()
        {
            StartCoroutine(NormalSize());
        }

        private IEnumerator NormalSize()
        {
            yield return new WaitForSeconds(10);
            transform.DOScale(new Vector3(1, 1, 0), 2);
        }

        private void Run()
        {
            Vector3 direction = transform.right * Input.GetAxis("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);

            sprite.flipX = direction.x < 0.0f;
            if (isGrounded)
            {
                State = CharacterState.Run;
            }
        }

        private void Jump()
        {
            State = CharacterState.Jump;
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySound(jumpSound);
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            isGrounded = colliders.Length > 1;

            if (!isGrounded)
            {
                State = CharacterState.Jump;
            }
        }
    }

    public enum CharacterState
    {
        Idle,
        Run,
        Jump
    }
}

