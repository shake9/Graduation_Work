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
    //フェード関連
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
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
        ishit = false;
        isHitTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && dead == false || (ishit && isHitTrigger))
        {
            //音を鳴らす
            audioSource.PlayOneShot(sound);
            LoadScene();
            isHitTrigger = false;
        }


        //死んでたらゲームオーバー画面を表示して画面を止める
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


    //当たり判定メソッド
    private void OnTriggerEnter(Collider other)
    {
        //衝突したオブジェクトがBullet(大砲の弾)だったとき
        if (other.gameObject.CompareTag("hand"))
        {

            ishit = true;
        }
        Debug.Log("敵と弾が衝突しました！！！");

    }

    void LoadScene()
    {
        fade.FadeIn();
        SceneManager.LoadScene(targetSceneName);
    }
}
