using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public partial class Boss : MonoBehaviour
{
    public enum States
    {
        Idle,

        Skill1,
        Skill2, 
    }

    public float speed;
    public float health;
    public float maxHealth;

    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public Transform targetPlayer;
    public float skillTime = 0;
    public float coolTime = 5f;

    bool isBossLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    public StateMachine stateMachine;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();

        stateMachine = new StateMachine();

        Init_State_Idle();
        Init_State_Skill1();
        Init_State_Skill2();

        stateMachine.Change((int)States.Idle);
    }

    private void FixedUpdate()
    {
        stateMachine.OnUpdate();
    }
 
    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isBossLive)
            return;

        spriter.flipX = target.position.x > rigid.position.x;

        targetPlayer = GameManager.instance.player.GetComponent<Player>().transform;
    }

    void OnEnable()
    {
        target.position = targetPlayer.position;
        isBossLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 5;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data) //DB를 사용가능하게
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isBossLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack");

        if (health > 0)
        {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            isBossLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 5;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;  //1 physical frame delaying
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse);
    }
    
    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
