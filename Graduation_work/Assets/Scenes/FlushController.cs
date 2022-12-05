using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FlushController : MonoBehaviour
{
    Image img;

    public AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;

        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
            //����炷
            audioSource.PlayOneShot(sound);
        }
        else
        {
            this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
        }
    }
}