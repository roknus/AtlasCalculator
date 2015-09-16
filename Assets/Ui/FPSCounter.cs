using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSCounter : MonoBehaviour 
{
    public Text Counter;

    private float deltaTime = 0.0f;
 
	void Update ()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        Counter.text = ((int)fps).ToString();
	}
}
