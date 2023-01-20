using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Setting", menuName = "Custom Scriptable Object/Create Wave Setting")]
public class WaveSetting : ScriptableObject
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

    public int enemyCount = 5;
    public List<EnemySpawnRate> enemySpawnRates;
}
