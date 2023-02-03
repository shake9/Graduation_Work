using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInteract : MonoBehaviour
{
    [SerializeField] private List<DifficultySelector> selectors;

    private bool isRunning = false;

    public int animationNum = 0;

    private void Awake()
    {
        foreach (var selector in selectors)
        {
            selector.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRunning)
            return;

        if (other.gameObject.CompareTag("hand"))
        {
            animationNum = 1;
            StartCoroutine(ShrinkCoroutine());
        }
    }

    private IEnumerator ShrinkCoroutine()
    {
        isRunning = true;
        float time = 3.0f;
        float elapsedTime = 0.0f;
        Vector3 originScale = transform.localScale;

        while (elapsedTime < time)
        {
            float scale = 1.0f - elapsedTime / time;
            transform.localScale = originScale * scale;
            yield return new WaitForFixedUpdate();
            elapsedTime += Time.fixedDeltaTime;
        }

        foreach (var selector in selectors)
        {
            selector.gameObject.SetActive(true);
            selector.StartAnimation();
        }

        gameObject.SetActive(false);
    }
}
