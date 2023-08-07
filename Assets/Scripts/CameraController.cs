using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    private Vector3 _cameraVelocity;
    public Vector3 CameraVelocity
    {
        get => _cameraVelocity;
        set => _cameraVelocity = value;
    }
    private void Update()
    {
        if(!GameManager.IsGameStarted)
            return;
        
        var velocity = Vector3.forward * _cameraSpeed * Time.deltaTime;
        transform.position += velocity;
        
        SetCameraVelocity(velocity);
        
    }

    private void SetCameraVelocity(Vector3 velocity)
    {
        _cameraVelocity = velocity;
    }
    
    
}
