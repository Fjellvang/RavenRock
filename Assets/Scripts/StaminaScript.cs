using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    private Slider slider;
    [Range(0.001f, 1f)]
    public float staminaIncreasePerSecond = 1;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < 1)
        {
            slider.value += staminaIncreasePerSecond * Time.deltaTime;
        }
    }

    public void ReduceStamina(float reduction) => slider.value -= reduction;
}
