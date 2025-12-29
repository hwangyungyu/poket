using System.Collections;
using UnityEngine;

public class chaserattack : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    public float shootDuration = 1.2f;
    public float shootInterval = 0.3f; // 총알이 발사되는 간격

    void Update()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    public void OnClickbtton()
    {
        StartCoroutine(ShootingCoroutine());
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Skill7);
    }

    private IEnumerator ShootingCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shootDuration)
        {
            Instantiate(bullet, pos.position, transform.rotation);
            elapsedTime += shootInterval;
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
