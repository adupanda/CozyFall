using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{

    
    private Vector2 dashMoveInput;

    public PlayerDashState(Character _character, PlayerStateMachine _stateMachine, string _animBoolName) : base(_character, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.GetComponent<HealthComponent>().isImmune = true;
        stateTimer = character.dashDuration;
        dashMoveInput = character.lastMoveInput;
        
    }

    public override void Exit()
    {
        base.Exit();
        character.GetComponent<HealthComponent>().isImmune = false;
        character.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        character.rb.velocity = character.dashSpeed * dashMoveInput;
        if(stateTimer<0)
        {
            stateMachine.ChangeState(character.idleState);
        }
    }
}
