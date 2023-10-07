using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingCircle : MonoBehaviour
{
    public float duration = 4f;        // Duration 
    public float maxRadius = 15f;      // The maximum radius 
    private float currentRadius = 0f; // The initial radius.

    private CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = currentRadius;
        
        StartCoroutine(IncreaseRadiusOverTime());
    }

    private IEnumerator IncreaseRadiusOverTime()
    {
        float timer = 0f;

        while (timer < duration)
        {
            
            currentRadius = Mathf.Lerp(0f, maxRadius, timer / duration);

            
            circleCollider.radius = currentRadius;

            
            timer += Time.deltaTime;

            yield return null;
        }

        // The effect is complete, destroy the GameObject.
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has a PlayerControllerTest component.
         PlayerControllerTest playerController  = collision.gameObject.GetComponent<PlayerControllerTest>();

        if (playerController != null)
        {
            // Access the player's current state.
            PlayerControllerTest.CharacterState playerState = playerController.currentState;

            // Check the player's state and perform actions accordingly.
            if (playerState == PlayerControllerTest.CharacterState.Dashing)
            {
                // Player is in the "Dashing" state, so do something specific.
                Debug.Log("Player is dodging.");
            }
            else
            {
                // Player is not in the "Dashing" state, so do something else.
                Debug.Log("Player is not dodging.");
            }
        }
    }
}
