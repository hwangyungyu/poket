using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    SpriteRenderer spriter;
    public Transform target; // Rigidbody2D 대신 Transform 사용

    private void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        parentPos = new Queue<Vector3>();
    }

    private void Update()
    {
        Watch();
        Follow();
    }

    private void LateUpdate()
    {
        target = GameManager.instance.player.transform; // Rigidbody2D 대신 Transform 사용

        spriter.flipX = target.position.x > transform.position.x;
    }

    void Watch()
    {
        // FIFO #입력 위치
        if (!parentPos.Contains(parent.position))
        {
            parentPos.Enqueue(parent.position);
        }

        // #출력 위치
        if (parentPos.Count > followDelay)
        {
            followPos = parentPos.Dequeue();
        }
        else if (parentPos.Count < followDelay)
        {
            followPos = parent.position;
        }
    }

    private void Follow()
    {
        transform.position = followPos;
    }
}
