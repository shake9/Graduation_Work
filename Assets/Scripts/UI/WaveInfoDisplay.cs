using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfoDisplay : MonoBehaviour
{
    // 残りウェーブ数表示テキスト
    [SerializeField] private Text waveNumText;

    // 残り敵数表示テキスト
    [SerializeField] private Text enemyNumText;

    private WaveManager waveManager;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        // 残りウェーブ数更新
        waveNumText.text = waveManager.GetCurentWaveNum().ToString() + "/" + waveManager.GetMaxWaveNum().ToString();

        // 残り敵数更新
        enemyNumText.text = waveManager.GetCurrentEnemyNum().ToString() + "/" + waveManager.GetMaxEnemyNum().ToString();
    }
}
