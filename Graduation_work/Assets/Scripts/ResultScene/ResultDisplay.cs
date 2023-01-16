using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    // スコア表示テキスト
    [SerializeField] private Text scoreNumText;

    // マイナス
    [SerializeField] private Image minus;

    // 被ダメージ数表示テキスト
    [SerializeField] private Text damageTakenText;

    // 最終スコアテキスト
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        ScoreManager.Instance.AddEnemyKillScore(100000, ScoreManager.ScoreType.Normal);
        ScoreManager.Instance.AddPlayerDamageCount(10);

        StartCoroutine(ResultCoroutine());
    }

    private IEnumerator ResultCoroutine()
    {
        int score = ScoreManager.Instance.EnemyKillScore;
        int damageCount = ScoreManager.Instance.DamageCount;
        int damageScore = 10 * damageCount;

        // スコア表示
        scoreNumText.text = score.ToString();
        scoreNumText.gameObject.SetActive(true);

        // 待機
        yield return new WaitForSeconds(0.5f);

        minus.gameObject.SetActive(true);

        // 待機
        yield return new WaitForSeconds(0.5f);

        // 被弾数表示
        damageTakenText.text = "10 x " + damageCount.ToString() + " Hit = " + damageScore.ToString();
        damageTakenText.gameObject.SetActive(true);

        // 待機
        yield return new WaitForSeconds(0.5f);

        // 最終スコア表示
        totalScoreText.gameObject.SetActive(true);

        // 最終スコアまでゆっくり上げる
        int displayScore = 0;
        int totalScore = score - damageScore;

        // フレーム数
        float fps = 1.0f / Time.fixedDeltaTime;

        // 5秒で表示しきれるように計算
        int increment = totalScore / (int)(fps * 5.0f);

        while (displayScore < totalScore)
        {
            totalScoreText.text = displayScore.ToString();
            displayScore += increment;

            yield return new WaitForFixedUpdate();
        }

        totalScoreText.text = totalScore.ToString();
    }
}
