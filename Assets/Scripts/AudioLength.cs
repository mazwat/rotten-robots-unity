//Attach an AudioSource component to a GameObject along with this script.
//Click and drag or choose an Audio clip to the AudioClip field in the AudioSource.
//Click and drag or choose a different Audio clip for the Audio Clip 2 field in the Inspector window.

//This script switches between two Audio clips and outputs each of their lengths in the console
//In Play Mode, press the space key to switch between the Audio clips

using UnityEngine;
using UnityEngine.Audio;

public class AudioLength : MonoBehaviour
{
	//Make sure your GameObject has an AudioSource component first

	AudioSource m_AudioSource;
    public static float clipLength;
	public static float clipPos;
    public AudioSourceLoudnessTester Loud1;
    public AudioSourceLoudnessTester Loud2;
    //public GameObject Soundtrack;
    public static float soundLevelAibo;
    public static float soundLevelGlom;
    //float[] buzzTimes = { 22.70, 50.75, 100.08, 126.54, 155.35, 208.86, 251.07, 252.80, 259.10 };
    // character - Aibo = 1  Glom = 2
    //int[] character = { 1, 1, 2, 2, 1, 1, 2, 1, 1,};
    int count = 0;

    //Make sure to set an Audio Clip in the AudioSource component
    AudioClip m_AudioClip;


    void Start()
	{
		//Fetch the AudioSource from the GameObject
		m_AudioSource = GetComponent<AudioSource>();

        //Set the original AudioClip as this clip
        m_AudioClip = m_AudioSource.clip;

		clipLength = m_AudioSource.clip.length;

        soundLevelAibo = 0;
        soundLevelGlom = 0;


        //Output the current clip's length
        //Debug.Log("Audio clip length : " + clipLength);    

    }

    void Update()
    {
        //Debug.Log("Audio clip position : " + m_AudioSource.time);
        clipPos = m_AudioSource.time;

        soundLevelAibo = Loud1.clipLoudness;
        soundLevelGlom = Loud2.clipLoudness;

        //if (clipPos >= buzzTimes[count] )
        //{

        //}
        //{

        //}

    }

  //  void OnGUI()
  //  {
		//// Make a text field that modifies stringToEdit.
		//clipLength = GUI.TextField(new Rect(10, 10, 200, 20), clipLength, 25);
  //  }

	void OnGUI()
	{
		string lengthText = GUI.TextField(new Rect(10, 10, 200, 20), "Soundtrack Length: " + clipLength.ToString("0.00"));
		string posText = GUI.TextField(new Rect(10, 40, 200, 20), "Soundtrack Position: " + clipPos.ToString("0.00"));
        string sound1 = GUI.TextField(new Rect(10, 80, 200, 20), "Aibo Level: "+soundLevelAibo.ToString("0"));
        string sound2 = GUI.TextField(new Rect(10, 120, 200, 20), "Glom Level: "+soundLevelGlom.ToString("0"));

        float newLength;
		float newPos;
		if (float.TryParse(lengthText, out newLength))
		{
			clipLength = newLength;
		}
		if (float.TryParse(posText, out newPos))
		{
			clipPos = newPos;
		}
	}
}


