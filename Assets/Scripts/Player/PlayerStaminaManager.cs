using Assets.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerStaminaManager : ITickable
    {
        private readonly PlayerSettings playerSettings;
        private readonly PlayerStamina playerStamina;

        public delegate void OnExchaustedAction();
        public event OnExchaustedAction OnExhausted;

        public PlayerStaminaManager(PlayerSettings playerSettings, PlayerStamina playerStamina)
        {
            this.playerSettings = playerSettings;
            this.playerStamina = playerStamina;
        }
        public void Tick()
        {
            if (playerStamina.Stamina < 1f)
            {
                playerStamina.Stamina += playerSettings.StaminaMultiplier * playerSettings.StaminaIncreasePerSecond * Time.deltaTime;
            }
        }

        public void ReduceStamina(float reduction)
        {
            playerStamina.Stamina -= reduction;
            if (playerStamina.Stamina <= 0)
            {
                OnExhausted?.Invoke();
            }
        }
    }
}
