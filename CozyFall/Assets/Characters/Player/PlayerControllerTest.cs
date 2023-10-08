using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
    private Vector3 moveDir;
    private Vector3 lastKnownMoveDir;

    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float attackCooldown = 0.5f;

    private bool isDashing = false;
    private bool canAttack = true;

    private Animator animator;
    private SpriteRenderer sr;
    public enum CharacterState
    {
        Idle,
        Moving,
        Dashing,
        Attacking
    }

    public CharacterState currentState = CharacterState.Idle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        switch (currentState)
        {
            case CharacterState.Idle:
                
                //animator.SetBool("IsMoving", false);
                break;

            case CharacterState.Moving:
                
                //animator.SetBool("IsMoving", true);
                break;

            case CharacterState.Dashing:
               
                //animator.SetTrigger("Dash");
                break;

            case CharacterState.Attacking:
                
               // animator.SetTrigger("Attack");
                break;
        }

        if(currentState != CharacterState.Dashing && currentState != CharacterState.Attacking)
        {
            float X = 0f;
            float Y = 0f;

            if (Input.GetKey(KeyCode.W))
            {
                Y = +1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Y = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                X = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                X = +1f;
            }
            moveDir = new Vector3(X, Y).normalized;
            
            if (moveDir.magnitude > 0)
            {
                // Update the last known move direction when moving
                lastKnownMoveDir = moveDir;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
            {
                StartCoroutine(Dash());
            }

            // Handle attacking
            if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
            {
                StartCoroutine(Attack());
            }


            if (moveDir.magnitude < 0)
            {
                currentState = CharacterState.Idle;
                rb.velocity = Vector3.zero;
            }
            
            
        }
        

    }

    private void FixedUpdate()
    {
        if(currentState == CharacterState.Moving)
        {
            rb.velocity = moveDir * speed * Time.deltaTime;
        }
        

    }

    private IEnumerator Dash()
    {
        currentState = CharacterState.Dashing;
        isDashing = true;
        GetComponent<HealthComponent>().isImmune = true;

        Vector3 dashDirection = moveDir.magnitude > 0 ? moveDir.normalized : lastKnownMoveDir.normalized;

        float startTime = Time.time;

        // Calculate the target velocity vector.
        Vector2 targetVelocity = dashDirection * dashSpeed;

        // Continue applying the velocity for the specified duration.
        while (Time.time < startTime + dashDuration)
        {
            rb.velocity = targetVelocity;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;

        GetComponent<HealthComponent>().isImmune = false;
        currentState = CharacterState.Idle;
    }

    private IEnumerator Attack()
    {
        currentState = CharacterState.Attacking;
        // Perform the attack logic here
        Debug.Log("Attack!");
        canAttack = false;

        // Wait for the attack cooldown
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        currentState = CharacterState.Idle;
    }

}

