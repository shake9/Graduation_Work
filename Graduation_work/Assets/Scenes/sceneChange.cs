using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{

    [SerializeField]
    string targetSceneName = "HandTest";

    [SerializeField]
    //�N���A�������ɕ\��������UI
    private GameObject clearUIPrefab;
    //�N���AUI�̃C���X�^���X
    private GameObject clearUIInstanse;

    [SerializeField]
    //�Q�[���I�[�o�[�������ɕ\��������UI
    private GameObject overUIPrefab;
    //�Q�[���I�[�o�[UI�̃C���X�^���X
    private GameObject overUIInstanse;
    //�t�F�[�h�֘A
    [SerializeField]
    OVRScreenFade fade;


    public PlayerHealth playerHealth;
    bool dead = false;
    bool clear = false;
    bool ishit = false;
    bool isHitTrigger = false;

    public AudioClip sound;
    AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();
        ishit = false;
        isHitTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && dead == false || (ishit && isHitTrigger))
        {
            //����炷
            audioSource.PlayOneShot(sound);
            LoadScene();
            isHitTrigger = false;
        }


        //����ł���Q�[���I�[�o�[��ʂ�\�����ĉ�ʂ��~�߂�
        dead = playerHealth.IsDead();
        //if (dead)
        //{
        //    if (overUIInstanse == null)
        //    {
        //        overUIInstanse = GameObject.Instantiate(overUIPrefab) as GameObject;
        //        Time.timeScale = 0f;
        //    }

        //    if (Input.GetKey(KeyCode.D))
        //    {
        //        Destroy(overUIInstanse);
        //        Time.timeScale = 1f;
        //        targetSceneName = "TitleScene"; 
        //        FadeManager.Instance.LoadScene(targetSceneName, 2.0f);
        //    }
        //}

        if (dead)
        {
            if (overUIInstanse == null)
            {
                overUIInstanse = GameObject.Instantiate(overUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Time.timeScale = 1f;
                targetSceneName = "TitleScene";
                LoadScene();
            }
        }

        //clear������Q�[���N���A��ʂ�\������
        //clear = playerHealth.IsClear();
        //if (clear)
        //{
        //    if (clearUIInstanse == null)
        //    {
        //        clearUIInstanse = GameObject.Instantiate(clearUIPrefab) as GameObject;
        //        Time.timeScale = 0f;
        //    }

        //    if (Input.GetKey(KeyCode.D))
        //    {
        //        Destroy(clearUIInstanse);
        //        Time.timeScale = 1f;
        //        targetSceneName = "TitleScene";
        //        FadeManager.Instance.LoadScene(targetSceneName, 2.0f);
        //    }
        //}

    }


    //�����蔻�胁�\�b�h
    private void OnTriggerEnter(Collider other)
    {
        //�Փ˂����I�u�W�F�N�g��Bullet(��C�̒e)�������Ƃ�
        if (other.gameObject.CompareTag("hand"))
        {

            ishit = true;
        }
        Debug.Log("�G�ƒe���Փ˂��܂����I�I�I");

    }

    void LoadScene()
    {
        fade.FadeIn();
        SceneManager.LoadScene(targetSceneName);
    }
}
