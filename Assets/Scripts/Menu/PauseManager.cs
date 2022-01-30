using Assets.Scripts.Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PauseManager : MonoBehaviour, IInitializable
{
    [SerializeField]
    GameObject[] SetActiveOnPause;
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    public void SetGameObjectsActive(bool active)
    {
        for (int i = 0; i < SetActiveOnPause.Length; i++)
        {
            SetActiveOnPause[i].SetActive(active);
        }
    }

    public void Initialize()
    {
        signalBus.Subscribe<GamePausedSignal>(() => SetGameObjectsActive(true));
        signalBus.Subscribe<GameUnPausedSignal>(() => SetGameObjectsActive(false));
    }
}
