using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrotCountUI : MonoBehaviour {
    private Text text;
    private int recordedCount;

    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
        recordedCount = SystemVariables.CarrotCount;
        recordedCount = -1;
    }

    // Update is called once per frame
    void Update() {
        if(recordedCount != SystemVariables.CarrotCount) {
            recordedCount = SystemVariables.CarrotCount;
            string temp = recordedCount.ToString();
            int tempLength = temp.Length;
            for (int i = 0; i < (8 - tempLength); i++) {
                temp = "0" + temp;
            }
            text.text = temp;
        }
    }
}
