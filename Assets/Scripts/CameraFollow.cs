using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour {
	[Range(0,1)]
	public float incrementX = .1f;
    private Transform target;
	private Transform subject;

	private PauseManager pauseManager;

	[Inject]
	public void Construct(PauseManager pauseManager)
	{
		this.pauseManager = pauseManager; //TODO: Really the states need injectables so we can avoid this.
	}
    // Use this for initialization
    void Start()
    {
		if (target == null)
		{
			target = GameObject.FindWithTag("Player").transform;
		}
		subject = transform;
		UpdatePosition();
    }
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
        if (pauseManager.IsPaused)
        {
			return;
        }
		//The smoothing could just aswell be done on more axis, but kept to x for now
		var subX = subject.position.x;
		var x = subX + (target.position.x - subX) * incrementX;
		transform.position = new Vector3(x, subject.position.z, subject.position.z);
	}
}
