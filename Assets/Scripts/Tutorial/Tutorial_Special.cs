using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Special : MonoBehaviour, ITutorial
{
    [SerializeField] private Text text;
    [SerializeField] private TextFader textFader;
    [SerializeField] private AudioSource successAudio;
    [SerializeField] private Animator demoAnimator;

    [SerializeField] private string text_Intro;
    [SerializeField] private string text_Special;
    [SerializeField] private string text_Clear;

    [SerializeField] private float intervalBetweenText = 0.5f;

    private bool isClear = false;

    public bool IsClear()
    {
        return isClear;
    }

    private void Start()
    {
        StartCoroutine(TextCoroutine());
    }

    private IEnumerator TextCoroutine()
    {
        // �C���g����\��
        {
            demoAnimator.SetInteger("animID", 0);
            ShowText(text_Intro);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            yield return new WaitForSecondsRealtime(intervalBetweenText);

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // �ˌ��`���[�g���A��
        {
            // ����{�������Đ^�����Ă��炤
            demoAnimator.SetInteger("animID", 2);

            ShowText(text_Special);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            // 1�񐬌�����܂ő҂�
            yield return new WaitUntil(IsShootSuccess);
            successAudio.Play();

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // �N���A
        {
            demoAnimator.SetInteger("animID", 0);
            ShowText(text_Clear);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            yield return new WaitForSecondsRealtime(intervalBetweenText);

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        isClear = true;
    }

    private bool IsShootSuccess()
    {
        // �K�E�Z���m�͂悭�킩��Ȃ��̂ł��肢���܂�
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void ShowText(string value)
    {
        text.text = value;
    }
}
