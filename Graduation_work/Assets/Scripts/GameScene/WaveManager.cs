using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // �����ŊJ�n���邩(�f�o�b�O�p)
    [SerializeField] private bool autoStart = false;

    // �E�F�[�u�ƃE�F�[�u�̊Ԋu
    [SerializeField] private float intervalBetweenWave = 2.0f;

    // �E�F�[�u�̑���
    private int waveCount = 3;

    // ���݂̃E�F�[�u
    private SimpleWave currentWave = null;

    // �o���͈�(X)
    [SerializeField] private float enemySpawnRangeX = 5.0f;

    // ��Փx�ݒ�
    [SerializeField] private DifficultySetting localDifficultySetting = null;

    // �A�i�E���X�p�e�L�X�g
    [SerializeField] private TextAnnouncer announceText = null;

    // ��Փx�ݒ�(static)
    public static DifficultySetting difficultySetting;

    public void Start()
    {
        if (difficultySetting != null)
            localDifficultySetting = difficultySetting;

        if (autoStart)
            StartWave(0.0f);
    }

    /// <summary>
    /// �Q�[���J�n
    /// </summary>
    /// <param name="delay">�x�����s(��ʃt�F�[�h�ƍ��킹��p)</param>
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
        return currentWave.waveNum;
    }

    // ���C�����[�v�p
    private IEnumerator MainCoroutine(float delay)
    {
        // �Q�[���J�n�̒x���ݒ蕪�ҋ@
        yield return new WaitForSeconds(delay);

        Debug.Log("�Q�[���J�n�I");

        // �E�F�[�u���̕����[�v
        for (int i = 0; i < waveCount; i++)
        {
            currentWave = GenerateWave(i);

            // TODO:�����ŃE�F�[�u�J�n���o
            announceText.AnnounceText("�E�F�[�u" + (i + 1) + "�J�n�I");

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
            announceText.AnnounceText("�E�F�[�u" + (i + 1) + "�I���I");

            // �E�F�[�u�Ԋu�̕��ҋ@
            yield return new WaitForSeconds(intervalBetweenWave);

            currentWave = null;
        }

        Debug.Log("�Q�[���I���I");
    }

    private SimpleWave GenerateWave(int waveNum)
    {
        return new SimpleWave(waveNum + 1, 10, 1.0f, localDifficultySetting.waves[waveNum]);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-enemySpawnRangeX, enemySpawnRangeX);
        float y = transform.position.y;
        return new Vector3(x, y, transform.position.z);
    }
}
