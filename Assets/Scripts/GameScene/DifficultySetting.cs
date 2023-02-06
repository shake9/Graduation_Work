using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty Setting", menuName = "Custom Scriptable Object/Create Difficulty Setting")]
[System.Serializable]
public class DifficultySetting : ScriptableObject
{
    // ウェーブ数
    public List<WaveSetting> waves;

    // HP倍率
    public int healthMultiplier = 1;
}
