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
        // �C���g����\��
        {
            ShowText(text_Intro);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            yield return new WaitForSecondsRealtime(intervalBetweenText);

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // �_���`���[�g���A��
        {
            // �����^�[�Q�b�g��_��
            ShowText(text_ShootTargets);
            textFader.FadeIn();

            // �I���o��
            foreach (var target in targets)
            {
                target.gameObject.SetActive(true);
            }

            yield return new WaitWhile(textFader.IsInFade);

            // 1�񐬌�����܂ő҂�
            yield return new WaitUntil(IsShootSuccess);
            successAudio.Play();

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // �N���A
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
        // �X�y�[�X�L�[�ŃX�L�b�v
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
#endif

        // �S�Ă̓I���󂳂ꂽ��true
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