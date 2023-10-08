using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jocko : MonoBehaviour
{
    
    public GameObject player;
    
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(player.transform.position.x<this.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX=false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthComponent>().TakeDamage(1);
        }
    }
}
