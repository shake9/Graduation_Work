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

    [SerializeField] WaveManager waveManager;


    public PlayerHealth playerHealth;
    private int time = 0;
    bool move = false;
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
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && dead == false || (ishit && isHitTrigger))
        {
            //����炷
            audioSource.PlayOneShot(sound);
           
            move = true;
        }
        if(move)
        {
            time++;
        }

        if(move && time >= 300)
        {
            LoadScene();
            isHitTrigger = false;
            move = false;
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
                Time.timeScale = 0.5f;
                
            }
            time++;
            if (Input.GetKeyDown(KeyCode.D)|| time >= 120)
            {
                Time.timeScale = 1f;
                targetSceneName = "TitleScene";
                LoadScene();
            }
        }

        //clear������Q�[���N���A��ʂ�\������
        if(waveManager != null)
        {
            clear = waveManager.isClear;
        }
        else
        {
            clear = false;
        }
        
        if (clear)
        {
            //if (clearUIInstanse == null)
            //{
            //    //clearUIInstanse = GameObject.Instantiate(clearUIPrefab) as GameObject;
            //    Time.timeScale = 1f;
            //}
            time++;
            if (Input.GetKey(KeyCode.D) || time >= 240)
            {
                //Destroy(clearUIInstanse);
                Time.timeScale = 1f;
                targetSceneName = "TitleScene";
                LoadScene();
                //FadeManager.Instance.LoadScene(targetSceneName, 2.0f);
            }
        }

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
