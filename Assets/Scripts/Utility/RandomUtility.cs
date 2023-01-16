using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtility
{
    public interface IWeighted
    {
        public int GetWeight();
    }

    // 重み付きランダム抽選
    public static T GetWeightedRandom<T>(List<T> values) where T : IWeighted
    {
        // 重みの合計を求める
        int totalWeight = 0;
        foreach(var value in values)
        {
            totalWeight += value.GetWeight();
        }

        // 判定用の値を用意
        int random = Random.Range(0, totalWeight);

        // 判定
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i].GetWeight() >= random)
            {
                return values[i];
            }
            random -= values[i].GetWeight();
        }

        // 見つからなければ0番の要素を返す
        return values[0];
    }
}
