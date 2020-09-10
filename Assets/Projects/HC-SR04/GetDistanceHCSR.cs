using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;
using System;

public class GetDistanceHCSR : MonoBehaviour
{
    public AudioLowPassFilter voiceAF;
    public AudioLowPassFilter voiceBF;

    public AudioReverbFilter voiceAR;
    public AudioReverbFilter voiceBR;

    public Transform distancePlane;

    bool enableSmooth = false;
    public bool hideWhenTooFar = false;
    public Text sensorText;

    float distance = 0;

    public float minDistance = 150;
    public float maxDistance = 2000;
    public float sensorValue = 200;

    [Range(0,200)]
    public float divideDistance = 15.0f;

    public float[] ds = new float[11];
    int dsi = 0;

    void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;
        if (!voiceAF || !voiceBF || !voiceAR || !voiceBR)
            Debug.LogError("Need to hook in the audio filters for voices!");
    }

    void Update()
    {
        //////////////////////////////////////////////////////// BLOCK A
        sensorText.text = "Sensor Value: " + distance;
        sensorValue = distance;

        //////////////////////////////////////////////////////// BLOCK B
        if (hideWhenTooFar && distance > maxDistance)
        {
            distancePlane.gameObject.SetActive(false);
        }
        else
        {
            if (!distancePlane.gameObject.activeInHierarchy)
                distancePlane.gameObject.SetActive(true);
        }

        // Double check this doesn't cause weird audio behaviour when hiding the far away obj
        if (!distancePlane.gameObject.activeInHierarchy) return;

        //////////////////////////////////////////////////////// BLOCK C
        // Ranges
        // Z: 0 - 30 Unity Meters
        // distance: 0 - 4000 mm

        // Control the number range of the sensor input
        var d = Mathf.Clamp(distance - minDistance, 1, maxDistance);
        // Make normalised value
        var d_norm = d / maxDistance;

        // This converts any sensor range into the arbitary unity range based on object positions.
        var unityZ = Klak.Math.BasicMath.Lerp(0, 30, d_norm);
        var normalisedPosition = new Vector3(0, 0, unityZ);

        //////////////////////////////////////////////////////// BLOCK D
        // Move the plane
        if (enableSmooth)
        {
            distancePlane.transform.position = Vector3.Lerp(distancePlane.transform.position, normalisedPosition, Time.deltaTime * 10.0f);
        }
        else
        {
           distancePlane.transform.position = normalisedPosition;
        }

        var audioFilterDistance = Klak.Math.BasicMath.Lerp(22000,150, d_norm);
        voiceAF.cutoffFrequency = audioFilterDistance;
        voiceBF.cutoffFrequency = audioFilterDistance;
        var audioReverbDistance = Klak.Math.BasicMath.Lerp(0, -1700, d_norm);
        voiceAR.dryLevel = audioReverbDistance;
        voiceBR.dryLevel = audioReverbDistance;
    }

    void DataReceived(string data, UduinoDevice baord)
    {
        float d = -1;        
        bool ok = float.TryParse(data, out d); // Trying to parse data to a float

        if(ok)
        {
            // Debug.Log("connected");
            if ((int)d == -1) return;
            // orig: distance = d;
            ds[dsi] = d;
            dsi++;
            if (dsi >= ds.Length) dsi = 0;
            distance = GetMedian(ds);
        } else
        {
            Debug.Log("Error parsing " + data);
        }
    }

    public float GetMedian(float[] array)
    {
        float[] tempArray = array;
        int count = tempArray.Length;

        Array.Sort(tempArray);

        float medianValue = 0;

        if (count % 2 == 0)
        {
            // count is even, need to get the middle two elements, add them together, then divide by 2
            float middleElement1 = tempArray[(count / 2) - 1];
            float middleElement2 = tempArray[(count / 2)];
            medianValue = (middleElement1 + middleElement2) / 2;
        }
        else
        {
            // count is odd, simply get the middle element.
            medianValue = tempArray[(count / 2)];
        }

        return medianValue;
    }
}
