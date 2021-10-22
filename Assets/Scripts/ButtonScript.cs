using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameManager gameManager;

    public void GameStart()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameManager.isGameStart = true;
    }

    public void LoadGame1()
    {
        SceneManager.LoadScene("Game_1");
    }

    public void LoadGame2()
    {
        SceneManager.LoadScene("Game_2");
    }

    public void LoadGame3()
    {
        SceneManager.LoadScene("Game_3");
    }

    public void LoadGame5()
    {
        SceneManager.LoadScene("Game_5");
    }
}
