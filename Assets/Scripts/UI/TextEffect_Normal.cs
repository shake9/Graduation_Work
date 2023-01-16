using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect_Normal : MonoBehaviour
{
    [SerializeField] private float textEffectTime;

    private void Start()
    {
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        Text text = GetComponent<Text>();
        RectTransform rectTr = GetComponent<RectTransform>();
        float start = 0.0f;
        float end = rectTr.localScale.x;

        float elapsedTime = 0.0f;

        while (elapsedTime <= textEffectTime)
        {
            // ���Ԃ̊������v�Z
            float ratio = elapsedTime / textEffectTime;

            // EaseOutBack�v�Z
            const float x = 1.70158f;
            const float y = x + 1.0f;
            float easing = 1.0f + y * Mathf.Pow(ratio - 1.0f, 3.0f) + x * Mathf.Pow(ratio - 1.0f, 2.0f);

            rectTr.localScale = new Vector2(Mathf.Lerp(start, end, easing), rectTr.localScale.y);

            Color color = text.color;
            color.a = ratio;
            text.color = color;

            // ���t���[���܂őҋ@
            yield return new WaitForFixedUpdate();

            elapsedTime += Time.fixedDeltaTime;
        }
    }
}
