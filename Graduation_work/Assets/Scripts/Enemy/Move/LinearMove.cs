using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 直線で動く
public class LinearMove : AbstractMove
{
    // 停止距離
    [SerializeField] private float minDistanceFromPlayer = 5.0f;

    // モデルのTransform
    [SerializeField] private Transform modelTr = null;

    private void Update()
    {
        // プレイヤーとの座標の差を計算
        Vector3 diff = enemyController.PlayerTransform.position - transform.position;

        // モデルをプレイヤーに向ける(Yを消す前に計算)
        modelTr.forward = Vector3.Normalize(diff);;

        // 平行移動したいのでYの差は考慮しない
        diff.y = 0.0f;

        // 正規化して方向にする
        Vector3 direction = Vector3.Normalize(diff);

        // 現在の距離を算出
        float sqrDistance = Vector3.SqrMagnitude(diff);

        // 停止距離と比較して判定
        if (sqrDistance > Mathf.Pow(minDistanceFromPlayer, 2))
        {
            // 移動量計算
            Vector3 move = direction * enemyController.MoveSpeed * Time.deltaTime;

            // 移動する
            transform.position = transform.position + move;
        }
    }
}
