using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float moveSpeed;

    public float dashSpeed;

    public float dashDuration;

    public Vector2 dashDirection;

    public Vector2 lastMoveInput;

    public Transform attackCheck;

    public float attackCheckRadius;
    public Animator animator { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public PlayerStateMachine stateMachine { get; private set; }


    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerBasicAttackState basicAttackState { get; private set; }

    

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");

        moveState = new PlayerMoveState(this, stateMachine, "Move");

        dashState = new PlayerDashState(this, stateMachine, "Dash");

        basicAttackState = new PlayerBasicAttackState(this, stateMachine, "Attack");


    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();

        animator.SetFloat("LastMoveX", lastMoveInput.x);
        animator.SetFloat("LastMoveY",lastMoveInput.y);

    }

    public void SetVelocity(float _xvelocity, float _yvelocity)
    {
        rb.velocity = new Vector2(_xvelocity, _yvelocity);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationEndTrigger();
}
