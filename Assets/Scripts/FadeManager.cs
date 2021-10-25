using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public int curSceneNum;
    public int nextSceneNum;

    public bool isGameClear;

    void GoToNextScene()
    {
        if (isGameClear)
        {
            if (nextSceneNum == 6)
                SceneManager.LoadScene("ResultScene");
            else
                SceneManager.LoadScene("Game_" + nextSceneNum);
        }
        else
            SceneManager.LoadScene("Game_" + curSceneNum);
    }

    void ActiveFalseWindow()
    {
        gameObject.SetActive(false);
    }
}
