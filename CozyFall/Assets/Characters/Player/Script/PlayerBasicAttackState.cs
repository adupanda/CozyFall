using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : PlayerState
{
    




    public PlayerBasicAttackState(Character _character, PlayerStateMachine _stateMachine, string _animBoolName) : base(_character, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();     
    }

   

    public override void Exit()
    {
        character.GetComponentInParent<AttackCheck>().hitObjectsList.Clear();
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled)
        {
            stateMachine.ChangeState(character.idleState);
        }
        
    }
}
