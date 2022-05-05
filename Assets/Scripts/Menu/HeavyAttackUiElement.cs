using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HeavyAttackUiElement : MonoBehaviour
{
    private PlayerCombatManager playerCombatManager;

    public GameObject attackReadyUi;
    public Image attackIcon;
    [Inject]
    public void Construct(PlayerCombatManager playerCombatManager)
    {
        this.playerCombatManager = playerCombatManager;
    }
    // Update is called once per frame
    void Update()
    {
        //TODO: Refactor this. Checking this every frame is a waaaste of resources.
        if (playerCombatManager.IsHeavyAttackAvailable)
        {
            attackReadyUi.SetActive(true);
        }
        else
        {
            attackReadyUi.SetActive(false);
        }
        // 0.2 the icon is fully dissolved
        // 0.1 the icon is fully visible
        var value = 0.1f + (0.1f * (1 - playerCombatManager.HeavyAttackReadyRange));
        attackIcon.material.SetFloat("_DissolveAmount", value);
    }
}
