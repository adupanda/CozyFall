using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    

    public PlayerMoveState(Character _character, PlayerStateMachine _stateMachine, string _animBoolName) : base(_character, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
        
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        character.SetVelocity(moveInput.x * character.moveSpeed, moveInput.y*character.moveSpeed);
        
        
        if(moveInput.magnitude == 0 )
        {
            stateMachine.ChangeState(character.idleState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(character.dashState);
        }
        if(Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(character.basicAttackState);
        }
    }
}
