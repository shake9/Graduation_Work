using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{

	[SerializeField]
    string targetSceneName = "HandTest";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		 if (Input.GetMouseButton(0))
        {
            LoadScene();
        }
    }

	void LoadScene()
	{
		//SceneManager.LoadScene(targetSceneName);
		FadeManager.Instance.LoadScene (targetSceneName, 2.0f);
	}
}
