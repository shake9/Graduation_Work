using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty Setting", menuName = "Custom Scriptable Object/DifficultySetting")]
public class DifficultySetting : ScriptableObject
{
    [System.Serializable]
    public class EnemySpawnRate : RandomUtility.IWeighted
    {
        public int spawnRate;
        public GameObject enemyPrefab;

        public int GetWeight()
        {
            return spawnRate;
        }
    }

    public List<EnemySpawnRate> enemySpawnRates;
}
