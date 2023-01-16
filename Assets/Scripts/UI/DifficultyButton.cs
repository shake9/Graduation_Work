using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // �{�^�����������Ƃ��ɐݒ肷���Փx
    [SerializeField] private DifficultySetting difficultySetting;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty()
    {
        WaveManager.difficultySetting = difficultySetting;
        FadeManager.Instance.LoadScene("HandTest", 1.0f);
    }
}
