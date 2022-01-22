using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    private Slider slider;
    private PlayerController playerController; //TODO: maybe have a script only refencing modifiable values?
    public Image fillArea;
    private float originalGreen;
    //TODO: UNTANGLE THIS SHIT FROM UI SCRIPTS

    public delegate void OnExhaustedAction();
    public event OnExhaustedAction OnExhausted;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        var player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        originalGreen = fillArea.color.g;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < 1)
        {
            slider.value += playerController.staminaMultiplier * playerController.staminaIncreasePerSecond * Time.deltaTime;
            //Removing some of the green will make red dominant.
            fillArea.color = new Color(fillArea.color.r, originalGreen * slider.value, fillArea.color.b);
        }
    }

    public void ReduceStamina(float reduction)
    {
        slider.value -= reduction;
        if (slider.value <= 0)
        {
            this.OnExhausted?.Invoke();
        }
    }
}
