using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp_spel : MonoBehaviour
{
    [SerializeField] gestureTest R_spel;
    public bool L_hit = false;
    public bool R_hit = false;
    public bool Isfire = false;
    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(L_hit)
        {
            time++;
        }
        
        if (time >= 10)
        {
            L_hit = false;
            time = 0;
        }

        if (L_hit && R_hit)
        {
            R_spel.Sp = true;
        }

        if(Isfire)
        {
            R_spel.Sp = false;
            Isfire = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(R_spel.Isspel)
        {
            //衝突したオブジェクトがBullet(大砲の弾)だったとき
            if (other.gameObject.CompareTag("L_page"))
            {

                L_hit = true;
            }

            if (other.gameObject.CompareTag("R_page"))
            {

                R_hit = true;
            }
            Debug.Log("スペル");
        }
        

    }
}
