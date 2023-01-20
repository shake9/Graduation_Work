using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfoDisplay : MonoBehaviour
{
    // �c��E�F�[�u���\���e�L�X�g
    [SerializeField] private Text waveNumText;

    // �c��G���\���e�L�X�g
    [SerializeField] private Text enemyNumText;

    private WaveManager waveManager;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        // �c��E�F�[�u���X�V
        waveNumText.text = waveManager.GetCurentWaveNum().ToString() + "/" + waveManager.GetMaxWaveNum().ToString();

        // �c��G���X�V
        enemyNumText.text = waveManager.GetCurrentEnemyNum().ToString() + "/" + waveManager.GetMaxEnemyNum().ToString();
    }
}
