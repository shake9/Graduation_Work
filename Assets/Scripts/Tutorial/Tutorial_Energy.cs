using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class Tutorial_Energy : MonoBehaviour, ITutorial
{
    [SerializeField] private TextFader textFader;

    private gestureTest gestureTest;
    private bool isClear = false;

    public bool IsClear()
    {
        return isClear;
    }

    private void Start()
    {
        gestureTest = GameObject.Find("L_page").transform.parent.GetComponent<gestureTest>();
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        // テキストを表示
        textFader.FadeIn();
        yield return new WaitWhile(textFader.IsInFade);
        yield return new WaitForSecondsRealtime(1.0f);

        // 成功するまで待つ
        yield return new WaitUntil(IsEnergyFull);
        GetComponent<AudioSource>().Play();

        // テキストが消えるまで待つ
        textFader.FadeOut();
        yield return new WaitWhile(textFader.IsInFade);

        isClear = true;
    }

    private bool IsEnergyFull()
    {
        return gestureTest.energy >= 1.0f || Input.GetKeyDown(KeyCode.Space);
    }
}
