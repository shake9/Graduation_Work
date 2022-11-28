using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody bulletRb = null;

    private void Awake()
    {
        // 自身のRigidbodyを取得
        bulletRb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 position, Quaternion rotation, float speed)
    {
        // ここで渡された座標と回転設定
        transform.position = position;
        transform.rotation = rotation;

        // 前方向に飛ばす(仮。後で自身の設定ファイル参照に変える)
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
