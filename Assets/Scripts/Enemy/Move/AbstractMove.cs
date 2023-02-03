using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public abstract class AbstractMove : MonoBehaviour
{
    protected EnemyController enemyController = null;

    protected virtual void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
}
