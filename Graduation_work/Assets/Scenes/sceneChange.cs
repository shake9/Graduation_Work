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


    public PlayerHealth playerHealth;
    bool dead = false;
    bool clear = false;

    public AudioClip sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		 if (Input.GetKey(KeyCode.A)&&dead==false)
        {
            //����炷
            audioSource.PlayOneShot(sound);
            LoadScene();
        }

         //����ł���Q�[���I�[�o�[��ʂ�\�����ĉ�ʂ��~�߂�
        dead = playerHealth.IsDead();
        if (dead)
        {
            if (overUIInstanse == null)
            {
                overUIInstanse = GameObject.Instantiate(overUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                Destroy(overUIInstanse);
                Time.timeScale = 1f;
                targetSceneName = "TitleScene"; 
                FadeManager.Instance.LoadScene(targetSceneName, 2.0f);
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

	void LoadScene()
	{
		FadeManager.Instance.LoadScene (targetSceneName, 2.0f);
	}
}
