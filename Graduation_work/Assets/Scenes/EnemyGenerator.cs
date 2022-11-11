using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //�G�v���n�u
    public GameObject enemyPrefab;
    //���ԊԊu�̍ŏ��l
    public float mintime = 2.0f;
    //���ԊԊu�̍ő�l
    public float maxtime = 5.0f;
    //�����_����X���W�̍ŏ��l
    public float minXpos = -5.0f;
    //�����_����X���W�̍ő�l
    public float maxXpos = 5.0f;
    //�G�������ԊԊu
    private float interval;
    //�o�ߎ���
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //���ԊԊu�����肷��
        interval = GetRandomTime();
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԍv��
        time += Time.deltaTime;

        //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
        if (time > interval)
        {
            //enemy���C���X�^���X������(��������)
            GameObject enemy = Instantiate(enemyPrefab);
            //���������G�̍��W�����肷��(����X=0,Y=10,Z=20�̈ʒu�ɏo��)
            enemy.transform.position = GetRandomPosition();
            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            time = 0f;
            //
            interval = GetRandomTime();
        }
    }

    //
    private float GetRandomTime()
    {
        return Random.Range(mintime, maxtime);
    }
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(minXpos, maxXpos);
        float y = Random.Range(0, maxXpos);
        return new Vector3(x, y, 10f);

    }
}
