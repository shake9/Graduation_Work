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

    private void Update()
    {
        transform.eulerAngles += new Vector3(0.0f, 0.0f, 3600.0f * Time.deltaTime);
    }

    public virtual void Shoot(float chargeTime = 0.0f)
    {
        StartCoroutine(ShootCoroutine(chargeTime));
    }

    private IEnumerator ShootCoroutine(float chargeTime)
    {
        if (chargeTime > 0.0f)
        {
            float elapsedTime = 0.0f;

            Vector3 originScale = transform.localScale;
            transform.localScale = Vector3.zero;

            while (elapsedTime <= chargeTime)
            {
                yield return new WaitForFixedUpdate();

                elapsedTime += Time.fixedDeltaTime;

                transform.localScale = originScale * elapsedTime / chargeTime;
            }
        }

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
        health.Hit();

        // 自身を破壊
        Destroy(gameObject);
    }
}
