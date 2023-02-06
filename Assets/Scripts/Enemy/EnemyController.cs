using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 体力
    [SerializeField] private int health = 1;

    // 移動速度
    [SerializeField] private float moveSpeed = 5.0f;
    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }

    // ダメージ時エフェクトのPrefab
    [SerializeField] private GameObject damageEffectPrefab;

    // 死亡時エフェクトのPrefab
    [SerializeField] private GameObject deathEffectPrefab;

    // プレイヤーのTransform
    private Transform playerTr = null;
    public Transform PlayerTransform { get { return playerTr; } private set { playerTr = value; } }

    // インスタンスの数
    public static int enemyCount = 0;

    private void Awake()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        enemyCount++;
    }

    private void Update()
    {
#if DEBUG
        // デバッグ用の瞬殺機能(Kキー)
        if (Input.GetKeyDown(KeyCode.K))
        {
            ScoreManager.Instance.AddEnemyKillScore(100, ScoreManager.ScoreType.Normal);
            health = 0;
        }
#endif

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        var deathEffect = Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        deathEffect.transform.localScale *= 3.0f;
        Destroy(deathEffect, 3.0f);
        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ScoreManager.Instance.AddEnemyKillScore(100, ScoreManager.ScoreType.Normal);
            var damageEffect = Instantiate(damageEffectPrefab, transform.position, transform.rotation);
            damageEffect.transform.localScale *= 3.0f;
            Destroy(damageEffect, 3.0f);

            health -= 1;
        }

        if (other.gameObject.CompareTag("SpecialBullet"))
        {
            ScoreManager.Instance.AddEnemyKillScore(1000, ScoreManager.ScoreType.Special);
            health = 0;
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}