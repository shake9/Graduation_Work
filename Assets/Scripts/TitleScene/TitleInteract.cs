using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInteract : MonoBehaviour
{
    [SerializeField] private List<DifficultySelector> selectors;
    [SerializeField] public Animator animator;

    private bool isRunning = false;

    private void Awake()
    {
        foreach (var selector in selectors)
        {
            selector.gameObject.SetActive(false);
        }

        animator.SetInteger("selecting", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRunning)
            return;

        if (other.gameObject.CompareTag("hand"))
        {
            animator.SetInteger("selecting", 1);
            foreach (var selector in selectors)
            {
                selector.gameObject.SetActive(true);
            }
            //StartCoroutine(ShrinkCoroutine());
        }
    }

    private IEnumerator ShrinkCoroutine()
    {
        isRunning = true;
        Vector3 originScale = transform.localScale;
        Vector3 enlargeScale = originScale * 1.2f;

        float time = 0.5f;
        float elapsedTime = 0.0f;

        var rotater = GetComponent<RotateAnimator>();
        Vector3 originRotateSpeed = rotater.anglesPerSecond;

        while (elapsedTime < time)
        {
            float scale = elapsedTime / time;
            transform.localScale = enlargeScale * scale;
            yield return new WaitForFixedUpdate();
            elapsedTime += Time.fixedDeltaTime;
        }

        time = 1.0f;
        elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            float scale = 1.0f - elapsedTime / time;
            transform.localScale = enlargeScale * scale;
            rotater.anglesPerSecond = originRotateSpeed * (1.0f - scale);

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
