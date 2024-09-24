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
            if (nextSceneNum == 5)
            {
                SceneManager.LoadScene("Result");
            }
            else
            {
                SceneManager.LoadScene("Game_" + nextSceneNum);
            }
        }
        else
        {
            ResultManager.Instance.tryCount++;
            SceneManager.LoadScene("Game_" + (nextSceneNum - 1));
        }
    }

    void ActiveFalseWindow()
    {
        gameObject.SetActive(false);
    }
}
