using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jocko : MonoBehaviour
{
    public float health = 100f;
    public GameObject player;

    private void Start()
    {
        Transform Wisp = transform.Find("Wisp");
        Wisp.GetComponent<SpriteRenderer>().enabled = true;
        Wisp.GetComponent<CircleCollider2D>().enabled = true;
    }


    void Update()
    {
        if(health <= 75)
        {
            Transform Wisp = transform.Find("Wisp1");
            Wisp.GetComponent<SpriteRenderer>().enabled = true;
            Wisp.GetComponent<CircleCollider2D>().enabled = true;

            
        }
        if(health <= 50)
        {
            Transform Wisp = transform.Find("Wisp2");
            Wisp.GetComponent<SpriteRenderer>().enabled = true;
            Wisp.GetComponent<CircleCollider2D>().enabled = true;
        }
        if(health <= 25)
        {
            Transform Wisp = transform.Find("Wisp3");
            Wisp.GetComponent<SpriteRenderer>().enabled = true;
            Wisp.GetComponent<CircleCollider2D>().enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            health -= 5;
        }
    }
}
