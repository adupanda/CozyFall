using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserBehaviour : StateMachineBehaviour
{
    //Stop Jocko from moving
    //Flip Sprite in player Direction
    //Begin Line trace
    //Line Renderer visibility with low alphha that increases over time
    //At full alpha check if player is being hit by line trace and deal damage.

    private Vector3 playerPosition;

    public float laserDamageTimer = 0f;

    LineRenderer lineRenderer;

    public Color lineColor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = animator.GetComponentInParent<Jocko>().player.transform.position;

        if (playerPosition.x < animator.transform.position.x)
        {
            animator.GetComponentInParent<SpriteRenderer>().flipX = true;
            
        }

        
        lineRenderer = animator.GetComponentInParent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, animator.transform.position);
        lineRenderer.SetPosition(1, playerPosition);
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.SetColors(lineColor, lineColor);

        
    }
     
   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        float newAlpha = Mathf.Lerp(0, 1, laserDamageTimer);

        
        Color newLineColor = new Color(lineColor.r, lineColor.g, lineColor.b, newAlpha);

        
        lineRenderer.material.color = newLineColor;

        laserDamageTimer += Time.deltaTime;
        if(laserDamageTimer >= 2f)
        {
            RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, (playerPosition - animator.transform.position).normalized, 100f);
            if(hit.transform.gameObject.CompareTag("Player"))
            {
                hit.transform.gameObject.GetComponent<HealthComponent>().TakeDamage(5);
            }

            animator.SetTrigger("Move");
        }
       
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        laserDamageTimer = 0;
    }


    
}
