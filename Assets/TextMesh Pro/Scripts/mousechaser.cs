using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousechasr : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // 마우스 위치를 화면 좌표에서 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z축 값을 0으로 설정하여 2D 평면에서의 위치를 유지

        // 총알이 마우스 위치로 이동
        Vector3 direction = (mousePosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
