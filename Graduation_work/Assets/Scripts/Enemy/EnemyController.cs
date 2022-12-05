using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
}
