using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTarget : MonoBehaviour
{
    public bool isHit = false;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("SpecialBullet"))
        {
            isHit = true;
            gameObject.SetActive(false);
        }
    }
}
