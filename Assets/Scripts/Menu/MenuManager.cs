using Assets.Scripts.GameInput;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuManager : MonoBehaviour
{
    private SignalBus signalBus;
	public void StartGame()
	{
		SceneManager.LoadScene("LVL1-Porktown");
	}
    [Inject]
    public void Construct(SignalBus bus)
    {
        this.signalBus = bus;
    }

    public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadBossScene()
    {
		SceneManager.LoadScene("tempBossScene");
    }

    public void PauseGame()
    {
        signalBus.Fire<GamePausedSignal>();
    }

}
