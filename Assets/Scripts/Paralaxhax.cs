using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxhax : MonoBehaviour {


    GameObject player;
    public float divosor = 2f;
    

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = player.transform.position;
        Vector3 tmp = -pos / divosor;
        tmp.y = transform.position.y;
        transform.position = tmp;
	}
}
