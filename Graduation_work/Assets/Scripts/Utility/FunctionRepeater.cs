using UnityEngine;
using UnityEngine.Events;

// エディタ上で指定したメソッドを繰り返し呼び出す(UnscaledTimeは必要あれば対応)
public class FunctionRepeater : MonoBehaviour
{
    // 呼び出すメソッド
    [SerializeField] public UnityEvent targetFunction;

    // リピートの間隔
    [SerializeField] public float repeatTime = 1.0f;

    // 現在の経過時間
    private float currentTime = 0.0f;

    public void Update()
    {
        // 時間を進める
        currentTime += Time.deltaTime;

        // 設定された時間分経過したら
        if (currentTime >= repeatTime)
        {
            // メソッド実行
            targetFunction.Invoke();

            // 時間リセット
            currentTime = 0.0f;
        }
    }
}
