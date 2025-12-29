using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kind : MonoBehaviour
{
    public float speed;
    public int prefabId;
    public Rigidbody2D target;
    public int count;
    public float damage;
    float timer;
    public Scanner scanner;


    Weapon weapon;
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    Player player;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        weapon = GetComponent<Weapon>();
        wait = new WaitForFixedUpdate();
        speed = 2.5f;
    }

    public float stopDistance = 1f; // Distance to the player to stop
    public float stopDuration = 2f; // Duration to stop
    private float stopTimer = 0f;

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (stopTimer > 0)
        {
            stopTimer -= Time.fixedDeltaTime;
            return; // Stop moving if stopTimer is active
        }

        Vector2 dirVec = target.position - rigid.position;
        if (dirVec.magnitude < stopDistance)
        {
            stopTimer = stopDuration; // Start the stop timer
            return;
        }

        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        Vector2 toVec = rigid.position + nextVec;
        if (!(toVec - nextVec == Vector2.one))
            rigid.MovePosition(toVec);
        rigid.velocity = Vector2.zero;
    }



    void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;
        spriter.flipX = target.position.x > rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
    }
}
