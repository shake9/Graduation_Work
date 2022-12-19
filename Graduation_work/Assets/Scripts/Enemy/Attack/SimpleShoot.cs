using System.Collections;
using UnityEngine;

public class SimpleShoot : MonoBehaviour
{
    // 弾のPrefab
    [SerializeField] private GameObject bulletPrefab = null;

    // 射撃前エフェクトの時間
    [SerializeField] private float chargeEffectTime = 0.0f;

    // 射撃前エフェクト中かどうか
    private bool isInChargeEffect = false;

    // エフェクト中の弾
    private Transform bulletInstanceTr = null;

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    // 攻撃のコルーチン
    private IEnumerator AttackCoroutine()
    {
        // 弾生成して座標と回転設定
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.transform.position = transform.position;
        bulletInstance.transform.rotation = transform.rotation;

        // チャージエフェクトを再生
        StartCoroutine(ChargeEffectCoroutine(bulletInstance.transform));

        // チャージエフェクトが終わるまで待つ
        yield return new WaitUntil(() => { return !isInChargeEffect; });

        // 弾の発射メソッドを実行
        bulletInstance.GetComponent<EnemyBullet>().Shoot();
    }

    // 攻撃前エフェクトのコルーチン
    private IEnumerator ChargeEffectCoroutine(Transform bulletTransform)
    {
        isInChargeEffect = true;
        bulletInstanceTr = bulletTransform;

        float currentTime = 0.0f;

        // 目標になるスケールを保存
        Vector3 originScale = bulletTransform.localScale;

        while (currentTime < chargeEffectTime)
        {
            bulletTransform.localScale = Vector3.Lerp(Vector3.zero, originScale, currentTime / chargeEffectTime);

            currentTime += Time.fixedDeltaTime;

            // 次の更新処理まで待機
            yield return new WaitForFixedUpdate();
        }

        bulletTransform.localScale = originScale;

        bulletInstanceTr = null;
        isInChargeEffect = false;
    }

    private void OnDestroy()
    {
        if (isInChargeEffect && bulletInstanceTr != null)
        {
            Destroy(bulletInstanceTr.gameObject);
        }
    }
}
