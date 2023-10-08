using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerState 
{
    protected Vector2 moveInput;


    protected Rigidbody2D rb;

    protected PlayerStateMachine stateMachine;

    protected Character character;

    private string animBoolName;

    protected float stateTimer;

    protected bool triggerCalled;
    public PlayerState(Character _character, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.character = _character;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        rb = character.rb;
        
        character.animator.SetBool(animBoolName, true);
        triggerCalled = false;

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
        if (moveInput.magnitude!=0)
        {
            character.lastMoveInput = moveInput;
        }
        character.animator.SetFloat("MoveX", moveInput.x);
        character.animator.SetFloat("MoveY", moveInput.y);
        

    }

    public virtual void Exit()
    {
        character.animator.SetBool(animBoolName, false);
    }

    public virtual void AnimationEndTrigger()
    {
        
        triggerCalled = true;
    }
}
