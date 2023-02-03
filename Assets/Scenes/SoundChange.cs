using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //追加！！

public class SoundChange : MonoBehaviour
{
    //シングルトン設定ここから
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
    //シングルトン設定ここまで




    public AudioSource Title_BGM;//AudioSource型の変数Title_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
    public AudioSource Tutorial_BGM;//AudioSource型の変数Tutorial_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
    public AudioSource Game_BGM;//AudioSource型の変数Game_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
    public AudioSource Result_BGM;//AudioSource型の変数Result_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ

    private string beforeScene;//string型の変数beforeSceneを宣言 

    void Start()
    {
        beforeScene = "TitleScene";//起動時のシーン名 を代入しておく
        Title_BGM.Play();//Title_BGMのAudioSourceコンポーネントに割り当てたAudioClipを再生

        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }




    //シーンが切り替わった時に呼ばれるメソッド　
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        //シーンがどう変わったかで判定
        //タイトルからゲームシーンへ
        if (beforeScene == "TitleScene" && nextScene.name == "HandTest")
        {
            Title_BGM.Stop();
            Game_BGM.Play();
        }

        //タイトルからチュートリアルへ
        if (beforeScene == "TitleScene" && nextScene.name == "TutorialScene")
        {
            Title_BGM.Stop();
            Tutorial_BGM.Play();
        }

        //チュートリアルからゲームシーンへ
        if(beforeScene == "TutorialScene" && nextScene.name == "HandTest")
        {
            Tutorial_BGM.Stop();
            Game_BGM.Play();
        }

        // ゲームシーンからリザルトへ
        if (beforeScene == "HandTest" && nextScene.name == "ResultScene")
        {
            Game_BGM.Stop();
            Result_BGM.Play();
        }

        // リザルトからタイトルへ
        if (beforeScene == "ResultScene" && nextScene.name == "TitleScene")
        {
            Result_BGM.Stop();
            Title_BGM.Play();
        }




        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
}