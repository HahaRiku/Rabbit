﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        if(SceneManager.GetActiveScene().name == "Title") {
            Destroy(gameObject);
        }
    }
}
