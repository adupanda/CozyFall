using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExpandingCircle : MonoBehaviour
{
    public float duration = 4f;        
    public float maxRadius = 15f;      
    private float currentRadius = 0f; 

    private CircleCollider2D circleCollider;

   

    public LineRenderer lineRenderer;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = currentRadius;

        lineRenderer= GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;  
        lineRenderer.endWidth = 0.2f;    
        lineRenderer.material.color = Color.black;

    }
    private void Start()
    {
        
        
        StartCoroutine(IncreaseRadiusOverTime());
    }

   

    private IEnumerator IncreaseRadiusOverTime()
    {
        float timer = 0f;

        while (timer < duration)
        {
            
            currentRadius = Mathf.Lerp(0f, maxRadius, timer / duration);

            
            circleCollider.radius = currentRadius;

            UpdateLineRenderer();
            timer += Time.deltaTime;

            yield return null;
        }

        
        Destroy(gameObject);
    }

    private void UpdateLineRenderer()
    {
        
        int numPoints = 40;
        lineRenderer.positionCount = numPoints +1;
        Vector3 centerPosition = transform.position;
        // Update the LineRenderer positions to form a circle.
        for (int i = 0; i <= numPoints; i++)
        {
            float angle = i * 2 * Mathf.PI / numPoints;
            float x = Mathf.Cos(angle) * currentRadius;
            float y = Mathf.Sin(angle) * currentRadius;
            Vector3 position = new Vector3(centerPosition.x + x,centerPosition.y + y, 0);
            lineRenderer.SetPosition(i, position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has a PlayerControllerTest component.
         
        HealthComponent component = collision.GetComponent<HealthComponent>();

        if (component != null)
        {
            

           
            
            component.TakeDamage(1);
            
        }
    }
}
