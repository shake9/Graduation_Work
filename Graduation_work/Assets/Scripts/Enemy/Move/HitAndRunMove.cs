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

    // 線形補間用の時間
    private float currentTime = 0.0f;

    // 移動にかかる時間
    [SerializeField] private float moveTime;

    // 線形補間の開始位置
    private Vector3 moveBeginPos;
    // 線形補間の目的地
    private Vector3 moveEndPos;

    // 一度移動してから再度移動開始するまでの時間
    [SerializeField] private float timeBetweenMove;

    // 移動しているかどうか
    private bool isMoving = false;

    private void Start()
    {
        // 登場演出(アニメーション)への数値渡しに書き換え予定
        transform.position = CalculateMovePos(distanceZFromPlayer);

        // 登場アニメーションが終わったら行動開始するように修正する
        StartCoroutine(MainCoroutine());
    }

    private void Update()
    {
        // プレイヤーとの座標の差を計算
        Vector3 diff = enemyController.PlayerTransform.position - transform.position;

        // モデルをプレイヤーに向ける(Yを消す前に計算)
        modelTr.forward = Vector3.Normalize(diff);

        // 移動中の処理
        if (isMoving)
        {
            // タイマーを進める
            currentTime += Time.deltaTime;

            // 線形補間で移動
            transform.position = Vector3.Lerp(moveBeginPos, moveEndPos, currentTime / moveTime);

            // 指定時間経過したら移動停止
            if (currentTime >= moveTime)
            {
                currentTime = 0.0f;
                isMoving = false;
            }
        }
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
            // 移動開始
            moveBeginPos = transform.position;
            moveEndPos = CalculateMovePos(transform.position.z);
            isMoving = true;

            // 移動終了まで待機する
            yield return new WaitUntil(() => 
            {
                return !isMoving;
            });

            // 攻撃する
            attack.Shoot();

            // 次の行動まで待機
            yield return new WaitForSeconds(timeBetweenMove);
        }
    }
}
