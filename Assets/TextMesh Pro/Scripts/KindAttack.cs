using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindAttack : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    float timer;
    

    Player player;
    Kind kind;
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;

        if (timer > speed)
        {
            Fire();
            timer = 0f;
        }
    }
    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Fire()
    {
        if (!GameManager.instance.isLive || !player.scanner.nearestTarget)
            return;
        
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;


        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
        
    }
}
