using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //敵プレハブ
    public GameObject enemyPrefab;
    //時間間隔の最小値
    public float mintime = 2.0f;
    //時間間隔の最大値
    public float maxtime = 5.0f;
    //ランダムなX座標の最小値
    public float minXpos = -5.0f;
    //ランダムなX座標の最大値
    public float maxXpos = 5.0f;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = GetRandomTime();
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            //enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(enemyPrefab);
            //生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
            enemy.transform.position = GetRandomPosition();
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
            //
            interval = GetRandomTime();
        }
    }

    //
    private float GetRandomTime()
    {
        return Random.Range(mintime, maxtime);
    }
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(minXpos, maxXpos);
        float y = Random.Range(0, maxXpos);
        return new Vector3(x, y, 10f);

    }
}
