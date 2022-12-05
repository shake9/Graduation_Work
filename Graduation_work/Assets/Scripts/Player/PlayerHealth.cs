using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // プレイヤーの最大体力(初期値)
    [SerializeField] private int playerMaxHealth = 10;

    public int PlayerMaxHealth { get { return playerMaxHealth; } }

    // プレイヤーの体力(現在値)
    private int playerHealth = 0;

    public int PlayerCurrentHealth { get { return playerHealth; } }

    private void Start()
    {
        // 最大値設定
        playerHealth = playerMaxHealth;
    }

    // ダメージを与える
    public void TakeDamage(int value)
    {
        if (IsDead())
            return;

        playerHealth -= value;

        Debug.Log("PlayerHP:" + playerHealth);
    }

    // 死んでいるかどうか
    public bool IsDead()
    {
        return playerHealth <= 0;
    }
}
