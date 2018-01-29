﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public static Fade fade;

    public Animator fading;

    private void Awake()
    {
        fade = this;
    }

    public void fadeNow()
    {
        fading.SetTrigger("Fade");
    }
}