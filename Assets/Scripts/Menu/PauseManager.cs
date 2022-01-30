using Assets.Scripts.GameInput;
using Assets.Scripts.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPauseManager
{
    void Register(Action<bool> onPause);

    void OnPauseChanged(bool paused);
}

public class PauseManager : IPauseManager, ITickable
{
    List<Action<bool>> actions = new List<Action<bool>>();
    private readonly InputState inputState;
    private bool paused = false;

    public PauseManager(InputState inputState, SignalBus bus)
    {
        bus.Subscribe<GamePausedSignal>(TogglePause);
        this.inputState = inputState;
    }

    public bool IsPaused => paused;

    public void OnPauseChanged(bool paused)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Invoke(paused);
        }
    }

    public void Register(Action<bool> onPause)
    {
        actions.Add(onPause ?? throw new NullReferenceException("Add a method please."));
    }

    public void Tick()
    {
        if (inputState.IsPressingPause)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        paused = !paused;
        OnPauseChanged(paused);
        if (paused)
        {
            Time.timeScale = 0f; //TODO: be more fancy than this...
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
