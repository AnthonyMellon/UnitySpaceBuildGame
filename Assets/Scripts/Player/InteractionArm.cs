using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class InteractionArm : InputReviever
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _scrollSpeed;
    private float _targetDistance;
    [SerializeField] private InteractionProbe _probe;

    protected override void ListenForInput()
    {
        _input.OnPlayerScrollInteractionDistance += ScrollTargetDistance;
    }

    protected override void UnlistenForInput()
    {
        _input.OnPlayerScrollInteractionDistance -= ScrollTargetDistance;
    }

    private void Awake()
    {
        _targetDistance = _maxDistance;
    }

    private void Update()
    {
        GetTarget();        
    }

    private void GetTarget()
    {
        RaycastHit hit;

        float distance = _targetDistance;
        Quaternion rotation = Quaternion.identity;

        if(Physics.Raycast(transform.position, transform.forward, out hit, _targetDistance))
        {            
            distance = hit.distance;
            rotation = hit.transform.rotation;
        }

        distance = Mathf.Clamp(distance, _minDistance, _targetDistance);

        _probe.UpdateTarget(distance, rotation);
    }

    private void ScrollTargetDistance(float direction)
    {
        _targetDistance += (direction * _scrollSpeed);
        _targetDistance = Mathf.Clamp(_targetDistance, _minDistance, _maxDistance);
    }
}
