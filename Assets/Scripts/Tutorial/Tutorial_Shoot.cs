using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Shoot : MonoBehaviour, ITutorial
{
    [SerializeField] private Text text;
    [SerializeField] private TextFader textFader;
    [SerializeField] private AudioSource successAudio;

    [SerializeField] private string text_Intro;
    [SerializeField] private string text_ShootOnce;
    [SerializeField] private string text_ShootRepeat;
    [SerializeField] private string text_Clear;

    [SerializeField] private float intervalBetweenText = 0.5f;

    private Animator demoAnimator;
    private gestureTest gestureTest;
    private bool isClear = false;

    private bool preShootFlag = false;

    public bool IsClear()
    {
        return isClear;
    }

    private void Start()
    {
        demoAnimator = GameObject.Find("te_born").GetComponent<Animator>();
        gestureTest = GameObject.Find("Fire Ball").transform.parent.GetComponent<gestureTest>();
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
            demoAnimator.SetInteger("animID", 1);

            ShowText(text_ShootOnce);
            textFader.FadeIn();
            yield return new WaitWhile(textFader.IsInFade);

            // 1�񐬌�����܂ő҂�
            yield return new WaitUntil(IsShootSuccess);
            successAudio.Play();

            textFader.FadeOut();
            yield return new WaitWhile(textFader.IsInFade);
        }

        // 3�񎎂��Ă��炤
        {
            ShowText(text_ShootRepeat);
            textFader.FadeIn();

            // 3�񐬌�����܂Ń��[�v
            for (int i = 0; i < 3; i++)
            {
                // �Ԃ��J����(����t���[�����ŕ����񔻒肪�ʂ�̂�h�~)
                yield return new WaitForSecondsRealtime(0.1f);

                yield return new WaitUntil(IsShootSuccess);

                ShowText(text_ShootRepeat + "\n(" + (i + 1).ToString() + "/3)");
                successAudio.Play();
            }

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
        bool result = !preShootFlag && gestureTest.fire;

        preShootFlag = gestureTest.fire;

        return Input.GetKeyDown(KeyCode.Space) || result;
    }

    private void ShowText(string value)
    {
        text.text = value;
    }
}
