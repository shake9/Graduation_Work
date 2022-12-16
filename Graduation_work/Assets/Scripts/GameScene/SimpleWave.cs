using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWave
{
    // ウェーブの番号
    public readonly int waveNum;

    // 敵の出現数
    public readonly int enemyCount;
    // 敵の出現間隔
    public readonly float timeBetweenSpawn = 0.0f;
    // 敵の出現キュー
    private Queue<GameObject> spawnQueue;

    public SimpleWave(int waveNum, int enemyCount, float timeBetweenSpawn, DifficultySetting difficultySetting)
    {
        this.waveNum = waveNum;
        this.enemyCount = enemyCount;
        this.timeBetweenSpawn = timeBetweenSpawn;

        spawnQueue = new Queue<GameObject>();

        // 出現キューを用意する
        for (int i = 0; i < enemyCount; i++)
        {
            spawnQueue.Enqueue(RandomUtility.GetWeightedRandom(difficultySetting.enemySpawnRates).enemyPrefab);
        }
    }

    public GameObject GetNextSpawnPrefab()
    {
        return spawnQueue.Dequeue();
    }

    public bool IsSpawnEnd()
    {
        return spawnQueue.Count == 0;
    }

    public bool IsEnd()
    {
        return spawnQueue.Count == 0 && EnemyController.enemyCount == 0;
    }

    // ゲーム内にいる敵の数+出現待ち中の敵の数
    public int EnemyCount()
    {
        return spawnQueue.Count + EnemyController.enemyCount;
    }
}
