using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattacklazer : MonoBehaviour
{
    void Update()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
