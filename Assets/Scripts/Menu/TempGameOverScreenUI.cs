using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempGameOverScreenUI : MonoBehaviour
{
    public void OnPressRetry()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
