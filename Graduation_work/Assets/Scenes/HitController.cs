using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private string tag1;
    private string sokushi = "sokushi";
    private float Hp;
    private Animator animator;

    private void Awake()
    {
        Hp = 1.0f;
        animator = GetComponent<Animator>();

    }
    //�����蔻�胁�\�b�h
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //�Փ˂����I�u�W�F�N�g��Bullet(��C�̒e)�������Ƃ�
    //    if (collision.gameObject.CompareTag("box"))
    //    {
    //        Debug.Log("�G�ƒe���Փ˂��܂����I�I�I");
    //        Destroy(gameObject);

    //    }
    //}
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag(tag1))
        {
            Debug.Log("�G�ƒe���Փ˂��܂����I�I�I");

            Hp -= 0.25f;

        }
        else if (other.gameObject.CompareTag(sokushi))
        {
            Debug.Log("�G�ƒe���Փ˂��܂����I�I�I");

            Hp -= 1.0f;

        }
    }
    private void Update()
    {
        if (Hp <= 0.0f)
        {
            Destroy(gameObject);
        }
        animator.SetFloat("hp", Hp);
    }
}
