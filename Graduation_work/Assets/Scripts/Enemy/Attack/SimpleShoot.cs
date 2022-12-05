using UnityEngine;

public class SimpleShoot : MonoBehaviour
{
    // 弾のPrefab
    [SerializeField] private GameObject bulletPrefab = null;

    public void Shoot()
    {
        // 弾生成して座標と回転設定
        GameObject bulletInstance = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.transform.position = transform.position;
        bulletInstance.transform.rotation = transform.rotation;
    }
}
