using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    // �G�l���M�[�̍ő�l
    [SerializeField] private float maxEnergy;

    // �G�l���M�[�̌��ݒl
    [SerializeField] private float currentEnergy;

    private void Update()
    {
        currentEnergy = FindObjectOfType<gestureTest>().energy;

#if DEBUG
        // �f�o�b�O�p�̃G�l���M�[��[�L�[
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
