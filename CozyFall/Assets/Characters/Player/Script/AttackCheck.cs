using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    private Character character => GetComponent<Character>();

    public int Damage;

    public List<GameObject> hitObjectsList;
    private void AttackTrigger( )
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(character.attackCheck.position, character.attackCheckRadius);

        foreach (var hit in colliders)
        {


            
            if (hit.CompareTag("Enemy"))
            {
                if(hitObjectsList.Contains(hit.gameObject))
                {
                    hit.GetComponent<HealthComponent>().TakeDamage(Damage);
                    hitObjectsList.Add(hit.gameObject);
                }
                
            }
           

        }


    }
}
