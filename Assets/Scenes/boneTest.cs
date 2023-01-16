using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class boneTest : MonoBehaviour
{

    Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    [SerializeField]
    OVRHand ovrHand;
    [SerializeField]
    OVRHand hand;
    public bool Guu
    {
        get
        {
            //薬指と小指は値が0になることが多いのであきらめた
            return (hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) >= 0.5f &&
                    hand.GetFingerPinchStrength(OVRHand.HandFinger.Index) >= 0.1f);
        }
    }

    public bool Choki
    {
        get
        {
            return ((hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky) >= 0.05f) && (hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring) >= 0.2f));
        }
    }

    public bool Paa
    {
        get
        {
            return (hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb) <= 0.01f);
        }
    }
    void Update()
    {
        //if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index) &&
        //    ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb))
        //{
        //    Debug.Log("つまんでる！");
        //}

        if (Guu)
        {
            Debug.Log("グー");
            animator.SetBool("Fire", false);
        }
        else if (Choki)
        {
           // Debug.Log("チョキ");
            animator.SetBool("Fire", true);
        }
        else if (Paa)
        {
            //Debug.Log("パー");
            animator.SetBool("Fire", true);
        }
        else
        {
            //Debug.Log("読み込めん");
            animator.SetBool("Fire", true);
        }
    }
}
