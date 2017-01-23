using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 3;
    public GameObject monkBody;
    public GameObject monkHead;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Attack")
        {
            health--;
            if (health < 0)
            {
                Destroy(this.gameObject);
                if(monkBody)
                    Instantiate(monkBody, transform.position, transform.rotation);
                if(monkHead)
                    Instantiate(monkHead, transform.position += new Vector3(0, 1f, 0f), transform.rotation);
            }
        }
    }
}