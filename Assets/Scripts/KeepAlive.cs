﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAlive : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
