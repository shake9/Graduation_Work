using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{

    [SerializeField]
    string targetSceneName = "HandTest";
    [SerializeField]
    //ポーズした時に表示させるUI
    private GameObject pauseUIPrefab;
    //ポーズUIのインスタンス
    private GameObject pauseUIInstanse;

    //ポーズ中か判断する変数
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
