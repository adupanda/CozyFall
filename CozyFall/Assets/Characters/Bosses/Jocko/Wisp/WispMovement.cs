using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispMovement : MonoBehaviour
{
    public GameObject pivot;

    public float distanceFromPivot;

    public float rotationSpeed;
    
    void Start()
    {
        Vector2 WispDirection = (this.transform.position - pivot.transform.position).normalized;
        this.transform.position = (Vector2)pivot.transform.position + WispDirection * distanceFromPivot;
    }

    
    void Update()
    {
        
        transform.RotateAround(pivot.transform.position, Vector3.forward, rotationSpeed*Time.deltaTime);
        transform.rotation = Quaternion.identity;

        
        
    }
}
