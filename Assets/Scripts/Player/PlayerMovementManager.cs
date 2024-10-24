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
        _input.OnMove += ChangeMoveDirection;
        _input.OnJump += Jump;
        _input.OnHorizontalMouseMove += RotateHorizontal;
        _input.OnSprintToggle += ToggleSprint;
    }

    protected override void UnlistenForInput()
    {
        _input.OnMove -= ChangeMoveDirection;
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

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Update move values
    /// </summary>
    /// <param name="newDirection">new values</param>
    private void ChangeMoveDirection(Vector3 newDirection)
    {
        _moveDirection = newDirection;
    }

    /// <summary>
    /// Move the player based off _moveDirection
    /// </summary>
    private void Move()
    {
        if (!OnGround()) return; //Don't try to move when in the air
        
        //Calculate new velocity
        Vector3 newVelocity = (transform.forward * _moveDirection.z) + (transform.right * _moveDirection.x);
        newVelocity *= _moveSpeed;

        //Apply new veclocity
        _rb.velocity = new Vector3(newVelocity.x, _rb.velocity.y, newVelocity.z);
    }

    private void Jump()
    {
        if (!OnGround()) return; //Don't jump while already jumping

        _rb.AddForce(Vector3.up * _jumpForce);
    }

    /// <summary>
    /// Check if the player is grounded
    /// </summary>
    /// <returns>True if the player is on the ground</returns>
    private bool OnGround()
    {
        return true;
    }

    /// <summary>
    /// Spin the player around the y axis
    /// </summary>
    /// <param name="delta">amount to rotate by</param>
    private void RotateHorizontal(float delta)
    {
        Rotate(new Vector3(0, delta, 0));
    }

    /// <summary>
    /// Rotate the player around all axis
    /// </summary>
    /// <param name="delta">Amount to rotate by</param>
    private void Rotate(Vector3 delta)
    {
        transform.Rotate(delta * _rotateSpeed);
    }

    /// <summary>
    /// Toggle weather or not the player is sprinting
    /// </summary>
    /// <param name="toggle"></param>
    private void ToggleSprint(bool toggle)
    {
        _moveSpeed = toggle ? _sprintSpeed : _walkSpeed;
    }
}
