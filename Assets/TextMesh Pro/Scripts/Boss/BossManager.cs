using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public float bossSpawnTime;
    private float gameTime;
    public bool isBossSpawned;
    public GameObject Boss;
    public GameObject BossAlarm;
    public AudioClip bossBGM;

    void Update()
    {
        gameTime = GameManager.instance.gameTime;
        isBossSpawned = gameTime > bossSpawnTime;
        if (isBossSpawned)
        {
            BossSpawn();
            bossSpawnTime = 100000f;
        }
    }

    void BossSpawn()
    {
            BossAlarm.SetActive(true);
            Boss.SetActive(true);
            AudioManager.instance.bgmClip = bossBGM;
            AudioManager.instance.bgmPlayer.clip = bossBGM;
            AudioManager.instance.bgmPlayer.Play();
            StartCoroutine(BossAlarmHide());
    }

    IEnumerator BossAlarmHide()
    {
        yield return new WaitForSecondsRealtime(2f);
        BossAlarm.SetActive(false);
    }
}
