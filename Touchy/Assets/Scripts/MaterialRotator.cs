using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class MaterialRotator : MonoBehaviour,IMixedRealityTouchHandler, IMixedRealityPointerHandler, IInteractableHandler
{
    public List<Material> Materials = new List<Material>();
    // Start is called before the first frame update
    private MeshRenderer _mr = null;
    private int _index = 0;
    void Start()
    {

        _mr = gameObject.GetComponent<MeshRenderer>();
        if (Materials.Contains(_mr.material))
        {
            _index = Materials.IndexOf(_mr.material);
        }
        else
        {
            Materials.Add(_mr.material);
        }

    }

    public void RotateMaterial()
    {
        if (_mr == null || Materials.Count < 2) return; // Nothing to do
        int nextMaterial = (_index + 1) % Materials.Count;

        _mr.material = Materials[nextMaterial];
        _index = nextMaterial;
    }

    private static bool _useTouch = true;
    private static bool _usePointer = false;

    public static void ToggleTouch()
    {
        _useTouch = !_useTouch;
        if (_useTouch) Debug.Log("Touch is now on");
        else Debug.Log("Touch is now off");
    }

    public static void TogglePointer()
    {
        _usePointer = !_usePointer;
        if (_usePointer) Debug.Log("Pointer is now on");
        else Debug.Log("Pointer is now off");
    }

    private DateTime _lastOnTouchStarted = DateTime.MinValue;
    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        //if (eventData.selectedObject == gameObject)
        //{
            if (_lastOnTouchStarted == eventData.EventTime)
            {
                Debug.Log($"Duplicate OnTouchStarted for {gameObject.name}");
            }
            else
            {
                _lastOnTouchStarted = eventData.EventTime;
                if (_useTouch)
                {
                    Debug.Log($"Rotating material because of OnTouchStarted for {gameObject.name}");
                    RotateMaterial();
                }
                else
                {
                    Debug.Log($"Ignoring OnTouchStarted for {gameObject.name} because touch is disabled");
                }
                eventData.Use();

            }
        //}

    }

    private DateTime _lastOnTouchCompleted = DateTime.MinValue;
    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        //if (eventData.selectedObject == gameObject)
        //{
            if (_lastOnTouchCompleted == eventData.EventTime)
            {
                Debug.Log($"Duplicate OnTouchCompleted for {gameObject.name}");
            }
            else
            {
                _lastOnTouchCompleted = eventData.EventTime;
                Debug.Log($"Eating OnTouchCompleted for {gameObject.name}");
                eventData.Use();
            }
        //}
        return;
    }

    private DateTime _lastOnTouchUpdated = DateTime.MinValue;
    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        //if (eventData.selectedObject == gameObject)
        //{
            //if (_lastOnTouchUpdated == eventData.EventTime)
            //{
            //    Debug.Log($"Duplicate OnTouchUpdated for {gameObject.name}");
            //    return;
            //}
            //else
            //{
            //    _lastOnTouchUpdated = eventData.EventTime;
            //    Debug.Log($"Eating OnTouchUpdated for {gameObject.name}");
            //    eventData.Use();
            //}
        //}
        return;
    }

    private DateTime _lastOnPointerDown = DateTime.MinValue;
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {

        //if (eventData.selectedObject == gameObject)
        //{
            if (_lastOnPointerDown == eventData.EventTime)
            {
                Debug.Log($"Duplicate OnPointerDown for {gameObject.name}");
                return;
            }
            else
            {
                _lastOnPointerDown = eventData.EventTime;
                if (_usePointer)
                {
                    Debug.Log($"Rotating because of OnPointerDown on {gameObject.name}");
                    RotateMaterial();
                }
                else
                {
                    Debug.Log($"Eating OnPointerDown for {gameObject.name} because pointer is disabled");
                }
                eventData.Use();
            }
        //} 
    }

    private DateTime _lastOnPointerDragged = DateTime.MinValue;
    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        //if (eventData.selectedObject == gameObject)
        //{
            //if (_lastOnPointerDragged == eventData.EventTime)
            //{
            //    Debug.Log($"Duplicate OnPointerDragged for {gameObject.name}");
            //}
            //else
            //{
            //    _lastOnPointerDragged = eventData.EventTime;
            //    Debug.Log($"Eating OnPointerDragged for {gameObject.name}");
            //    eventData.Use();
            //}
        //}
    }

    private DateTime _lastOnPointerUp = DateTime.MinValue;
    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        //if (eventData.selectedObject == gameObject)
        //{
            if (_lastOnPointerUp == eventData.EventTime)
            {
                Debug.Log($"Duplicate OnPointerUp for {gameObject.name}");
            }
            else
            {
                _lastOnPointerUp = eventData.EventTime;
                Debug.Log($"Eating OnPointerUp for {gameObject.name}");
                eventData.Use();
            }
        //}
    }

    private DateTime _lastOnPointerClicked = DateTime.MinValue;
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        //if (eventData.selectedObject == gameObject)
        //{
            if (_lastOnPointerClicked == eventData.EventTime)
            {
                Debug.Log($"Duplicate OnPointerClicked for {gameObject.name}");
            }
            else
            {
                _lastOnPointerClicked = eventData.EventTime;
                Debug.Log($"Eating OnPointerClicked for {gameObject.name}");
                eventData.Use();
            }
        //}
    }

    public void OnStateChange(InteractableStates state, Interactable source)
    {
        Debug.Log($"{state}, {source}");
    }

    public void OnVoiceCommand(InteractableStates state, Interactable source, string command, int index = 0, int length = 1)
    {
        Debug.Log($"{state},{source},{command},{index},{length}");
    }

    public void OnClick(InteractableStates state, Interactable source, IMixedRealityPointer pointer = null)
    {
        Debug.Log($"{state},{source},{pointer}");
    }
}
