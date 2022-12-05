using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody bulletRb = null;

    // 弾の速度
    [SerializeField] private float speed = 1.0f;

    private void Awake()
    {
        // 自身のRigidbodyを取得
        bulletRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 前方向に飛ばす
        bulletRb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        // プレイヤーでなかったらreturn
        if (!other.transform.CompareTag("Player"))
            return;

        // GetComponentしてダメージを与える(あんまり重くなるようなら別の方法に変更する)
        var health = other.transform.GetComponent<PlayerHealth>();
        // ダメージ1は仮。後で設定ファイル参照に変える
        health.TakeDamage(1);

        // 自身を破壊
        Destroy(gameObject);
    }
}
