using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class GameEvent : ScriptableObject
    {
        List<GameEventListener> listeners = new List<GameEventListener>(); // Consider changing list if many listeners?

        public void Raise()
        {
            for (int i = listeners.Count-1; i >= 0; i--) // loop backwards
            {
                listeners[i].OnEventRaised();
            }
        }

        public void Register(GameEventListener listene) => listeners.Add(listene);
        public void UnRegister(GameEventListener listene) => listeners.Remove(listene);
    }
}
