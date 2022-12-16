using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndRunMove : AbstractMove
{
    // モデルのTransform
    [SerializeField] private Transform modelTr = null;

    // 攻撃用クラス
    [SerializeField] private SimpleShoot attack;

    // 目標地点候補の範囲(X)
    [SerializeField] private float moveRangeX;

    // 目標地点候補の範囲(Y)
    [SerializeField] private float moveRangeY;

    // プレイヤーからの距離(Z)
    [SerializeField] private float distanceZFromPlayer;

    // 一度移動してから再度移動開始するまでの時間
    [SerializeField] private float timeBetweenMove;

    // 移動しているかどうか
    private bool isMoving = false;

    private void Start()
    {
        // 登場演出(アニメーション)への数値渡しに書き換え予定
        transform.position = CalculateMovePos(enemyController.PlayerTransform.position.z + distanceZFromPlayer);

        // 登場アニメーションが終わったら行動開始するように修正する
        StartCoroutine(MainCoroutine());
    }

    private void Update()
    {
        // プレイヤーとの座標の差を計算
        Vector3 diff = enemyController.PlayerTransform.position - transform.position;

        // モデルをプレイヤーに向ける(Yを消す前に計算)
        modelTr.forward = Vector3.Normalize(diff);
    }

    // 移動場所の算出
    private Vector3 CalculateMovePos(float zPos)
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);

        return new Vector3(randomX, randomY, zPos);
    }

    // メインループのコルーチン
    private IEnumerator MainCoroutine()
    {
        while (isActiveAndEnabled)
        {
            // 移動処理を開始
            StartCoroutine(MoveCoroutine());

            // 移動終了まで待機する
            yield return new WaitUntil(() => 
            {
                return !isMoving;
            });

            // 攻撃する
            attack.Attack();

            // 次の行動まで待機
            yield return new WaitForSeconds(timeBetweenMove);
        }
    }

    // 移動用コルーチン
    private IEnumerator MoveCoroutine()
    {
        // 移動開始
        isMoving = true;
        Vector3 moveBeginPos = transform.position;
        Vector3 moveEndPos = CalculateMovePos(enemyController.PlayerTransform.position.z + distanceZFromPlayer);

        // 差を計算
        Vector3 diff = moveEndPos - moveBeginPos;
        // 移動方向をあらかじめ計算しておく
        Vector3 moveDir = Vector3.Normalize(diff);

        float currentTime = 0.0f;
        // 移動にかかる時間を計算
        float endTime = diff.magnitude / enemyController.MoveSpeed;

        // 移動が完了するまでループ
        while (currentTime < endTime)
        {
            transform.position = transform.position + moveDir * enemyController.MoveSpeed * Time.fixedDeltaTime;

            currentTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        transform.position = moveEndPos;
        isMoving = false;
    }
}
