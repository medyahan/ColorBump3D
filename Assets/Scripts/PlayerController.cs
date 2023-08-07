using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("RIGIDBODY")]
    [SerializeField] private Rigidbody _rb;
    
    [Header("MESH")]
    [SerializeField] private MeshRenderer _mesh;
    
    [Header("COLLIDER")]
    [SerializeField] private Collider _collider;
    
    [Header("MOVEMENT")]
    [SerializeField] private float _movementSensivity;
    [SerializeField] private float _movementClampForce;
    [SerializeField] private float _movementBounds;
    
    private Vector3 _lastMousePos;

    void Update()
    {
        if(!GameManager.IsGameStarted)
            return;
        
        ClampPosition();
        transform.position += GameManager.Instance.CameraControl.CameraVelocity;
    }
    
    void FixedUpdate()
    {
        if(!GameManager.IsGameStarted)
            return;
        
        if(Input.GetMouseButtonDown(0))
        {
            _lastMousePos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            Move();
        }

        _rb.velocity.Normalize();
    }

    private void Move()
    {
        Vector3 vec = _lastMousePos - Input.mousePosition;
        _lastMousePos = Input.mousePosition;
        
        vec = new Vector3(vec.x, 0, vec.y);

        Vector3 moveForce = Vector3.ClampMagnitude(vec, _movementClampForce);

        _rb.AddForce((-moveForce * _movementSensivity - _rb.velocity / 5f), ForceMode.VelocityChange);

    }
    
    private void ClampPosition()
    {
        var currentPosition = transform.position;
        currentPosition = new Vector3(Mathf.Clamp(currentPosition.x, -_movementBounds, _movementBounds), currentPosition.y, currentPosition.z);
        transform.position = currentPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnFailed();
            GameManager.Instance.OnLevelFailed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.OnLevelCompleted();
        }
    }

    private void OnFailed()
    {
        _mesh.enabled = false;
        _collider.enabled = false;
    }
}
