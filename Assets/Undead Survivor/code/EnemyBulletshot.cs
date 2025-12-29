using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyBulletshot : MonoBehaviour
{
    public float speed;

    public float distance;
    public LayerMask isLayer;
    void Start()
    {
        Invoke("DestoryBullet", 1);
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.tag == "Player")
            {
                Debug.Log("Hit");
            }
            DestoryBullet();
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

    void DestoryBullet()
    {
        gameObject.SetActive(false);
    }
}