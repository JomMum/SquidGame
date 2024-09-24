using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameManager gameManager;

    public void SetCharacter(int index)
    {
        ResultManager.Instance.characterIndex = index;
        LoadGame1();
    }

    public void GameStart()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameManager.isGameStart = true;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
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

    public void LoadGame4()
    {
        SceneManager.LoadScene("Game_4");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
