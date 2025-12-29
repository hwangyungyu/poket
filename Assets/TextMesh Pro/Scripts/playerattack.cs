using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;

    public float shootDuration = 1.2f;
    public float shootInterval = 0.3f; // 총알이 발사되는 간격

    public void OnClickbtton()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Skill2);

        StartCoroutine(ShootingCoroutine());

    }

    private IEnumerator ShootingCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shootDuration)
        {
            for (int i = 0; i < 360; i += 45)
            {
                transform.rotation = Quaternion.Euler(0, 0, i);
                Instantiate(bullet, pos.position, transform.rotation);
            }
            elapsedTime += shootInterval;
            yield return new WaitForSeconds(shootInterval);
        }
    }
}