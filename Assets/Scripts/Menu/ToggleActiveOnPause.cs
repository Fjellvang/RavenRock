using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ToggleActiveOnPause : MonoBehaviour
{
    private IPauseManager pauseManager;
    [SerializeField]
    GameObject[] objectsToToggleActive;

    [Inject]
    public void Construct(IPauseManager pauseManager)
    {
        this.pauseManager = pauseManager;
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseManager.Register(paused =>
        {
            for (int i = 0; i < objectsToToggleActive.Length; i++)
            {
                objectsToToggleActive[i].SetActive(paused);
            }
            return;
        });
    }
    
}
