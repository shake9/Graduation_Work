using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFader : MonoBehaviour
{
    [SerializeField] public float fadeTime = 1.0f;

    [SerializeField] private bool fadeInOnAwake = true;

    [SerializeField] private bool fadeOutOnAwake = false;

    private bool isInFade = false;

    private void Start()
    {
        if (fadeInOnAwake && fadeOutOnAwake)
        {
            Debug.LogWarning(gameObject.name + ":二重でフェードがかかっています");
        }

        if (fadeInOnAwake)
        {
            FadeIn();
        }

        if (fadeOutOnAwake)
        {
            FadeOut();
        }
    }

    public bool IsInFade()
    {
        return isInFade;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        isInFade = true;

        float time = 0.0f;
        var text = GetComponent<Text>();
        Color color = text.color;
        color.a = 0.0f;
        text.color = color;

        while (time < fadeTime)
        {
            color = text.color;
            color.a = time / fadeTime;
            text.color = color;

            yield return new WaitForFixedUpdate();

            time += Time.fixedDeltaTime;
        }

        isInFade = false;
    }

    private IEnumerator FadeOutCoroutine()
    {
        isInFade = true;

        float time = 0.0f;
        var text = GetComponent<Text>();
        Color color = text.color;
        color.a = 1.0f;
        text.color = color;

        while (time < fadeTime)
        {
            color = text.color;
            color.a = 1.0f - time / fadeTime;
            text.color = color;

            yield return new WaitForFixedUpdate();

            time += Time.fixedDeltaTime;
        }

        isInFade = false;
    }
}
