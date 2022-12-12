using UnityEngine;

public class EnemyController : AbstractMove
{
    // 体力
    [SerializeField] private int health = 1;

    // 移動速度
    [SerializeField] private float moveSpeed = 5.0f;
    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }

    // プレイヤーのTransform
    private Transform playerTr = null;
    public Transform PlayerTransform { get { return playerTr; } private set { playerTr = value; } }

    private void Awake()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health = 0;
        }
    }
}
