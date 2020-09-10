using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class SendArduino : MonoBehaviour
{
    public int glomRightEye = 13;
    public int glomLeftEye = 12;
    public int aiboRightEye = 10;
    public int aiboLeftEye = 9;
    public int glomBody = 8;
    public int aiboBody = 6;
    private float playhead;


    IEnumerator aiboBlink;
    IEnumerator glomBlink;
    IEnumerator glomFadeIn;
    IEnumerator glomFadeOut;
    IEnumerator aiboFadeIn;
    IEnumerator aiboFadeOut;

    [Range(0,255)]
    //public int intensity = 5;
    public int dim = 0;
    public int bright = 150;
    //public int fadeAmount = 5;

    // Use this for initialization
    void Start()
    {
        UduinoManager.Instance.pinMode(glomRightEye, PinMode.Output);
        UduinoManager.Instance.pinMode(glomLeftEye, PinMode.Output);
        UduinoManager.Instance.pinMode(aiboRightEye, PinMode.Output);
        UduinoManager.Instance.pinMode(aiboLeftEye, PinMode.Output);
        /// Create Coroutines for robot movement
        ///
        // Blinking
        aiboBlink = BlinkLoop(2f, 2f, aiboRightEye, aiboLeftEye);
        glomBlink = BlinkLoop(4f, 4f, glomLeftEye, glomRightEye);
        // Fading
        glomFadeIn = FadeInLoop(1, 0.2f, glomLeftEye, glomRightEye);
        glomFadeOut = FadeOutLoop(3, 0.2f, glomLeftEye, glomRightEye);
        aiboFadeIn = FadeInLoop(3, 0.2f, aiboLeftEye, aiboRightEye);
        aiboFadeOut = FadeOutLoop(2, 0.2f, aiboLeftEye, aiboRightEye);
        // Test starts
        //StartCoroutine(glomBlink);
        //StartCoroutine(aiboBlink);

    }

    // Update is called once per frame
    IEnumerator BlinkLoop(float on, float off, int pin1, int pin2)
    {
        while (true)
        {

            UduinoManager.Instance.digitalWrite(pin1, State.HIGH);
            UduinoManager.Instance.digitalWrite(pin2, State.HIGH);
            yield return new WaitForSeconds(on);
            UduinoManager.Instance.digitalWrite(pin1, State.LOW);
            UduinoManager.Instance.digitalWrite(pin2, State.LOW);
            yield return new WaitForSeconds(off);
            yield break;

        }


    }
    IEnumerator FadeInLoop(int fadeAmount1, float interval, int pin1, int pin2)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            UduinoManager.Instance.analogWrite(pin1, dim);
            UduinoManager.Instance.analogWrite(pin2, dim);
            Debug.Log("dim: " + dim);
            dim = dim + fadeAmount1;

            if (dim < 150)
            {
                fadeAmount1 = +fadeAmount1;

            } else
            {
                dim = 0;
                UduinoManager.Instance.analogWrite(pin1, dim);
                UduinoManager.Instance.analogWrite(pin2, dim);
                yield break;
            }
           
        }


    }
    IEnumerator FadeOutLoop(int fadeAmount, float interval, int pin1, int pin2)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            UduinoManager.Instance.analogWrite(pin1, bright);
            UduinoManager.Instance.analogWrite(pin2, bright);
            Debug.Log("bright: " + bright);
            bright = bright - fadeAmount;

            if (bright > 0)
            {
                fadeAmount = +fadeAmount;

            }
            else
            {
                bright = 0;
                UduinoManager.Instance.analogWrite(pin1, bright);
                UduinoManager.Instance.analogWrite(pin2, bright);
                yield break;
            }

        }


    }

    private void Update()
    {
        playhead = AudioLength.clipPos;
        //playhead = Mathf.Round(playhead * 1000f);
        //playhead.ToString();
        Debug.Log("From Arduino: " + playhead);

        if (playhead > 5.12)
        {
            StartCoroutine(aiboBlink);
        }
        if (playhead > 6.23)
        {
            StartCoroutine(glomBlink);
        }
        if (playhead > 10.12)
        {
            StartCoroutine(aiboBlink);
        }
        if (playhead > 12.23)
        {
            StartCoroutine(glomBlink);
        }

    }
}
