﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackBot : MonoBehaviour {

    public Transform rightWheel;
    public Transform leftWheel;
    // Use this for initialization
    void Start () {
       /* rightWheel = transform.Find("rightWheel").transform;
        leftWheel = transform.Find("leftWheel").transform;*/
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("转");
        Debug.Log(rightWheel.localEulerAngles);
        rightWheel.Rotate(0, -1, 0);
        leftWheel.Rotate(0, 1, 0);
    }
}
