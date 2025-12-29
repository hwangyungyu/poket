using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;

    public float cooltime;
    public int prefabId;
    public int damage;
    public int count;
    public int maxChainTargets = 5; // Maximum number of targets for the chain lightning

    Player player;
    List<Transform> targetList = new List<Transform>();

    private void Awake()
    {
        player = GameManager.instance.player;
    }

    void SkillLightning()
    {
        // Clear the target list before finding new targets
        targetList.Clear();

        // Find the nearest target to start the chain lightning
        Transform currentTarget = player.scanner.nearestTarget;
        if (!currentTarget)
            return;

        targetList.Add(currentTarget);

        // Find additional targets for chaining
        for (int i = 1; i < maxChainTargets; i++)
        {
            currentTarget = FindNextTarget(currentTarget);
            if (currentTarget == null)
                break;
            targetList.Add(currentTarget);
        }

        // Fire bullets at each target in the chain
        for (int index = 0; index < targetList.Count; index++)
        {
            Vector3 targetPos = targetList[index].position;
            Vector3 nextPos = (index < targetList.Count - 1) ? targetList[index + 1].position : targetPos;
            Vector3 direction = nextPos - targetPos;

            Transform bulletTransform = GameManager.instance.pool.Get(prefabId).transform;
            bulletTransform.position = targetPos;
            bulletTransform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            bulletTransform.GetComponent<Bullet>().Init(damage, count, direction);
        }
    }

    // Method to find the next target in the chain
    Transform FindNextTarget(Transform currentTarget)
    {
        Collider[] colliders = Physics.OverlapSphere(currentTarget.position, 10f); // Adjust the radius as needed
        Transform nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if (collider.transform == currentTarget || targetList.Contains(collider.transform))
                continue;

            float distance = Vector3.Distance(currentTarget.position, collider.transform.position);
            if (distance < nearestDistance)
            {
                nearestTarget = collider.transform;
                nearestDistance = distance;
            }
        }

        return nearestTarget;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SkillLightning();
    }
}