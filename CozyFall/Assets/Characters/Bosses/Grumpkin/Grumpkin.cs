using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grumpkin : MonoBehaviour
{
    
    [SerializeField]
    private int health;
    [SerializeField]
    private int speed;
    [SerializeField]
    private int dashSpeed;

    [SerializeField]
    private float dashTimer = 2;

    [SerializeField]
    private float shootTimer = 10;

    public Transform playerTransform;

    public GameObject bullet;
    public GameObject expandingCircle;


    private int attackMeter = 0;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private enum GrumpkinState
    {
        follow,
        attack, //very small dash in the direction of the player
        jump, // up and down ground slam which causes a ring to spawn in the center and rapidly increase in size. 
        shoot // small charge and shoot a bullet in players direction 

            //start in follow and if player gets close enough do either a dash or a jump attack randomly
            // if player has not come close 
    }

    private GrumpkinState bossState = GrumpkinState.follow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(ShootTimer());
    }

    private void Update()
    {
        
        if(playerTransform.position.x < this.transform.position.x) 
        { 
            sr.flipX= true;
        }
        else
        {
            sr.flipX= false;
        }
        
        if(bossState== GrumpkinState.follow) 
        { 
            Follow();
        }
    }


    private void Follow()
    {
        this.transform.Translate((playerTransform.position - this.transform.position).normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bossState == GrumpkinState.follow)
        {
            if (attackMeter == 0 || attackMeter == 1)
            {
                bossState = GrumpkinState.attack;
                animator.SetTrigger("dash");
                attackMeter += 1;
            }
            else
            {
                bossState = GrumpkinState.jump;
                animator.SetTrigger("jump");
                attackMeter = 0;
            }
        }
        
    }

    public void Attack()
    {
        //call this after telegraph frames

        Vector3 playerDir = (playerTransform.position - this.transform.position).normalized;
        StartCoroutine(Dash(playerDir));

        bossState = GrumpkinState.follow;
        animator.SetTrigger("follow");


    }


    private IEnumerator Dash(Vector3 dir)
    {
        while(dashTimer > 0)
        {
            this.transform.Translate(dir * dashSpeed * Time.deltaTime);

            dashTimer -= Time.deltaTime;
            yield return null;
        }
    }
    public void Jump()
    {
        //call after telegraph frames

        GameObject circleRef = Instantiate(expandingCircle, this.transform);
        //spawn ring at center (could use a circle collider and just increase it in size across the screen, if on trigger enter2d player is dashing dont do damge, otherwise do damge)
        //go back to follow

        bossState = GrumpkinState.follow;
        animator.SetTrigger("follow");
        //create a gameobject with a circle collider which runs a coroutine with a while loop, in which the radius of the colider is increased per frame 
    }

    public void Shoot()
    {


        //call after telegraph frames
        Vector3 playerDir = (playerTransform.position - this.transform.position).normalized;

        GameObject bulletRef = Instantiate(bullet, this.transform);
        bulletRef.GetComponent<Bullet>().shootDir = playerDir;
        bulletRef.GetComponent<Bullet>().Fire();



        bossState = GrumpkinState.follow;
        animator.SetTrigger("follow");

        StartCoroutine(ShootTimer());

    }

    private IEnumerator ShootTimer()
    {
        float timer = shootTimer;
        while (timer > 0)
        {
            

            timer -= Time.deltaTime;
            yield return null;
        }

        if(bossState == GrumpkinState.follow) 
        {
            bossState = GrumpkinState.shoot;
            animator.SetTrigger("spit");
        }
        else
        {
            StartCoroutine(ShootTimer());
        }
    }
}
