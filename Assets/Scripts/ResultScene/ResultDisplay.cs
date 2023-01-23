using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    // �X�R�A�\���e�L�X�g
    [SerializeField] private Text scoreNumText;

    // �}�C�i�X
    [SerializeField] private Image minus;

    // ��_���[�W���\���e�L�X�g
    [SerializeField] private Text damageTakenText;

    // �ŏI�X�R�A�e�L�X�g
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        ScoreManager.Instance.AddEnemyKillScore(100000, ScoreManager.ScoreType.Normal);
        ScoreManager.Instance.AddPlayerDamageCount(11);

        StartCoroutine(ResultCoroutine());
    }

    private IEnumerator ResultCoroutine()
    {
        int score = ScoreManager.Instance.EnemyKillScore;
        int damageCount = ScoreManager.Instance.DamageCount;
        int damageScore = 10 * damageCount;

        // �X�R�A�\��
        scoreNumText.text = score.ToString();
        scoreNumText.gameObject.SetActive(true);

        // �ҋ@
        yield return new WaitForSeconds(0.5f);

        minus.gameObject.SetActive(true);

        // �ҋ@
        yield return new WaitForSeconds(0.5f);

        // ��e���\��
        damageTakenText.text = "10 x " + damageCount.ToString() + " Hit = " + damageScore.ToString();
        damageTakenText.gameObject.SetActive(true);

        // �ҋ@
        yield return new WaitForSeconds(0.5f);

        // �ŏI�X�R�A
        int totalScore = score - damageScore;

        // ����
        int level = totalScore.ToString().Length;

        // �ŏI�X�R�A�\��
        totalScoreText.gameObject.SetActive(true);

        // 1�����\��
        for (int i = 0; i < level + 1; i++)
        {
            totalScoreText.text = totalScore.ToString().Substring(level - i, i);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
