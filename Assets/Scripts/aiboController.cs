using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class aiboController : MonoBehaviour
{
    public int aiboRightEye = 10;
    public int aiboLeftEye = 9;
    public int aiboBody = 7;
    public AudioSourceLoudnessTester LoudTracker;
    public float soundLevelAibo;
    public bool gate = false;
    public bool rumbleGate = false;

    IEnumerator aiboBlink;
    IEnumerator aiboFadeIn;
    IEnumerator aiboFadeOut;
    IEnumerator aiboRumble;
    private float playhead;

    [Range(0, 255)]
    //public int intensity = 5;
    public int dim = 0;
    public int bright = 150;
    //public int fadeAmount = 5;

    // Use this for initialization
    void Start()
    {
        UduinoManager.Instance.pinMode(aiboRightEye, PinMode.Output);
        UduinoManager.Instance.pinMode(aiboLeftEye, PinMode.Output);
        UduinoManager.Instance.pinMode(aiboBody, PinMode.Output);
        /// Create Coroutines for robot movement
        ///
        // Blinking
        aiboBlink = BlinkLoop(.1f, .1f, aiboRightEye, aiboLeftEye);
        // Fading
        aiboFadeIn = FadeInLoop(3, 0.2f, aiboLeftEye, aiboRightEye);
        aiboFadeOut = FadeOutLoop(2, 0.2f, aiboLeftEye, aiboRightEye);
        // Test starts
        //StartCoroutine(glomBlink);
       // StartCoroutine(aiboBlink);

        

    }

    // Update is called once per frame
    IEnumerator BlinkLoop(float on, float off, int pin1, int pin2)
    {
        while (true)
        {

            UduinoManager.Instance.digitalWrite(pin1, State.HIGH);
            yield return new WaitForSeconds(on);
            UduinoManager.Instance.digitalWrite(pin1, State.LOW);
            yield return new WaitForSeconds(off);
            UduinoManager.Instance.digitalWrite(pin2, State.HIGH);   
            yield return new WaitForSeconds(on);
            UduinoManager.Instance.digitalWrite(pin2, State.LOW);
            yield return new WaitForSeconds(off);
           // gate = false;
            //yield break;

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

            }
            else
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

        /// EYES
        /// 
        soundLevelAibo = LoudTracker.clipLoudness;
        Debug.Log("soundLevel Aibo: " + soundLevelAibo);

       if (soundLevelAibo >= 12 && gate == false)
        {
            gate = true;
            if (gate == true)
            {
                UduinoManager.Instance.digitalWrite(aiboLeftEye, State.HIGH);
                //UduinoManager.Instance.digitalWrite(aiboLeftEye, State.HIGH);
            }


        }
        if (soundLevelAibo >= 8)
        {

                //UduinoManager.Instance.digitalWrite(aiboRightEye, State.HIGH);
                UduinoManager.Instance.digitalWrite(aiboRightEye, State.HIGH);


        }
        if (soundLevelAibo <= 3 && gate == true)
        {
            gate = false;
            if (gate == false)
            {
                UduinoManager.Instance.digitalWrite(aiboRightEye, State.LOW);
                UduinoManager.Instance.digitalWrite(aiboLeftEye, State.LOW);
            }


        }
        if (soundLevelAibo <= 0)
        {
            gate = false;
            if (gate == false)
            {
                UduinoManager.Instance.digitalWrite(aiboRightEye, State.LOW);
                UduinoManager.Instance.digitalWrite(aiboLeftEye, State.LOW);
            }


        }

        //BODY
        //float[] buzzTimes = { 22.70, 50.75, 100.08, 126.54, 155.35, 208.86, 251.07, 252.80, 259.10 };
        // character - Aibo = 1  Glom = 2
        //int[] character = { 1, 1, 2, 2, 1, 1, 2, 1, 1,};
        playhead = AudioLength.clipPos;
        Debug.Log("playheadAibo: " + playhead);

        if (playhead >= 22.7  && playhead <= 24.8)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                rumbleGate = false;
            }

        }
        else if (playhead >= 50 && playhead <= 54)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                rumbleGate = false;
            }
        }
        else if (playhead >= 93 && playhead <= 94.2)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                rumbleGate = false;
            }
        }
        else if (playhead >= 124.8 && playhead <= 126)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                rumbleGate = false;
            }
        }
        else if (playhead >= 173 && playhead <= 175)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                rumbleGate = false;
            }
        }
        else if (playhead >= 178 && playhead <= 178)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(aiboBody, State.HIGH);
                rumbleGate = false;
            }
        }
        else
        {
            UduinoManager.Instance.digitalWrite(aiboBody, State.LOW);
            UduinoManager.Instance.digitalWrite(aiboBody, State.LOW);
        }


    }

}

