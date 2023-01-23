using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // プレイヤーの最大体力(初期値)
    [SerializeField] private int playerMaxHealth = 10;

    // 無敵モード(デバッグ用)
    [SerializeField] private bool debug_PlayerInvincivle = false;

    public bool isHit = false;

    public int PlayerMaxHealth { get { return playerMaxHealth; } }

    // プレイヤーの体力(現在値)
    private int playerHealth = 0;

    public int PlayerCurrentHealth { get { return playerHealth; } }

    private void Start()
    {
        // 最大値設定
        playerHealth = playerMaxHealth;
    }

    private void FixedUpdate()
    {
        if(isHit)
        {
            isHit = false;
        }
        
    }

    // ダメージを与える
    public void TakeDamage(int value)
    {
#if DEBUG
        // 無敵モードならreturn
        if (debug_PlayerInvincivle)
            return;
#endif

        if (IsDead())
            return;

        playerHealth -= value;

        ScoreManager.Instance.AddPlayerDamageCount(1);
    }

    public void Hit()
    {
        isHit = true;
    }

    // 死んでいるかどうか
    public bool IsDead()
    {
        return playerHealth <= 0;
    }
}
