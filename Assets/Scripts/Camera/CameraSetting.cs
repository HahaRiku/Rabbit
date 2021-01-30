using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour {
    private const float PIXELS_PER_UNIT = 100;
    private const float SCREEN_WIDTH_PIXELS = 360;
    // Start is called before the first frame update
    void Start() {
        float expectedHeightPixels = SCREEN_WIDTH_PIXELS * Screen.height / Screen.width;
        Camera.main.orthographicSize = expectedHeightPixels / 2.0f / PIXELS_PER_UNIT;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
