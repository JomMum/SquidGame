using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public int nextSceneNum;

    public bool isGameClear;

    void GoToNextScene()
    {
        if (isGameClear)
        {
            SceneManager.LoadScene("Game_" + nextSceneNum);
        }
        else
            SceneManager.LoadScene("MainMenu");
    }

    void ActiveFalseWindow()
    {
        gameObject.SetActive(false);
    }
}
