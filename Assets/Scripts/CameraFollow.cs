using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform plane;
    [SerializeField] Vector3 cameraOffset;


    private void Update()
    {
        transform.position = plane.position + cameraOffset;
    }
}
