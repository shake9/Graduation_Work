using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Target : MonoBehaviour, ITutorial
{
    [SerializeField] private Text text;
    [SerializeField] private TextFader textFader;
    [SerializeField] private AudioSource successAudio;

    [SerializeField] private string text_Intro;
    [SerializeField] private string text_ShootTargets;
    [SerializeField] private string text_Clear;

    [SerializeField] private float intervalBetweenText = 0.5f;
    [SerializeField] private List<ShootTarget> targets = new List<ShootTarget>();

    private bool isClear = false;

    public bool IsClear()
    {
        return isClear;
    }

    private void Start()
    {
        foreach (var target in targets)
        {
            target.gameObject.SetActive(false);
        }

        StartCoroutine(TextCoroutine());
    }

    private IEnumerator TextCoroutine()
    {
        // イントロを表示
        {
            ShowText(text_Intro);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            yield return new WaitForSecondsRealtime(intervalBetweenText);

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // 狙うチュートリアル
        {
            // 複数ターゲットを狙う
            ShowText(text_ShootTargets);
            textFader.FadeIn();

            // 的を出す
            foreach (var target in targets)
            {
                target.gameObject.SetActive(true);
            }

            yield return new WaitWhile(textFader.IsInFade);

            // 1回成功するまで待つ
            yield return new WaitUntil(IsShootSuccess);
            successAudio.Play();

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // クリア
        {
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
#if DEBUG
        // スペースキーでスキップ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
#endif

        // 全ての的が壊されたらtrue
        foreach (var target in targets)
        {
            if (!target.isHit)
            {
                return false;
            }
        }

        return true;
    }

    private void ShowText(string value)
    {
        text.text = value;
    }
}