using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class FillBar : MonoBehaviour
{

    // Unity UI References
    public float total;
    public float current;
    public float test;
    public Slider slider;
    //public Text displayText;

    // Event to invoke when the progress bar fills up
    private UnityEvent onProgressComplete;

    // Create a property to handle the slider's value
    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
            //displayText.text = (slider.value * 100).ToString("0.00") + "%";
        }
    }

    // Use this for initialization
    void Start()
    {
        CurrentValue = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
             
        current = AudioLength.clipPos;
        total = AudioLength.clipLength;
        CurrentValue = current / total;
        test = CurrentValue;

    }
}

