using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // 自動で開始するか(デバッグ用)
    [SerializeField] private bool autoStart = false;

    // ウェーブとウェーブの間隔
    [SerializeField] private float intervalBetweenWave = 2.0f;

    // ウェーブの総数
    private int waveCount = 3;

    // 現在のウェーブ
    private SimpleWave currentWave = null;

    // 出現範囲(X)
    [SerializeField] private float spawnRangeX = 5.0f;

    // 出現範囲(Y)
    [SerializeField] private float spawnRangeY = 3.0f;

    // 難易度設定
    [SerializeField] private DifficultySetting localDifficultySetting = null;

    // アナウンス用テキスト
    [SerializeField] private TextAnnouncer announceText = null;

    // 難易度設定(static)
    public static DifficultySetting difficultySetting;

    // クリアしたかどうか
    public bool isClear = false;

    public void Start()
    {
        isClear = false;

        if (difficultySetting != null)
            localDifficultySetting = difficultySetting;
        else
            difficultySetting = localDifficultySetting;

        if (autoStart)
            StartWave(0.0f);
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    /// <param name="delay">遅延実行(画面フェードと合わせる用)</param>
    public void StartWave(float delay = 0.0f)
    {
        waveCount = localDifficultySetting.waves.Count;
        StartCoroutine(MainCoroutine(delay));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    public bool IsGameEnd()
    {
        return currentWave.waveNum == waveCount;
    }

    public int GetCurentWaveNum()
    {
        if (currentWave == null)
            return 0;

        return currentWave.waveNum;
    }

    public int GetMaxWaveNum()
    {
        return waveCount;
    }

    public int GetCurrentEnemyNum()
    {
        return EnemyController.enemyCount;
    }

    public int GetMaxEnemyNum()
    {
        if (currentWave == null)
            return 0;

        return currentWave.maxEnemyCount;
    }

    // メインループ用
    private IEnumerator MainCoroutine(float delay)
    {
        // ゲーム開始の遅延設定分待機
        yield return new WaitForSeconds(delay);

        Debug.Log("ゲーム開始！");

        // ウェーブ数の分ループ
        for (int i = 0; i < waveCount; i++)
        {
            currentWave = GenerateWave(i);

            // TODO:ここでウェーブ開始演出
            announceText.AnnounceText("ウェーブ" + (i + 1) + " 開始！");

            // 敵を全て沸かせるまでループ
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
            announceText.AnnounceText("ウェーブ" + (i + 1) + "終了！");

            // ウェーブ間隔の分待機
            yield return new WaitForSeconds(intervalBetweenWave);

            currentWave = null;
        }
        isClear = true;
        Debug.Log("ゲーム終了！");
    }

    private SimpleWave GenerateWave(int waveNum)
    {
        var wave = localDifficultySetting.waves[waveNum];
        return new SimpleWave(waveNum + 1, wave.enemyCount, localDifficultySetting.timeBetweenSpawn, wave);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-spawnRangeX, spawnRangeX) + transform.position.x;
        float y = Random.Range(-spawnRangeY, spawnRangeY) + transform.position.y;

        return new Vector3(x, y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Color preColor = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRangeX * 2.0f, spawnRangeY * 2.0f, 1.0f));
        Gizmos.color = preColor;
    }
}
