using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    
    private Vector2 _bound;

    [SerializeField] private bool _toRight;
    [SerializeField] private bool _isFixed;
    private bool _isStopped;
    
    private void Start()
    {
        _bound.x = transform.position.x - _distance;
        _bound.y = transform.position.x + _distance;
    }

    private void Update()
    {
        if (!_isStopped && !_isFixed)
        {
            if (_toRight)
            {
                MoveToRight();
            }
            else
            {
                MoveToLeft();
            }
        }
    }

    private void MoveToRight()
    {
        transform.position += Vector3.right * _speed * Time.deltaTime;
        if (transform.position.x >= _bound.y)
            _toRight = false;
    }
    
    private void MoveToLeft()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
        if (transform.position.x <= _bound.x)
            _toRight = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("White") && other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
        {
            Stop();
        }
    }

    private void Stop()
    {
        _isStopped = true;
        GetComponent<Rigidbody>().freezeRotation = false;
    }
}
