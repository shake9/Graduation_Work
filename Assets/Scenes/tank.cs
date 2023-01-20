using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : MonoBehaviour
{
    Animator animator;
    [SerializeField] gestureTest L_hand;
    [SerializeField] gestureTest R_hand;
    private float energy = 0.0f;
    void Start()
    {
        animator = GetComponent<Animator>();
        //L_hand = GetComponent<gestureTest>();
        //R_hand = GetComponent<gestureTest>();
    }

    void Update()
    {
        float L_energy = L_hand.energy;

       

        energy = L_energy;


        animator.SetFloat("Energy", energy);
    }

}
