using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndRunMove : AbstractMove
{
    // 攻撃用クラス
    [SerializeField] private SimpleShoot attack;

    private void Update()
    {
        attack.Shoot();
    }
}
