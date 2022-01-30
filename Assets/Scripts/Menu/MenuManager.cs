using Assets.Scripts.GameInput;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuManager : MonoBehaviour
{
    private InputState inputState;
    private SignalBus signalBus;
	public void StartGame()
	{
		SceneManager.LoadScene("LVL1-Porktown");
	}
    [Inject]
    public void Construct(SignalBus bus, InputState inputState)
    {
        this.inputState = inputState;
        signalBus = bus;
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

    public static bool gameIsPaused;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (inputState.IsPressingPause)//TODO: GET FROM INPUT MANAGER.
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            signalBus.Fire<GamePausedSignal>();
            Time.timeScale = 0f; //TODO: manager for this? It has nothing to do with menu..
        }
        else
        {
            signalBus.Fire<GameUnPausedSignal>();
            Time.timeScale = 1;
        }
    }

}
