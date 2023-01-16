using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect_Special : MonoBehaviour
{
    [SerializeField] private float textEffectTime = 0.25f;

    private void Start()
    {
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        Text text = GetComponent<Text>();
        RectTransform rectTr = GetComponent<RectTransform>();
        Color originColor = Color.red;
        float maxTime = textEffectTime * 0.5f;

        // 拡大
        {
            Vector3 start = Vector3.zero;
            Vector3 end = Vector3.one * 2.0f;
            float elapsedTime = 0.0f;

            while (elapsedTime <= maxTime)
            {
                // 時間の割合を計算
                float ratio = elapsedTime / maxTime;

                // EaseOutQuart計算
                float easing = 1.0f - Mathf.Pow(1.0f - ratio, 4);

                // スケール変更
                rectTr.localScale = Vector3.Lerp(start, end, easing);

                text.color = Color.Lerp(originColor, Color.white, ratio);

                // 次フレームまで待機
                yield return new WaitForFixedUpdate();

                elapsedTime += Time.fixedDeltaTime;
            }
        }

        // 縮小
        {
            Vector3 start = rectTr.localScale;
            Vector3 end = Vector3.one;
            float elapsedTime = 0.0f;

            while (elapsedTime <= maxTime)
            {
                // 時間の割合を計算
                float ratio = elapsedTime / maxTime;

                // EaseOutQuart計算
                float easing = 1.0f - Mathf.Pow(1.0f - ratio, 4);

                // スケール変更
                rectTr.localScale = Vector3.Lerp(start, end, easing);

                text.color = Color.Lerp(Color.white, originColor, ratio);

                // 次フレームまで待機
                yield return new WaitForFixedUpdate();

                elapsedTime += Time.fixedDeltaTime;
            }
        }


    }
}
