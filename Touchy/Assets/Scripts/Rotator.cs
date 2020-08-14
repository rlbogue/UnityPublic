using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Rotator : MonoBehaviour
{
    public float XRotationRate = 1.0f;
    public float YRotationRate = 2.0f;
    public float ZRotationRate = 3.0f;
    public float RotationRateScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Rotation
        float totalScale = Time.deltaTime * RotationRateScale;
        Vector3 v3 = new Vector3(XRotationRate * totalScale, YRotationRate * totalScale, ZRotationRate * totalScale);
        gameObject.transform.Rotate(v3);
    }
}
