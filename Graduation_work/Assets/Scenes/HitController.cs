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
    //当たり判定メソッド
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //衝突したオブジェクトがBullet(大砲の弾)だったとき
    //    if (collision.gameObject.CompareTag("box"))
    //    {
    //        Debug.Log("敵と弾が衝突しました！！！");
    //        Destroy(gameObject);

    //    }
    //}
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag(tag1))
        {
            Debug.Log("敵と弾が衝突しました！！！");

            Hp -= 0.25f;

        }
        else if (other.gameObject.CompareTag(sokushi))
        {
            Debug.Log("敵と弾が衝突しました！！！");

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
