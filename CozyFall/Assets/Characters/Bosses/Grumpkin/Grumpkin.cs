using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grumpkin : MonoBehaviour
{
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private int health;
    [SerializeField]
    private int speed;

    public Transform playerTransform;


    

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private enum GrumpkinState
    {
        follow,
        attack, //very small dash in the direction of the player
        jump, // up and down ground slam which does some aoe damage 
        shoot // small charge and shoot a bullet in players direction 

            //start in follow and if player gets close enough do either a dash or a jump attack randomly
            // if player has not come close 
    }

    private GrumpkinState bossState = GrumpkinState.follow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (bossState)
        {
            case GrumpkinState.follow:

                Follow();
                break;

            case GrumpkinState.attack:

                //animator.SetBool("IsMoving", true);
                break;

            case GrumpkinState.jump:

                //animator.SetTrigger("Dash");
                break;

            case GrumpkinState.shoot:

                // animator.SetTrigger("Attack");
                break;
        }

        
    }


    private void Follow()
    {
        this.transform.Translate((playerTransform.position - this.transform.position).normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
