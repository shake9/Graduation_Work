using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialPrefabs = new List<GameObject>();
    [SerializeField] private string nextSceneName;

    private Queue<GameObject> tutorialQueue = new Queue<GameObject>();

    private GameObject currentTutorialObject;
    private ITutorial currentTutorial;

    private void Start()
    {
        foreach (var tutorial in tutorialPrefabs)
        {
            tutorialQueue.Enqueue(tutorial);
        }

        ShowNextTutorial();
    }

    private void Update()
    {
        if (currentTutorial.IsClear())
        {
            ShowNextTutorial();
        }
    }

    private void ShowNextTutorial()
    {
        // 前のチュートリアルを削除
        if (currentTutorialObject != null)
        {
            Destroy(currentTutorialObject);
        }

        // チュートリアルが残っていないならreturn
        if (tutorialQueue.Count == 0)
        {
            Debug.Log("チュートリアル終了");
            FindObjectOfType<FadeManager>().LoadScene(nextSceneName, 0.5f);
            return;
        }

        // 次のチュートリアルを生成
        currentTutorialObject = Instantiate(tutorialQueue.Dequeue(), transform);
        currentTutorial = currentTutorialObject.GetComponent<ITutorial>();
    }
}
