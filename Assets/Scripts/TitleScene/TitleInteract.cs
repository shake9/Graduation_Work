using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInteract : MonoBehaviour
{
    [SerializeField] private List<Transform> selectors;

    private void Awake()
    {
        foreach (var selector in selectors)
        {
            selector.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            foreach (var selector in selectors)
            {
                selector.gameObject.SetActive(true);
            }
        }
    }
}
