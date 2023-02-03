using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMove : AbstractMove
{
    // 次に実行されるMove
    [SerializeField] private AbstractMove nextMove;

    // プレイヤーからの距離(Z)
    [SerializeField] private float distanceZFromPlayer;

    // 移動にかかる時間
    [SerializeField] private float moveTime;

    protected override void Awake()
    {
        base.Awake();

        // 終了するまで次のMoveを無効化する
        nextMove.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        // 経過時間
        float elapsedTime = 0.0f;

        Vector3 start = transform.position;
        Vector3 end = enemyController.PlayerTransform.position + new Vector3(0.0f, 0.0f, distanceZFromPlayer);

        // 移動処理
        while (elapsedTime < moveTime)
        {
            // 移動
            float ratio = elapsedTime / moveTime;
            transform.position = Vector3.Lerp(start, end, ratio);

            // 回転
            Vector3 angles = transform.eulerAngles;
            angles.y = Mathf.Lerp(0.0f, 720.0f, ratio);
            transform.eulerAngles = angles;

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // 次のMoveを有効化する
        nextMove.enabled = true;
    }
}
