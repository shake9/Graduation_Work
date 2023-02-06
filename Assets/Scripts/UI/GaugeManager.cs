using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] gestureTest L_hand;

    [SerializeField] PlayerHealth playerHealth;

    // �G�l���M�[�摜
    [SerializeField] Image energy_gauge;

    [SerializeField] Image hp_gauge;

    // Start is called before the first frame update
    void Start()
    {
        // �����l��0��
        energy_gauge.fillAmount = 0.0f;

        // �����l��1(MAX)��
        hp_gauge.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        hp_gauge.fillAmount = (float)(playerHealth.PlayerCurrentHealth * 0.1f);

        energy_gauge.fillAmount = L_hand.energy;
    }
}
