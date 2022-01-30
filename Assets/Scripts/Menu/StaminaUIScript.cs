using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Menu
{
    public class StaminaUIScript : MonoBehaviour
    {
        private Slider slider;
        public Image fillArea;
        private float originalGreen;
        private PlayerStamina playerStamina;

        // Start is called before the first frame update
        void Start()
        {
            slider = GetComponent<Slider>();
            originalGreen = fillArea.color.g;
        }

        [Inject]
        void Construct(PlayerStamina playerStamina)
        {
            this.playerStamina = playerStamina;
        }

        // Update is called once per frame
        void Update()
        {
            //Removing some of the green will make red dominant.
            fillArea.color = new Color(fillArea.color.r, originalGreen * playerStamina.Stamina, fillArea.color.b);
            slider.value = playerStamina.Stamina;
        }
    }
}
