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
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<AudioSource>().Play();
        }
    }
}
