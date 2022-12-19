using System.Collections;
using UnityEngine;

public class SimpleShoot : MonoBehaviour
{
    // 弾のPrefab
    [SerializeField] private GameObject bulletPrefab = null;

    // 射撃前エフェクトの時間
    [SerializeField] private float chargeEffectTime = 0.0f;

    public void Attack()
    {
        // 弾生成して座標と回転設定
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.transform.position = transform.position;
        bulletInstance.transform.rotation = transform.rotation;

        // 弾の発射メソッドを実行
        bulletInstance.GetComponent<EnemyBullet>().Shoot(chargeEffectTime);
    }
}
