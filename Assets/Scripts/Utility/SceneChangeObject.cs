using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeObject : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("コライダーがありません");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<OVRScreenFade>().FadeIn();
        SceneManager.LoadScene(sceneName);
    }
}
