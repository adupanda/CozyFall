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
        attack,
        jump,
        shoot
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

}
