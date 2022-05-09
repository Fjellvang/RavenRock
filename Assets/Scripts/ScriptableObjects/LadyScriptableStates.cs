using UnityEngine;

[CreateAssetMenu(fileName = "LadyData", menuName = "ScriptableObjects/LadyScriptableObjects", order = 1)]
public class LadyScriptableStates : ScriptableObject
{
    [SerializeField]
    private AnimatorOverrideController[] OverrideControllers;

    public AnimatorOverrideController GetRandomController() => OverrideControllers[Random.Range(0, OverrideControllers.Length)];

}
