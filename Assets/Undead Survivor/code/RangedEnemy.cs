using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; // EnemyBullet 프리팹
    public Transform firePoint; // 총알 발사 위치
    public float fireRate = 5f; // 발사 간격
    public float detectionRange = 5f; // 플레이어 감지 거리
    public Scanner scanner; // Scanner 컴포넌트
    private Transform player; // 플레이어의 Transform

    private void Start()
    {
        scanner = GetComponent<Scanner>(); // Scanner 컴포넌트 가져오기
        StartCoroutine(ShootAtPlayer());
    }

    private IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            // Scanner에서 가장 가까운 타겟을 가져옴
            player = scanner.nearestTarget;

            // 플레이어가 감지되었고, 거리 내에 있는 경우
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer <= detectionRange)
                {
                    Shoot();
                    yield return new WaitForSeconds(fireRate);
                }
            }

            yield return null; // 1초마다 체크
        }
    }

    private void Shoot()
    {
        StartCoroutine(ShootCoroutine());

    }

    private IEnumerator ShootCoroutine()
    {
        // 총알 프리팹을 인스턴스화
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 플레이어 방향 계산
        Vector3 direction = (player.position - firePoint.position).normalized;

        // EnemyBullet 스크립트 초기화
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.Init(10f, 5, direction); // 데미지, 퍼, 방향 설정

        yield return new WaitForSeconds(fireRate); // 발사 간격
    }
}


