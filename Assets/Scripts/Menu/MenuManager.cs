using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("LVL1-Porktown");
	}

	public void QuitGame()
	{
		Application.Quit();
	}


	public void LoadBossScene()
    {
		SceneManager.LoadScene("tempBossScene");
    }

	public void Awake()
    {
		Object.DontDestroyOnLoad(this);
    }
}
