using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // �E�F�[�u��
    [SerializeField] private int waveCount = 3;

    // ���݂̃E�F�[�u
    private SimpleWave currentWave = null;

    // �o���͈�(X)
    [SerializeField] private float enemySpawnRangeX = 5.0f;

    // ��Փx�ݒ�
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

    // ���C�����[�v�p
    private IEnumerator MainCoroutine()
    {
        Debug.Log("�Q�[���J�n�I");

        // �E�F�[�u���̕����[�v
        for (int i = 0; i < waveCount; i++)
        {
            currentWave = GenerateWave(i + 1);

            // TODO:�����ŃE�F�[�u�J�n���o
            Debug.Log("�E�F�[�u" + (i + 1) + "�J�n�I");

            // �G��S�ĕ�������܂ŏI���܂Ń��[�v
            while (!currentWave.IsSpawnEnd())
            {
                // �G�𐶐�
                GameObject enemyInstance = Instantiate(currentWave.GetNextSpawnPrefab());
                // ���W�������_���ɐݒ�
                enemyInstance.transform.position = GetRandomPosition();

                // ���̏o���܂őҋ@
                yield return new WaitForSeconds(currentWave.timeBetweenSpawn);
            }

            // �E�F�[�u�I��(�G�S��)�܂őҋ@
            yield return new WaitUntil(() => { return currentWave.IsEnd(); });

            // TODO:�����ŃE�F�[�u�N���A���o
            Debug.Log("�E�F�[�u" + (i + 1) + "�I���I");

            currentWave = null;
        }

        Debug.Log("�Q�[���I���I");
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
