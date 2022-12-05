using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{

    [SerializeField]
    string targetSceneName = "HandTest";
    [SerializeField]
    //�|�[�Y�������ɕ\��������UI
    private GameObject pauseUIPrefab;
    //�|�[�YUI�̃C���X�^���X
    private GameObject pauseUIInstanse;

    //�|�[�Y�������f����ϐ�
    bool isPause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if(pauseUIInstanse == null && isPause == false)
            {
                pauseUIInstanse = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
                isPause = true;
            }
            else
            {
                Destroy(pauseUIInstanse);
                Time.timeScale = 1f;
                isPause = false;
            }
        }

        if(Input.GetKeyDown("r") && isPause == true)
        {
            Destroy(pauseUIInstanse);
            Time.timeScale = 1f;
            LoadScene();
        }

        
    }

    void LoadScene()
    {
        FadeManager.Instance.LoadScene(targetSceneName, 2.0f);
    }
}
