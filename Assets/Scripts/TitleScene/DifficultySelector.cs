using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField] private DifficultySetting difficultySetting;

    public void StartAnimation()
    {
        //StartCoroutine(EnlargeCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            WaveManager.difficultySetting = difficultySetting;
            FindObjectOfType<OVRScreenFade>().FadeIn();
            SceneManager.LoadScene("HandTest");
        }
    }

    private IEnumerator EnlargeCoroutine()
    {
        float time = 3.0f;
        float elapsedTime = 0.0f;
        Vector3 originScale = transform.localScale;

        while (elapsedTime < time)
        {
            float scale = elapsedTime / time;
            transform.localScale = originScale * scale;
            yield return new WaitForFixedUpdate();
            elapsedTime += Time.fixedDeltaTime;
        }

        Debug.Log("HOGE");
    }
}
