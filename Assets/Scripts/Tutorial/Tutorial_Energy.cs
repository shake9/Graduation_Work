using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Energy : MonoBehaviour, ITutorial
{
    [SerializeField] private TextFader textFader;

    private PlayerEnergy playerEnergy;
    private bool isClear = false;

    public bool IsClear()
    {
        return isClear;
    }

    private void Start()
    {
        playerEnergy = FindObjectOfType<PlayerEnergy>();
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        // テキストを表示
        textFader.FadeIn();
        yield return new WaitWhile(textFader.IsInFade);
        yield return new WaitForSecondsRealtime(1.0f);

        // 成功するまで待つ
        yield return new WaitUntil(playerEnergy.IsEnergyFull);
        GetComponent<AudioSource>().Play();

        // テキストが消えるまで待つ
        textFader.FadeOut();
        yield return new WaitWhile(textFader.IsInFade);

        isClear = true;
    }
}
