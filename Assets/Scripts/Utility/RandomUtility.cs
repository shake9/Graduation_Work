using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtility
{
    public interface IWeighted
    {
        public int GetWeight();
    }

    // �d�ݕt�������_�����I
    public static T GetWeightedRandom<T>(List<T> values) where T : IWeighted
    {
        // �d�݂̍��v�����߂�
        int totalWeight = 0;
        foreach(var value in values)
        {
            totalWeight += value.GetWeight();
        }

        // ����p�̒l��p��
        int random = Random.Range(0, totalWeight);

        // ����
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i].GetWeight() >= random)
            {
                return values[i];
            }
            random -= values[i].GetWeight();
        }

        // ������Ȃ����0�Ԃ̗v�f��Ԃ�
        return values[0];
    }
}
