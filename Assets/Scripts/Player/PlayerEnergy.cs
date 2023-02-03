using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    // エネルギーの最大値
    [SerializeField] private float maxEnergy;

    // エネルギーの現在値
    [SerializeField] private float currentEnergy;

    private void Update()
    {
        currentEnergy = FindObjectOfType<gestureTest>().energy;

#if DEBUG
        // デバッグ用のエネルギー補充キー
        if (Input.GetKey(KeyCode.Space))
        {
            currentEnergy = maxEnergy;
        }
#endif
    }

    public bool IsEnergyFull()
    {
        return currentEnergy == maxEnergy;
    }

    public void SetMaxEnergy(float value)
    {
        maxEnergy = value;
        currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

    public float GetEnergy()
    {
        return currentEnergy;
    }

    public void SetEnergy(float value)
    {
        currentEnergy = value;
        currentEnergy = Mathf.Clamp(currentEnergy, 0.0f, maxEnergy);
    }

    public void AddEnergy(float value)
    {
        currentEnergy += value;
        currentEnergy = Mathf.Clamp(currentEnergy, 0.0f, maxEnergy);
    }

    public void RemoveEnergy(float value)
    {
        currentEnergy -= value;
        currentEnergy = Mathf.Clamp(currentEnergy, 0.0f, maxEnergy);
    }
}
