using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private float textEffectTime = 0.25f;
    [SerializeField] private float scoreLogRefreshTime = 5.0f;
    private float scoreLogRefreshElapsedTime = 0.0f;

    [SerializeField] private GameObject scoreTextParent;

    [SerializeField] private GameObject normalTextPrefab;
    [SerializeField] private GameObject specialTextPrefab;

    // 一度に表示されるスコア表示ログの最大数
    [SerializeField] private int maxLogNum = 5;

    private Queue<GameObject> scoreTextQueue = new Queue<GameObject>();

    private void Start()
    {
        ScoreManager.Instance.onScoreAdd.AddListener(Display);
    }

    private void Update()
    {
        scoreLogRefreshElapsedTime += Time.deltaTime;

        if (scoreLogRefreshElapsedTime >= scoreLogRefreshTime)
        {
            int loopCount = 0;

            // スコアのログを消す
            while (scoreTextQueue.Count > 0)
            {
                float fadeTime = 0.25f * ++loopCount;

                var text = scoreTextQueue.Dequeue().GetComponent<Text>();
                text.CrossFadeAlpha(0.0f, fadeTime, true);
                Destroy(text.gameObject, fadeTime);
            }
        }
    }

    private void Display(int value, ScoreManager.ScoreType scoreType)
    {
        // スコア取得ログ用オブジェクト生成
        GameObject scoreText = null;

        // Text
        Text text = null;

        // テキスト生成
        switch (scoreType)
        {
            case ScoreManager.ScoreType.Normal:
                scoreText = Instantiate(normalTextPrefab, scoreTextParent.transform);
                // Textを取得
                text = scoreText.GetComponent<Text>();
                // テキスト設定
                text.text = "＋" + value + " KILL";
                break;
            case ScoreManager.ScoreType.Special:
                scoreText = Instantiate(specialTextPrefab, scoreTextParent.transform);
                // Textを取得
                text = scoreText.GetComponent<Text>();
                // テキスト設定
                text.text = "＋" + value + " OVERKILL!!!";
                break;
        }

        // キューに登録
        scoreTextQueue.Enqueue(scoreText);

        // ログを進める
        LogQueue();

        // ログ消去までの時間をリセット
        scoreLogRefreshElapsedTime = 0.0f;
    }

    private void LogQueue()
    {
        // 表示数を超えたなら一番古いログを削除
        if (scoreTextQueue.Count > maxLogNum)
        {
            var text = scoreTextQueue.Dequeue().GetComponent<Text>();
            text.CrossFadeAlpha(0.0f, 0.25f, true);
            Destroy(text.gameObject, 0.25f);
        }
    }
}
