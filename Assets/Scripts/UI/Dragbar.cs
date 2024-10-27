using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragbar : InputReviever, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _parent;
    private Transform _targetTransform;    
    private bool _dragging = false;
    private Vector2? _mouseOffset;

    private new void OnEnable()
    {
        base.OnEnable();
        
        if(_parent == null)
        {
            _targetTransform = transform;
        }
        else
        {
            _targetTransform = _parent.transform;
        }
    }

    protected override void ListenForInput()
    {
        _input.OnMenuMouseMove += dragByMouse;
    }

    protected override void UnlistenForInput()
    {
        _input.OnMenuMouseMove -= dragByMouse;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
        _mouseOffset = null;
    }

    private void dragByMouse(Vector2 mousePos)
    {
        if (!_dragging) return;

        Vector2 currentPosition = _targetTransform.position;

        //Get mouse offset if there is none already (first frame of current drag)
        if (!_mouseOffset.HasValue)
        {
            _mouseOffset = new Vector2(currentPosition.x - mousePos.x, currentPosition.y - mousePos.y);
        }
        
        //Create new position
        Vector2 newPosition = new Vector2(mousePos.x + _mouseOffset.Value.x, mousePos.y + _mouseOffset.Value.y);
        
        _targetTransform.position = newPosition;        
    }
}
