using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.ScriptableObjects
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;

        private void OnEnable()
        {
            gameEvent.Register(this);
        }

        private void OnDisable()
        {
            gameEvent.UnRegister(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
}