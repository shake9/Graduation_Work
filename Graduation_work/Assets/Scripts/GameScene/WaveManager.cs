using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // ウェーブ数
    [SerializeField] private int waveCount = 3;

    // 現在のウェーブ
    private SimpleWave currentWave = null;

    // 出現範囲(X)
    [SerializeField] private float enemySpawnRangeX = 5.0f;

    // 難易度設定
    [SerializeField] private DifficultySetting difficultySetting = null;

    public void Start()
    {
        StartCoroutine(MainCoroutine());
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    public bool IsGameEnd()
    {
        return currentWave.waveNum == waveCount;
    }

    // メインループ用
    private IEnumerator MainCoroutine()
    {
        Debug.Log("ゲーム開始！");

        // ウェーブ数の分ループ
        for (int i = 0; i < waveCount; i++)
        {
            currentWave = GenerateWave(i + 1);

            // TODO:ここでウェーブ開始演出
            Debug.Log("ウェーブ" + (i + 1) + "開始！");

            // 敵を全て沸かせるまで終了までループ
            while (!currentWave.IsSpawnEnd())
            {
                // 敵を生成
                GameObject enemyInstance = Instantiate(currentWave.GetNextSpawnPrefab());
                // 座標をランダムに設定
                enemyInstance.transform.position = GetRandomPosition();

                // 次の出現まで待機
                yield return new WaitForSeconds(currentWave.timeBetweenSpawn);
            }

            // ウェーブ終了(敵全滅)まで待機
            yield return new WaitUntil(() => { return currentWave.IsEnd(); });

            // TODO:ここでウェーブクリア演出
            Debug.Log("ウェーブ" + (i + 1) + "終了！");

            currentWave = null;
        }

        Debug.Log("ゲーム終了！");
    }

    private SimpleWave GenerateWave(int waveNum)
    {
        return new SimpleWave(waveNum, 10, 1.0f, difficultySetting);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-enemySpawnRangeX, enemySpawnRangeX);
        float y = transform.position.y;
        return new Vector3(x, y, transform.position.z);
    }
}
