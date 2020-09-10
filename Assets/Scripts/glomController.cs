using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class glomController : MonoBehaviour
{
    public int glomRightEye = 13;
    public int glomLeftEye = 12;
    public int glomBody = 8;
    public AudioSourceLoudnessTester LoudTracker;
    public float soundLevelGlom;
    public bool gate = false;
    public bool rumbleGate = false;
    private float timerMax = 5f;
    private float timer = 0f;

    IEnumerator glomBlink;
    IEnumerator glomFadeIn;
    IEnumerator glomFadeOut;
    IEnumerator glomRumble;
    private float playhead;

    [Range(0, 255)]
    //public int intensity = 5;
    public int dim = 0;
    public int bright = 150;

    //public int fadeAmount = 5;

    // Use this for initialization
    void Start()
    {
        UduinoManager.Instance.pinMode(glomRightEye, PinMode.Output);
        UduinoManager.Instance.pinMode(glomLeftEye, PinMode.Output);
        UduinoManager.Instance.pinMode(glomBody, PinMode.Output);

        /// Create Coroutines for robot movement
        ///
        // Blinking
        glomBlink = BlinkLoop(.2f, .2f, glomLeftEye, glomRightEye);
        // Fading
        glomFadeIn = FadeInLoop(1, 0.2f, glomLeftEye, glomRightEye);
        glomFadeOut = FadeOutLoop(3, 0.2f, glomLeftEye, glomRightEye);
        //glomRumble = rumble();
        // Test starts
        //StartCoroutine(glomBlink);
        //StartCoroutine(aiboBlink);


        //soundLevelGlom = LoudTracker.clipLoudness;




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
        soundLevelGlom = LoudTracker.clipLoudness;
        Debug.Log("soundLevelGlom: " + soundLevelGlom);
        timer += Time.deltaTime;
        if (soundLevelGlom >= 5 && gate == false)
        {
            gate = true;
            if (gate == true)
            {
                //UduinoManager.Instance.digitalWrite(glomRightEye, State.HIGH);
                //UduinoManager.Instance.digitalWrite(glomLeftEye, State.HIGH);
                StartCoroutine(glomBlink);
            }


        }
        if (soundLevelGlom <= 2 && gate == true)
        {
            gate = false;
            if (gate == false)
            {
                UduinoManager.Instance.digitalWrite(glomRightEye, State.LOW);
                UduinoManager.Instance.digitalWrite(glomLeftEye, State.LOW);
                StopCoroutine(glomBlink);
            }


        }


        //float[] buzzTimes = { 22.70, 50.75, 100.08, 126.54, 155.35, 208.86, 251.07, 252.80, 259.10 };
        // character - Aibo = 1  Glom = 2
        //int[] character = { 1, 1, 2, 2, 1, 1, 2, 1, 1,};
        playhead = AudioLength.clipPos;
        Debug.Log("playhead: "+playhead);
        if (playhead >= 64.8 && playhead <= 69)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(glomBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(glomBody, State.HIGH);
                rumbleGate = false;
            }

        }
        else if (playhead >= 96 && playhead <= 98)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(glomBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(glomBody, State.HIGH);
                rumbleGate = false;
            }
        }
        else if (playhead >= 165.6 && playhead <= 178)
        {
            rumbleGate = true;
            if (rumbleGate)
            {
                UduinoManager.Instance.digitalWrite(glomBody, State.HIGH);
                UduinoManager.Instance.digitalWrite(glomBody, State.HIGH);
                rumbleGate = false;
            }
        }
        else
        {
            UduinoManager.Instance.digitalWrite(glomBody, State.LOW);
            UduinoManager.Instance.digitalWrite(glomBody, State.LOW);
        }


    }
}
