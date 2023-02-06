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
    private bool isend = false;
    private bool ischeck = false;

    private void Start()
    {
        isend = false;
        ischeck = false;
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

        if (isend && !ischeck)
        {
            FindObjectOfType<FadeManager>().LoadScene(nextSceneName, 0.5f);
            ischeck = true;
        }

        // チュートリアルが残っていないならreturn
        if (tutorialQueue.Count == 0)
        {
            isend = true;
            Debug.Log("チュートリアル終了");
            //FindObjectOfType<FadeManager>().LoadScene(nextSceneName, 0.5f);
            return;
        }

       
        

        // 次のチュートリアルを生成
        currentTutorialObject = Instantiate(tutorialQueue.Dequeue(), transform);
        currentTutorial = currentTutorialObject.GetComponent<ITutorial>();
    }
}
