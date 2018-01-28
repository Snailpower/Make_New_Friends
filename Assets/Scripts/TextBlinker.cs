using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinker : MonoBehaviour {

    public float interval = 1f;

    private float currentTime;

    private Text text;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        currentTime = interval;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            if (text.enabled)
            {
                text.enabled = false;
                currentTime = interval;
            }
            else
            {
                text.enabled = true;
                currentTime = interval;
            }
        }
	}
}
