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

    // ��x�ɕ\�������X�R�A�\�����O�̍ő吔
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

            // �X�R�A�̃��O������
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
        // �X�R�A�擾���O�p�I�u�W�F�N�g����
        GameObject scoreText = null;

        // Text
        Text text = null;

        // �e�L�X�g����
        switch (scoreType)
        {
            case ScoreManager.ScoreType.Normal:
                scoreText = Instantiate(normalTextPrefab, scoreTextParent.transform);
                // Text���擾
                text = scoreText.GetComponent<Text>();
                // �e�L�X�g�ݒ�
                text.text = "�{" + value + " KILL";
                break;
            case ScoreManager.ScoreType.Special:
                scoreText = Instantiate(specialTextPrefab, scoreTextParent.transform);
                // Text���擾
                text = scoreText.GetComponent<Text>();
                // �e�L�X�g�ݒ�
                text.text = "�{" + value + " OVERKILL!!!";
                break;
        }

        // �L���[�ɓo�^
        scoreTextQueue.Enqueue(scoreText);

        // ���O��i�߂�
        LogQueue();

        // ���O�����܂ł̎��Ԃ����Z�b�g
        scoreLogRefreshElapsedTime = 0.0f;
    }

    private void LogQueue()
    {
        // �\�����𒴂����Ȃ��ԌÂ����O���폜
        if (scoreTextQueue.Count > maxLogNum)
        {
            var text = scoreTextQueue.Dequeue().GetComponent<Text>();
            text.CrossFadeAlpha(0.0f, 0.25f, true);
            Destroy(text.gameObject, 0.25f);
        }
    }
}
