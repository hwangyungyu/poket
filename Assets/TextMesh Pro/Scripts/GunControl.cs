using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public GameObject bullet;
    public Transform sPoint;

    void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButtonDown(0))
        {
            
                Instantiate(bullet, sPoint.position, Quaternion.AngleAxis(angle-90, Vector3.forward));

            
        }
    }
}
