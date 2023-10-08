using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispMovement : MonoBehaviour
{
    public GameObject pivot;

    public float distanceFromPivot;

    public float rotationSpeed;

    float ElapsedTime;

    public float minimumDistanceFromPivot;

    public float oscillationPeriod;

    public float oscillationAmplitude;

    

    
    void Update()
    {
        
        transform.RotateAround(pivot.transform.position, Vector3.forward, rotationSpeed*Time.deltaTime);
        transform.rotation = Quaternion.identity;

        
        ElapsedTime += Time.deltaTime;

        distanceFromPivot = minimumDistanceFromPivot + oscillationAmplitude* Mathf.Abs(Mathf.Sin(oscillationPeriod*ElapsedTime));

        


        Vector2 WispDirection = (this.transform.position - pivot.transform.position).normalized;
        this.transform.position = (Vector2)pivot.transform.position + WispDirection * distanceFromPivot;
        

    }
}
