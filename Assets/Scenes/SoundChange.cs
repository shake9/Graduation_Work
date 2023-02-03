using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�ǉ��I�I

public class SoundChange : MonoBehaviour
{
    //�V���O���g���ݒ肱������
    static public SoundChange instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    //�V���O���g���ݒ肱���܂�




    public AudioSource Title_BGM;//AudioSource�^�̕ϐ�Title_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`
    public AudioSource Tutorial_BGM;//AudioSource�^�̕ϐ�Tutorial_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`
    public AudioSource Game_BGM;//AudioSource�^�̕ϐ�Game_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`
    public AudioSource Result_BGM;//AudioSource�^�̕ϐ�Result_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`

    private string beforeScene;//string�^�̕ϐ�beforeScene��錾 

    void Start()
    {
        beforeScene = "TitleScene";//�N�����̃V�[���� �������Ă���
        Title_BGM.Play();//Title_BGM��AudioSource�R���|�[�l���g�Ɋ��蓖�Ă�AudioClip���Đ�

        //�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h��o�^
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }




    //�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h�@
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        //�V�[�����ǂ��ς�������Ŕ���
        //�^�C�g������Q�[���V�[����
        if (beforeScene == "TitleScene" && nextScene.name == "HandTest")
        {
            Title_BGM.Stop();
            Game_BGM.Play();
        }

        //�^�C�g������`���[�g���A����
        if (beforeScene == "TitleScene" && nextScene.name == "TutorialScene")
        {
            Title_BGM.Stop();
            Tutorial_BGM.Play();
        }

        //�`���[�g���A������Q�[���V�[����
        if(beforeScene == "TutorialScene" && nextScene.name == "HandTest")
        {
            Tutorial_BGM.Stop();
            Game_BGM.Play();
        }

        // �Q�[���V�[�����烊�U���g��
        if (beforeScene == "HandTest" && nextScene.name == "ResultScene")
        {
            Game_BGM.Stop();
            Result_BGM.Play();
        }

        // ���U���g����^�C�g����
        if (beforeScene == "ResultScene" && nextScene.name == "TitleScene")
        {
            Result_BGM.Stop();
            Title_BGM.Play();
        }




        //�J�ڌ�̃V�[�������u�P�O�̃V�[�����v�Ƃ��ĕێ�
        beforeScene = nextScene.name;
    }
}