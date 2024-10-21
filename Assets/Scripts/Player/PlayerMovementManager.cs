using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementManager : InputReviever
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _rotateSpeed;
    
    private Vector3 _moveDirection;
    private float _moveSpeed;
    
    #region Event Subscription
    protected override void ListenForInput()
    {
        _input.OnChangeDirection += ChangeMoveDirection;
        _input.OnJump += Jump;
        _input.OnHorizontalMouseMove += RotateHorizontal;
        _input.OnSprintToggle += ToggleSprint;
    }

    protected override void UnlistenForInput()
    {
        _input.OnChangeDirection -= ChangeMoveDirection;
        _input.OnJump -= Jump;
        _input.OnHorizontalMouseMove -= RotateHorizontal;
        _input.OnSprintToggle -= ToggleSprint;
    }
    #endregion

    protected new void OnEnable()
    {
        base.OnEnable();

        ToggleSprint(false);
    }

    private void ChangeMoveDirection(Vector3 newDirection)
    {
        _moveDirection = newDirection;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!OnGround()) return; //Don't try to move when in the air

        //_rb.AddRelativeForce(_moveDirection * _moveSpeed);
        Vector3 newVelocity = (transform.forward * _moveDirection.z) + (transform.right * _moveDirection.x);
        newVelocity *= _moveSpeed;

        _rb.velocity = new Vector3(newVelocity.x, _rb.velocity.y, newVelocity.z);
    }

    private void Jump()
    {
        if (!OnGround()) return; //Don't jump while already jumping

        _rb.AddForce(Vector3.up * _jumpForce);
    }

    private bool OnGround()
    {
        return true;
    }

    private void RotateHorizontal(float delta)
    {
        Rotate(new Vector3(0, delta, 0));
    }

    private void Rotate(Vector3 delta)
    {
        transform.Rotate(delta * _rotateSpeed);
    }

    private void ToggleSprint(bool toggle)
    {
        _moveSpeed = toggle ? _sprintSpeed : _walkSpeed;
    }
}
