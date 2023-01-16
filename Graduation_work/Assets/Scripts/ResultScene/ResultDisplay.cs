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
        ScoreManager.Instance.AddPlayerDamageCount(10);

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

        // �ŏI�X�R�A�\��
        totalScoreText.gameObject.SetActive(true);

        // �ŏI�X�R�A�܂ł������グ��
        int displayScore = 0;
        int totalScore = score - damageScore;

        // �t���[����
        float fps = 1.0f / Time.fixedDeltaTime;

        // 5�b�ŕ\���������悤�Ɍv�Z
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
