﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class datasaver : MonoBehaviour {

    public int shouldcreate = 1;
	// Use this for initialization
	void Start () {
        GameObject.DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
