using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float dampTime = 0.15f;
 
    public GameObject target;
    float origY;
    // Use this for initialization
    void Start()
    {
        origY = transform.position.y;
        target = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = target.transform.position;
        pos.y = origY;
        pos.z = -10;
        transform.position = pos; 
    }
}
