using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;
    bool isSlowed;  // 적이 느려졌는지 여부

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    private Coroutine hpAttackCoroutine;
    private Coroutine slowCoroutine;
    private float skillHitDelay = 0.7f; // 스킬 데미지가 들어가는 딜레이
    private float skillDuration = 1f; // 스킬 데미지가 들어가는 지속 시간
    private float slowDuration = 1f; // 속도가 느려지는 지속 시간
    private float slowSpeedFactor = 0f; // 속도가 느려지는 비율 (50% 감소)

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
        {
            return;
        }

        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        //isSlowed = false;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLive)
            return;
        if (collision.gameObject.tag == "map")
        {
            Physics2D.IgnoreCollision(collision, GetComponent<CapsuleCollider2D>());
        }

        if (collision.CompareTag("Bullet"))
        {
            health -= collision.GetComponent<Bullet>().damage;

            StartCoroutine(KnockBack());

            if (health > 0)
            {
                anim.SetTrigger("Hit");
            }
            else
            {
                Die();
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"입장 {collision.name}");
        if (!isLive)
            return;

        if (collision.CompareTag("skill"))
        {
            float hit_damage = collision.GetComponent<skill>().damage;
            Debug.Log("타격");
            if (hpAttackCoroutine == null)
            {
                hpAttackCoroutine = StartCoroutine(HpAttack(hit_damage));
            }
        }
        else if (collision.CompareTag("skill1") && !isSlowed)
        {
            if (slowCoroutine != null)
            {
                Debug.Log("느려지나");
                StopCoroutine(slowCoroutine);
            }
            slowCoroutine = StartCoroutine(Slow(slowDuration));
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 Player 태그를 가지고 있지 않으면 충돌 무시
        if (collision.gameObject.tag == "map")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CapsuleCollider2D>());
        }
    }

    IEnumerator HpAttack(float hit_damage)
    {
        float elapsed = 0f;

        while (isLive && elapsed < skillDuration)
        {
            health -= hit_damage;

            if (health > 0)
            {
                anim.SetTrigger("Hit");
            }
            else
            {
                Die();
                yield break; // 적이 죽으면 코루틴 종료
            }

            yield return new WaitForSeconds(skillHitDelay); // 1초마다 데미지 적용
            elapsed += skillHitDelay;
        }

        hpAttackCoroutine = null;
    }

    IEnumerator Slow(float duration)
    {
        isSlowed = true;
        float originalSpeed = speed;
        speed *= slowSpeedFactor;

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
        isSlowed = false;
        slowCoroutine = null;
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Die()
    {
        isLive = false;
        coll.enabled = false;
        rigid.simulated = false;
        spriter.sortingOrder = 1;
        anim.SetBool("Dead", true);
        GameManager.instance.kill++;
        GameManager.instance.GetExp();

        if (hpAttackCoroutine != null)
        {
            StopCoroutine(hpAttackCoroutine);
            hpAttackCoroutine = null;
        }

        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            slowCoroutine = null;
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
