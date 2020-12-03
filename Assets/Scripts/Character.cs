using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class Character : MonoBehaviour
    {
        private float yPosition;
        private float maxX = 8.2f;
        public float moveSpeed;

        public bool isSpeedUpBuff;
        public bool isSizeIncreased;
        public bool isSpeedDown;

        private SpriteRenderer sprite;
        private Animator animator;

        private CharacterState State
        {
            get { return (CharacterState)animator.GetInteger("State"); }
            set { animator.SetInteger("State", (int)value); }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            yPosition = transform.position.y;

        }

        void Update()
        {
            State = CharacterState.Idle;
            if (Input.GetButton("Horizontal"))
            {
                Run();
            }
        }
        public void CharacterMove()
        {
            Vector3 mousePixelPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixelPosition);
            Vector3 padNewposition = new Vector3(mouseWorldPosition.x, yPosition, 0);
            padNewposition.x = Mathf.Clamp(padNewposition.x, -maxX, maxX);
            transform.position = padNewposition;
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
            transform.DOScale(new Vector3 (2,2,0), 2);
            StartCoroutine(NormalSize());
        }

        public void DegreaseSize()
        {
            transform.DOScale(new Vector3(0.5f, 0.5f, 0), 2);
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
            //transform.position.x = Mathf.Clamp(transform.position.x, -maxX, maxX);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);

            sprite.flipX = direction.x < 0.0f;
            State = CharacterState.Run;
        }

        public enum CharacterState
        {
            Idle,
            Run
        }

    }
}

