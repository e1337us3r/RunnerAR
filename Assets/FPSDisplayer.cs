﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplayer : MonoBehaviour
{

    public Text gui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        gui.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    }
}
