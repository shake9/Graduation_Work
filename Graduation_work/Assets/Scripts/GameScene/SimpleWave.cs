using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWave
{
    // �E�F�[�u�̔ԍ�
    public readonly int waveNum;

    // �G�̏o����
    public readonly int enemyCount;
    // �G�̏o���Ԋu
    public readonly float timeBetweenSpawn = 0.0f;
    // �G�̏o���L���[
    private Queue<GameObject> spawnQueue;

    public SimpleWave(int waveNum, int enemyCount, float timeBetweenSpawn, DifficultySetting difficultySetting)
    {
        this.waveNum = waveNum;
        this.enemyCount = enemyCount;
        this.timeBetweenSpawn = timeBetweenSpawn;

        spawnQueue = new Queue<GameObject>();

        // �o���L���[��p�ӂ���
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

    // �Q�[�����ɂ���G�̐�+�o���҂����̓G�̐�
    public int EnemyCount()
    {
        return spawnQueue.Count + EnemyController.enemyCount;
    }
}
