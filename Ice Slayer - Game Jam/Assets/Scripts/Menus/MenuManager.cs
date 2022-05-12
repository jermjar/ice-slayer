using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    public static MenuManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new MenuManager();
            }
            return instance;
        }
    }

    void Update()
    {
    }

    public void playGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
