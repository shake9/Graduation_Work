using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // プレイヤーの最大体力(初期値)
    [SerializeField] private int playerMaxHealth = 10;

    // プレイヤーの体力(現在値)
    private int playerHealth = 0;

    private bool clearflag = false;

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

    //ゲームクリアしたかどうか
    public bool IsClear()
    {
        return clearflag;
    }
}
