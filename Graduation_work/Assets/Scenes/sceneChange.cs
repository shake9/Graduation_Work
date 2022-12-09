using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{

	[SerializeField]
    string targetSceneName = "HandTest";

    [SerializeField]
    //クリアした時に表示させるUI
    private GameObject clearUIPrefab;
    //クリアUIのインスタンス
    private GameObject clearUIInstanse;

    [SerializeField]
    //ゲームオーバーした時に表示させるUI
    private GameObject overUIPrefab;
    //ゲームオーバーUIのインスタンス
    private GameObject overUIInstanse;


    public PlayerHealth playerHealth;
    bool dead = false;
    bool clear = false;

    public AudioClip sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		 if (Input.GetKey(KeyCode.A)&&dead==false)
        {
            //音を鳴らす
            audioSource.PlayOneShot(sound);
            LoadScene();
        }

         //死んでたらゲームオーバー画面を表示して画面を止める
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

        //clearしたらゲームクリア画面を表示する
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
