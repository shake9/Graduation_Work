using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncer : MonoBehaviour
{
    // 表示用テキスト
    [SerializeField] private Text displayText;

    // 一文字ごとの表示間隔
    [SerializeField] private float intervalBetweenCharacter = 0.1f;

    public void AnnounceText(string text, float displayTime = 1.0f)
    {
        StopAllCoroutines();
        StartCoroutine(TextDisplayCoroutine(text, displayTime));
    }

    private IEnumerator TextDisplayCoroutine(string text, float displayTime)
    {
        for (int i = 0; i < text.Length; i++)
        {
            // テキストを1文字ずつ表示
            displayText.text = text.Substring(0, i + 1);

            // 指定秒数待機
            yield return new WaitForSeconds(intervalBetweenCharacter);
        }

        // 表示時間の分待機
        yield return new WaitForSeconds(displayTime);

        displayText.text = string.Empty;
    }
}
