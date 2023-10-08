using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grumpkin : MonoBehaviour
{
    
    
    [SerializeField]
    private int speed;
    [SerializeField]
    private int dashSpeed;

    [SerializeField]
    private float dashTimer = 0.2f;

    [SerializeField]
    private float shootTimer = 5;

    public Transform playerTransform;

    public GameObject bullet;
    public GameObject expandingCircle;


    private int attackMeter = 0;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private AudioSource audioSource;
    public AudioClip dashSound; //done
    public AudioClip shootSound; //done
    public AudioClip jumpUp; //done
    public AudioClip jumpDown; //done
   

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
        audioSource = GetComponent<AudioSource>();
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
        // Calculate the direction from this object to the player
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Calculate the desired velocity
        Vector2 desiredVelocity = directionToPlayer * speed;

        // Apply the velocity to the Rigidbody2D
        rb.velocity = desiredVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent playerController = collision.gameObject.GetComponent<HealthComponent>();
        if (playerController != null)
        {
            
            if (bossState == GrumpkinState.follow)
            {
                if (attackMeter == 0 || attackMeter == 1)
                {
                    rb.velocity = Vector3.zero;
                    bossState = GrumpkinState.attack;
                    animator.SetTrigger("dash");
                    audioSource.clip = dashSound; audioSource.Play();
                    attackMeter += 1;
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    bossState = GrumpkinState.jump;
                    animator.SetTrigger("jump");
                    audioSource.clip = jumpUp;
                    audioSource.Play();
                    attackMeter = 0;
                }
            }
        }
        
        
    }

    public void Attack()
    {
        //call this after telegraph frames

        Vector3 playerDir = (playerTransform.position - this.transform.position).normalized;
        
        StartCoroutine(Dash(playerDir));

        rb.velocity = Vector3.zero;
        bossState = GrumpkinState.follow;
        animator.SetTrigger("follow");


    }


    private IEnumerator Dash(Vector3 dir)
    {
        float startTime = Time.time;

        // Calculate the target velocity vector.
        Vector2 targetVelocity = dir * dashSpeed ;

        // Continue applying the velocity for the specified duration.
        while (Time.time < startTime + dashTimer)
        {
            rb.velocity = targetVelocity;
            yield return null;
        }

       rb.velocity = Vector3.zero;

    }
    public void Jump()
    {
        //call after telegraph frames
        audioSource.clip = jumpDown;
        audioSource.Play();
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
        audioSource.clip = shootSound; audioSource.Play();
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
            rb.velocity = Vector3.zero;
            animator.SetTrigger("spit");
        }
        else
        {
            StartCoroutine(ShootTimer());
        }
    }
}
