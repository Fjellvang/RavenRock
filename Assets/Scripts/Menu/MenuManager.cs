using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public GameObject menu;
	public void StartGame()
	{
		SceneManager.LoadScene("LVL1-Porktown");
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

	private bool IsGameScene(int sceneIndex) => sceneIndex == 1 || sceneIndex == 4; //TODO: find something cleaner

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (IsGameScene(arg0.buildIndex))
        {
            Debug.Log("Loaded game scene");
            SetGameUIActive(true, "UI");
            return;
        }
        Debug.Log("Loaded none game scene");
        SetGameUIActive(false, "UI");
    }

    private void SetGameUIActive(bool active, string tag)
    {
        var childUi = menu.GetComponentsInChildren<RectTransform>(includeInactive: true).Where(x => x.tag == tag).ToArray();
#if DEBUG
        if (!childUi.Any())
        {
            Debug.Log("Found no obects with tag" + tag + "in " + menu.name);
        }
#endif
        foreach (var item in childUi)
        {
            item.gameObject.SetActive(active);
        }
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
		DontDestroyOnLoad(menu);
    }

    public static bool gameIsPaused;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            SetGameUIActive(true, "PauseMenu");
        }
        else
        {
            Time.timeScale = 1;
            SetGameUIActive(false, "PauseMenu");
        }
    }

}
