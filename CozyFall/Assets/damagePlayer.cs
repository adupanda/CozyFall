using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent playerController = collision.gameObject.GetComponent<HealthComponent>();
        if(playerController)
        {
            playerController.TakeDamage(1);
            
        }
    }
}
