using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float timer;

    Player player;
    List<Transform> bulletTransforms = new List<Transform>();


    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;


        switch (id)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 5:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    Fire();
                    timer = 0f;
                }
                break;
            case 6:

                break;
            case 7:


            case 8:

            case 9:
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    Fire();
                    timer = 0f;
                }
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Charactor.Damage;
        this.count += count;

        if (id == 4)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage * Charactor.Damage;
        count = data.baseCount + Charactor.Count;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                break;
            case 4:
                speed = 150 * Charactor.WeaponSpeed;
                Batch();
                break;
            case 5:
                speed = 0.5f * Charactor.WeaponRate;
                break;
            case 6:
            case 7:
            case 8:
            case 9:
                break;
            default:
                speed = 0.5f * Charactor.WeaponRate;
                break;
        }

        // Trigger event to notify other scripts
        EventManager.TriggerItemIdInit(data.itemId);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.3f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);  // -1 is Infinity per
        }
    }

    void Batch1()
    {
        Transform bullet;

        bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.parent = transform;

        // 플레이어의 위치를 기준으로 약간의 오프셋을 추가하여 총알 위치 설정
        Vector3 offset = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0); // 임의의 오프셋
        bullet.position = player.transform.position + offset; // 플레이어 위치 + 오프셋

        bullet.localRotation = Quaternion.identity;
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
        
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

}