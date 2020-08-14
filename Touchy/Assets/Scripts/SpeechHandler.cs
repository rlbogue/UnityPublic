using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class SpeechHandler : MonoBehaviour, IMixedRealitySpeechHandler
{
    public Transform ObjectToPull = null;
    public Camera RelativeToCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySpeechHandler>(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private DateTime lastKeyword = DateTime.MinValue;
    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        if (eventData.EventTime == lastKeyword)
        {
            Debug.Log($"Duplicate event {eventData.Command.Keyword} blocked");
        }
        else lastKeyword = eventData.EventTime;

        switch (eventData.Command.Keyword.ToLower())
        {
            case "touch":
                MaterialRotator.ToggleTouch();
                eventData.Use();
                break;
            case "pointer":
                MaterialRotator.TogglePointer();
                eventData.Use();
                break;
            case "pull":
                if (ObjectToPull == null || RelativeToCamera == null )
                {
                    Debug.Log("Unable to pull, no object or no camera");
                }
                else
                {
                    // Get New Object World Space Location
                    Vector3 targetLocation = RelativeToCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f*ObjectToPull.transform.localScale.z));
                    ObjectToPull.transform.position = targetLocation;
                }
                eventData.Use();
                break;
            case "smaller":
                ScaleObject(0.9f);
                break;
            case "larger":
                ScaleObject(1.1f);
                break;
            default:
                Debug.Log($"Unrecognized keyword {eventData.Command.Keyword}");
                break;
        }

    }

    public void ScaleObject(float scale)
    {
        if (ObjectToPull == null) return;
        ObjectToPull.transform.localScale = (ObjectToPull.transform.localScale * scale);
    }
}
