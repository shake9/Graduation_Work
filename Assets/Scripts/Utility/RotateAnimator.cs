using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimator : MonoBehaviour
{
    public Vector3 anglesPerSecond;

    private void Update()
    {
        transform.eulerAngles += anglesPerSecond * Time.deltaTime;
    }
}
