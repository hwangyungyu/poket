using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public SpawnData[] eliteSpawnData; // 엘리트 몬스터 데이터 추가
    public GameObject rangedEnemyPrefab; // 원거리 적 프리펩 추가


    float timer;
    float eliteTimer; // 엘리트 몬스터 타이머 추가
    int level;
    int eliteLevel;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        eliteTimer += Time.deltaTime; // 엘리트 몬스터 타이머 증가

        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);
        eliteLevel = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 60f), eliteSpawnData.Length - 1); // 엘리트 레벨 결정

        if (timer > spawnData[level].spawnTime)
        {
            Spawn();
            timer = 0;
        }

        if (eliteTimer > 60f) // 1분마다 엘리트 몬스터 생성
        {
            EliteSpawn();
            eliteTimer = 0;
        }
    }

    void Spawn()
    {

        GameObject enemy;

        if (spawnData[level].isRanged)
        {
            enemy = Instantiate(rangedEnemyPrefab); // 원거리 적 생성
        }
        else
        {
            enemy = GameManager.instance.pool.Get(0); // 일반 적 생성
        }

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        enemy.transform.localScale = new Vector3(spawnData[level].scale.x, spawnData[level].scale.y, 1); // 몬스터 크기 설정

    }

    void EliteSpawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(eliteSpawnData[eliteLevel]);
        enemy.transform.localScale = new Vector3(eliteSpawnData[eliteLevel].scale.x, eliteSpawnData[eliteLevel].scale.y, 1); // 엘리트 몬스터 크기 설정
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public Vector2 scale; // 크기 설정을 위한 변수 추가
    public bool isRanged; // 원거리 적 여부를 구분하는 변수 추가

}
