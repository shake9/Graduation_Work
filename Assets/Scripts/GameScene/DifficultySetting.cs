using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty Setting", menuName = "Custom Scriptable Object/Create Difficulty Setting")]
[System.Serializable]
public class DifficultySetting : ScriptableObject
{
    // ÉEÉFÅ[Éuêî
    public List<WaveSetting> waves;

    // HPî{ó¶
    public int healthMultiplier = 1;

    // ìGÇÃèoåªä‘äu
    public float timeBetweenSpawn = 3.0f;
}
