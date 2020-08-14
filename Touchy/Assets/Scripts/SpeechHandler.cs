using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class SpeechHandler : MonoBehaviour, IMixedRealitySpeechHandler
{
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
            default:
                Debug.Log($"Unrecognized keyword {eventData.Command.Keyword}");
                break;
        }

    }
}
