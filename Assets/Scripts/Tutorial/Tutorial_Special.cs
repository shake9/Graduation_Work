using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Special : MonoBehaviour, ITutorial
{
    [SerializeField] private Text text;
    [SerializeField] private TextFader textFader;
    [SerializeField] private AudioSource successAudio;

    [SerializeField] private string text_Intro;
    [SerializeField] private string text_Special;
    [SerializeField] private string text_Clear;

    [SerializeField] private float intervalBetweenText = 0.5f;

    private Animator demoAnimator;
    private gestureTest gestureTest;
    private bool isClear = false;

    public bool IsClear()
    {
        return isClear;
    }

    private void Start()
    {
        demoAnimator = GameObject.Find("te_born").GetComponent<Animator>();
        gestureTest = FindObjectOfType<gestureTest>();
        StartCoroutine(TextCoroutine());
    }

    private IEnumerator TextCoroutine()
    {
        // イントロを表示
        {
            demoAnimator.SetInteger("animID", 0);
            ShowText(text_Intro);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            yield return new WaitForSecondsRealtime(intervalBetweenText);

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // 射撃チュートリアル
        {
            // お手本を見せて真似してもらう
            demoAnimator.SetInteger("animID", 2);

            ShowText(text_Special);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            // 1回成功するまで待つ
            yield return new WaitUntil(IsShootSuccess);
            successAudio.Play();

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
            yield return new WaitForSeconds(3.0f);
        }

        // クリア
        {
            demoAnimator.SetInteger("animID", 0);
            ShowText(text_Clear);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            yield return new WaitForSecondsRealtime(3.0f);

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        isClear = true;
    }

    private bool IsShootSuccess()
    {
        return Input.GetKeyDown(KeyCode.Space) || gestureTest.Sp;
    }

    private void ShowText(string value)
    {
        text.text = value;
    }
}
