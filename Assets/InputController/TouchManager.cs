using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    public float ZoomMin = 100.0f;
    public float ZoomMax = 1500.0f;
    public float ZoomSpeed = 1.0f;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (EnhancedTouch.Touch.activeTouches.Count == 0)
            return;

        var prTouch = EnhancedTouch.Touch.activeTouches[0];
        var prTouchCurrentWorldPosition = _mainCamera.ScreenToWorldPoint(prTouch.screenPosition);
        var prTouchPreviousWorldPosition = _mainCamera.ScreenToWorldPoint(prTouch.screenPosition - prTouch.delta);

        if (EnhancedTouch.Touch.activeTouches.Count == 2)
        {
            var scTouch = EnhancedTouch.Touch.activeTouches[1];
            var scTouchCurrentWorldPosition = _mainCamera.ScreenToWorldPoint(scTouch.screenPosition);
            var scTouchPreviousWorldPosition = _mainCamera.ScreenToWorldPoint(scTouch.screenPosition - scTouch.delta);

            var currentDistanceBtwTouches = Vector3.Distance(prTouchCurrentWorldPosition, scTouchCurrentWorldPosition);
            var previousDistanceBtwTouches = Vector3.Distance(prTouchPreviousWorldPosition, scTouchPreviousWorldPosition);

            var difference = currentDistanceBtwTouches - previousDistanceBtwTouches;

            Zoom(difference * ZoomSpeed);
        }
        else
        {
            var direction = prTouchPreviousWorldPosition - prTouchCurrentWorldPosition;
            _mainCamera.transform.position += direction;
        }
    }

    private void Zoom(float increment)
    {
        _mainCamera.orthographicSize = Mathf.Clamp(_mainCamera.orthographicSize - increment, ZoomMin, ZoomMax);
    }
}
