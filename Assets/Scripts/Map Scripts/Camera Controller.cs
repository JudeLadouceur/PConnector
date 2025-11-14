using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;      
    public float smoothSpeed = 0.125f; 
    public Vector3 offset;         

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned!");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}