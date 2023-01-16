using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// スコア制御用Singletonクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
	#region Singleton

	private static ScoreManager instance;

	public static ScoreManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (ScoreManager)FindObjectOfType(typeof(ScoreManager));

				if (instance == null)
                {
					// インスタンスが無ければ作成
					GameObject gameObject = new GameObject("ScoreManager");
					instance = gameObject.AddComponent<ScoreManager>();
				}
			}

			return instance;
        }
    }

    #endregion Singleton

    public enum ScoreType
    {
		Normal,
		Special
	}

    // 敵キル時スコア
    private int enemyKillScore;

	public int EnemyKillScore { get { return enemyKillScore; } }

	// プレイヤーの被弾回数
	private int damageCount;

	public int DamageCount { get { return damageCount; } }

	// スコア追加時のコールバック
	[HideInInspector] public UnityEvent<int, ScoreType> onScoreAdd = new UnityEvent<int, ScoreType>();

	private void Start()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

	/// <summary>
	/// 敵キル時のスコアを追加する
	/// </summary>
	/// <param name="value">スコアの値</param>
	/// <param name="scoreType">スコアの種類</param>
	public void AddEnemyKillScore(int value, ScoreType scoreType)
    {
		enemyKillScore += value;
		onScoreAdd.Invoke(value, scoreType);
    }

	/// <summary>
	/// プレイヤーの被ダメージカウントを追加する
	/// </summary>
	/// <param name="value"></param>
	public void AddPlayerDamageCount(int value)
    {
		damageCount += value;
    }
}
