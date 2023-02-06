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
        // �O�̃`���[�g���A�����폜
        if (currentTutorialObject != null)
        {
            Destroy(currentTutorialObject);
        }

        if (isend && !ischeck)
        {
            FindObjectOfType<FadeManager>().LoadScene(nextSceneName, 0.5f);
            ischeck = true;
        }

        // �`���[�g���A�����c���Ă��Ȃ��Ȃ�return
        if (tutorialQueue.Count == 0)
        {
            isend = true;
            Debug.Log("�`���[�g���A���I��");
            //FindObjectOfType<FadeManager>().LoadScene(nextSceneName, 0.5f);
            return;
        }

       
        

        // ���̃`���[�g���A���𐶐�
        currentTutorialObject = Instantiate(tutorialQueue.Dequeue(), transform);
        currentTutorial = currentTutorialObject.GetComponent<ITutorial>();
    }
}
