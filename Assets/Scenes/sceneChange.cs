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
        //Componentを取得
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
            //音を鳴らす
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

        //clearしたらゲームクリア画面を表示する
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
