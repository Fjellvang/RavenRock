using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    SpriteRenderer sprite;
    public int numSprites = 4;
    public Sprite[] listOfSprites = new Sprite[5];

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPicture(int x)
    {
        sprite.sprite = listOfSprites[x];
    }
}
