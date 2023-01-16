using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncer : MonoBehaviour
{
    // �\���p�e�L�X�g
    [SerializeField] private Text displayText;

    // �ꕶ�����Ƃ̕\���Ԋu
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
            // �e�L�X�g��1�������\��
            displayText.text = text.Substring(0, i + 1);

            // �w��b���ҋ@
            yield return new WaitForSeconds(intervalBetweenCharacter);
        }

        // �\�����Ԃ̕��ҋ@
        yield return new WaitForSeconds(displayTime);

        displayText.text = string.Empty;
    }
}
