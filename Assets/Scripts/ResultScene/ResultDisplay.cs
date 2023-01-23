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

        // 最終スコア
        int totalScore = score - damageScore;

        // 桁数
        int level = totalScore.ToString().Length;

        // 最終スコア表示
        totalScoreText.gameObject.SetActive(true);

        // 1桁ずつ表示
        for (int i = 0; i < level + 1; i++)
        {
            totalScoreText.text = totalScore.ToString().Substring(level - i, i);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
