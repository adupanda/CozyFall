using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;




    [SerializeField]
    private float lifetime = 7f;

    public Vector3 shootDir;
    public void Fire()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {

        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthComponent component = collision.GetComponent<HealthComponent>();
            component.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
