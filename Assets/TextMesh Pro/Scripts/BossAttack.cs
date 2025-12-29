using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharge : MonoBehaviour
{
    public float chargeSpeed = 5f;      // 돌진 속도
    public float chargeDuration = 2f;   // 돌진 시간
    public float cooldownDuration = 3f; // 돌진 후 쿨다운 시간
    public bool isLive;

    private Transform player;           // 플레이어의 위치
    private Rigidbody2D rigid;             // 보스의 Rigidbody2D
    private bool isCharging = false;    // 돌진 중인지 여부
    private float chargeTime = 0f;      // 현재 돌진 시간
    private float cooldownTime = 0f;    // 현재 쿨다운 시간


    void Start()
    {
        if (player != null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        isLive = true;
    }

    void Update()
    {
        if (!isLive)
            return;

        if (isCharging)
        {
            // 돌진 중일 때
            chargeTime += Time.deltaTime;
            if (chargeTime >= chargeDuration)
            {
                isCharging = false;
                cooldownTime = 0f;
            }
        }
        else
        {
            // 쿨다운 중일 때
            cooldownTime += Time.deltaTime;
            if (cooldownTime >= cooldownDuration)
            {
                StartCharge();
            }
        }
    }

    void FixedUpdate()
    {
        if (player != null && transform != null)
        {
            if (isCharging)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rigid.velocity = direction * chargeSpeed;
            }
            else
            {
                rigid.velocity = Vector2.zero;
            }
        }
    }

    void StartCharge()
    {
        isCharging = true;
        chargeTime = 0f;
    }
}