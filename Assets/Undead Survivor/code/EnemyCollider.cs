using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCollider2D : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            // 충돌한 객체가 Player 태그를 가지고 있지 않으면 충돌 무시
            if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "skill" || collision.gameObject.tag != "skill1" || collision.gameObject.tag != "bullet")
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<CapsuleCollider2D>());
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // 충돌한 객체가 Player 태그를 가지고 있지 않으면 충돌 무시
            if (other.gameObject.tag != "Player" || other.gameObject.tag != "skill" || other.gameObject.tag != "skill1" || other.gameObject.tag != "bullet")
            {
                Physics2D.IgnoreCollision(other, GetComponent<CapsuleCollider2D>());
            }
        }
    }

