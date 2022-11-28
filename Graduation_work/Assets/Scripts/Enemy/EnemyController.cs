using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 移動速度
    [SerializeField] private float moveSpeed = 5.0f;

    // 弾のPrefab
    [SerializeField] private GameObject bulletPrefab = null;

    // プレイヤーのTransform(シーン上で指定)
    [SerializeField] private Transform playerTr = null;

    // 停止距離
    [SerializeField] private float minDistanceFromPlayer = 5.0f;

    // 弾の生成位置のTransform(これは仮が良さげ)
    [SerializeField] private Transform bulletSpawnTr;

    public void Update()
    {
        // プレイヤーとの座標の差を計算
        Vector3 diff = playerTr.position - transform.position;

        // 正規化して方向にする
        Vector3 direction = Vector3.Normalize(diff);

        // 現在の距離を算出
        float sqrDistance = Vector3.SqrMagnitude(diff);

        transform.forward = direction;

        // 停止距離と比較して判定
        if (sqrDistance > Mathf.Pow(minDistanceFromPlayer, 2))
        {
            direction.y = 0.0f;

            // 移動する
            transform.position = transform.position + direction * moveSpeed * Time.deltaTime;
        }
    }

    public void Shoot()
    {
        // 弾生成
        GameObject bulletInstance = GameObject.Instantiate(bulletPrefab, bulletSpawnTr.position, bulletSpawnTr.rotation);

        var bullet = bulletInstance.GetComponent<EnemyBullet>();
        bullet.Shoot(bulletSpawnTr.position, bulletSpawnTr.rotation, 10.0f);
    }
}
