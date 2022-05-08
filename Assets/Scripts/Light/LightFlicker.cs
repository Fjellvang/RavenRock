using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public float minIntensity = 2f;
    public float maxIntensity = 4f;

    public float maxDuration = 0.2f;
    public Light2D lightTwoD;
    // Start is called before the first frame update
    void Start()
    {
        lightTwoD = GetComponent<Light2D>();
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            lightTwoD.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(0, maxDuration));
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Flicker());
    }

    private void OnDisable()
    {
        StopCoroutine(Flicker());
    }
}
