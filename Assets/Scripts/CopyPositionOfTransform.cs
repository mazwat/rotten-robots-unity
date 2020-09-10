using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPositionOfTransform : MonoBehaviour {
    public Transform target;
    public enum Mode
    {
        World,
        Local
    }
    public Mode mode = Mode.World;
	
	// Update is called once per frame
	void Update () {
		if(mode == Mode.Local)
        {
            transform.localPosition = target.localPosition;
        }
        else
        {
            transform.position = target.position;
        }
	}
}
