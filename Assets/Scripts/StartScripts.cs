using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScripts : MonoBehaviour {


    public string levelToLoad;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.E))
        {

            Application.LoadLevel(levelToLoad);
        }
	}
}
