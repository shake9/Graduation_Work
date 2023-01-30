using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] gestureTest L_hand;

    [SerializeField] PlayerHealth playerHealth;

    // エネルギー画像
    [SerializeField] Image energy_gauge;

    [SerializeField] Image hp_gauge;

    // Start is called before the first frame update
    void Start()
    {
        // 初期値を0に
        energy_gauge.fillAmount = 0.0f;

        // 初期値を1(MAX)に
        hp_gauge.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        hp_gauge.fillAmount = (float)(playerHealth.PlayerCurrentHealth * 0.1f);

        energy_gauge.fillAmount = L_hand.energy;
    }
}
