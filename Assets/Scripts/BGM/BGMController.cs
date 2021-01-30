using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        if(SceneManager.GetActiveScene().name == "Title") {
            Destroy(gameObject);
        }
    }
}
