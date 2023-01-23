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
        // �e�L�X�g��\��
        textFader.FadeIn();
        yield return new WaitWhile(textFader.IsInFade);
        yield return new WaitForSecondsRealtime(1.0f);

        // ��������܂ő҂�
        yield return new WaitUntil(playerEnergy.IsEnergyFull);
        GetComponent<AudioSource>().Play();

        // �e�L�X�g��������܂ő҂�
        textFader.FadeOut();
        yield return new WaitWhile(textFader.IsInFade);

        isClear = true;
    }
}
