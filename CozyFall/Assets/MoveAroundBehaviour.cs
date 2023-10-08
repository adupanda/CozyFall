using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAroundBehaviour : StateMachineBehaviour
{
    private GameObject[] patrolPoints;

    int randomPoint;
    public float speed;

    public float switchToLaserTimer; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");
        randomPoint = Random.Range(0, patrolPoints.Length);

    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPoint].transform.position, speed*Time.deltaTime);

        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position)<0.1f)
        {
            randomPoint = Random.Range(0, patrolPoints.Length);
        }

        switchToLaserTimer -= Time.deltaTime;
        if(switchToLaserTimer <= 0) 
        {
            animator.SetTrigger("ShootLaser");
            switchToLaserTimer = 0;
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switchToLaserTimer = 5f;
        animator.ResetTrigger("ShootLaser");
    }

    


}
