using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public abstract class AbstractMove : MonoBehaviour
{
    protected EnemyController enemyController = null;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
}
