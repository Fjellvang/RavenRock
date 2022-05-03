namespace Assets.Scripts.Player
{
    public class PlayerCombatManager 
    {
        private readonly PlayerSettings playerSettings;
        private int successfullAttacks = 0;

        public PlayerCombatManager(PlayerSettings playerSettings)
        {
            this.playerSettings = playerSettings;
        }
        
        public void OnAttackHit()
        {
            successfullAttacks++;
        }

        public bool IsHeavyAttackAvailable => successfullAttacks >= playerSettings.SuccessfullAttacksBeforeSuperAttack;

        public void OnHeavyAttack() => successfullAttacks = 0;
    }
}
