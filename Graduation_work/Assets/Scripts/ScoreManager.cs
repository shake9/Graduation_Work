using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �X�R�A����pSingleton�N���X
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
					// �C���X�^���X��������΍쐬
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

    // �G�L�����X�R�A
    private int enemyKillScore;

	public int EnemyKillScore { get { return enemyKillScore; } }

	// �v���C���[�̔�e��
	private int damageCount;

	public int DamageCount { get { return damageCount; } }

	// �X�R�A�ǉ����̃R�[���o�b�N
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
	/// �G�L�����̃X�R�A��ǉ�����
	/// </summary>
	/// <param name="value">�X�R�A�̒l</param>
	/// <param name="scoreType">�X�R�A�̎��</param>
	public void AddEnemyKillScore(int value, ScoreType scoreType)
    {
		enemyKillScore += value;
		onScoreAdd.Invoke(value, scoreType);
    }

	/// <summary>
	/// �v���C���[�̔�_���[�W�J�E���g��ǉ�����
	/// </summary>
	/// <param name="value"></param>
	public void AddPlayerDamageCount(int value)
    {
		damageCount += value;
    }
}
