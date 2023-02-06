using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty Setting", menuName = "Custom Scriptable Object/Create Difficulty Setting")]
[System.Serializable]
public class DifficultySetting : ScriptableObject
{
    // �E�F�[�u��
    public List<WaveSetting> waves;

    // HP�{��
    public int healthMultiplier = 1;
}
