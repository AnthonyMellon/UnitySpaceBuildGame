using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionProbe : InputReviever
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private GameObject _toSpawn;
    private Quaternion _defaultRotation;

    private new void OnEnable()
    {
        base.OnEnable();

        UpdateToSpawn();
    }

    protected override void ListenForInput()
    {
        _input.OnPlayerInteract += Use;
    }

    protected override void UnlistenForInput()
    {
        _input.OnPlayerInteract -= Use;
    }

    private void UpdateToSpawn()
    {
        Mesh newMesh = _toSpawn.gameObject.GetComponent<MeshFilter>().sharedMesh;
        _meshFilter.sharedMesh = newMesh;
    }

    public void UpdateTarget(float distance, Quaternion rotation)
    {
        transform.localPosition = Vector3.forward * distance;
        transform.rotation = rotation;
    }

    private void Use()
    {        
        Transform spawned = Instantiate(_toSpawn, transform.position, transform.rotation).transform;
    }
}
