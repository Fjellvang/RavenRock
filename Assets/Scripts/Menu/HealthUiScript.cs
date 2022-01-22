using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUiScript : MonoBehaviour
{
    Health playerHealthScript;
    Slider healthSlider;
    float originalHealth;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerHealthScript = player.GetComponent<Health>();
        healthSlider = GetComponent<Slider>();
        originalHealth = playerHealthScript.health;
    }

    // Update is called once per frame
    void Update()
    {
        var value = playerHealthScript.health / originalHealth;
        healthSlider.value = value;
    }
}
