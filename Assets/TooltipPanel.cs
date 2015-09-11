﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TooltipPanel : MonoBehaviour 
{
    public Text text;

    private static Vector3 mouseOffset = new Vector2(5, 5);

    public static TooltipPanel Instance { get; private set; }

	void Awake () 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        text = GetComponentInChildren<Text>();
	}

    public void Start()
    {
        gameObject.SetActive(false);
    }
	
	void Update () 
    {
        RectTransform t = transform as RectTransform;
        if (t.sizeDelta.x + Input.mousePosition.x > Screen.width)
        {
            transform.position = new Vector3(Screen.width - t.sizeDelta.x, Input.mousePosition.y) + mouseOffset;
        }
        else
        {
            transform.position = Input.mousePosition + mouseOffset;
        }
	}

    public void Enable(string _ttp)
    {
        text.text = _ttp;
    }

    public void Disable()
    {
        transform.position = new Vector3(-50,-50);
    }
}