using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public int per;
    public static EnemyBullet instance; // 싱글톤 인스턴스 추가

    Rigidbody2D rigid;
    Collider2D bulletCollider;

    void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rigid.velocity = dir * 3f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || per == -1)
            return;

        per--;

        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 Player 태그를 가지고 있지 않으면 충돌 무시
        if (collision.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, bulletCollider);
            return;
        }

        per--;

        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
