using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WaveSetting.EnemySpawnRate))]
public class EnemySpawnRateDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // èoåªÇÃèdÇ›Çï`âÊ
        var spawnRate = property.FindPropertyRelative("spawnRate");
        var spawnRateRect = position;
        spawnRateRect.width = EditorGUIUtility.labelWidth + EditorGUIUtility.fieldWidth;
        spawnRateRect.height = EditorGUIUtility.singleLineHeight;
        spawnRate.intValue = EditorGUI.IntField(spawnRateRect, "Spawn Rate", spawnRate.intValue);

        // ìGÇÃPrefabÇï`âÊ
        var enemyPrefab = property.FindPropertyRelative("enemyPrefab");
        var enemyPrefabRect = spawnRateRect;
        enemyPrefabRect.x += spawnRateRect.width;
        enemyPrefabRect.width = EditorGUIUtility.labelWidth + EditorGUIUtility.fieldWidth;
        EditorGUI.ObjectField(enemyPrefabRect, enemyPrefab, typeof(GameObject));
    }
}
