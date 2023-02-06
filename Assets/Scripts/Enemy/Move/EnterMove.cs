using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMove : AbstractMove
{
    // ���Ɏ��s�����Move
    [SerializeField] private AbstractMove nextMove;

    // �v���C���[����̋���(Z)
    [SerializeField] private float distanceZFromPlayer;

    // �ړ��ɂ����鎞��
    [SerializeField] private float moveTime;

    protected override void Awake()
    {
        base.Awake();

        // �I������܂Ŏ���Move�𖳌�������
        nextMove.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        // �o�ߎ���
        float elapsedTime = 0.0f;

        Vector3 start = transform.position;
        Vector3 end = enemyController.PlayerTransform.position + new Vector3(0.0f, 0.0f, distanceZFromPlayer);

        // �ړ�����
        while (elapsedTime < moveTime)
        {
            // �ړ�
            float ratio = elapsedTime / moveTime;
            transform.position = Vector3.Lerp(start, end, ratio);

            // ��]
            Vector3 angles = transform.eulerAngles;
            angles.y = Mathf.Lerp(0.0f, 720.0f, ratio);
            transform.eulerAngles = angles;

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // ����Move��L��������
        nextMove.enabled = true;
    }
}
