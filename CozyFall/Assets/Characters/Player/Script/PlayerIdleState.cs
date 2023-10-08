using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Character _character, PlayerStateMachine _stateMachine, string _animBoolName) : base(_character, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

       
        if(moveInput.magnitude != 0)
        {
            stateMachine.ChangeState(character.moveState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(character.dashState);
        }
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(character.basicAttackState);
        }
    }
}
