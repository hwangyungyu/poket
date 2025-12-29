using System.Collections;
using System.Collections.Generic;
//using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;


    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        speed *= Charactor.Speed;
        animator.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }


    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        animator.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x > 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameManager.instance.health -= Time.deltaTime * 10;
        }

        if (GameManager.instance.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 총알과 충돌했을 때 데미지 처리
        if (other.CompareTag("EnemyBullet"))
        {
            GameManager.instance.health -= EnemyBullet.instance.damage; // 데미지 적용
            // 총알이 플레이어를 통과하는 경우 비활성화
        }

        if (GameManager.instance.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
