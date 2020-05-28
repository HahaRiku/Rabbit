using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchEvent_handler;
using UnityEngine.UI;

public class TestInput : MonoBehaviour {

    private Image image;
    private GameObject text;

    // Start is called before the first frame update
    void Start() {
        image = GetComponent<Image>();
        text = transform.GetChild(0).gameObject;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //EventDetect.TouchDetect();
        /*if(Input.GetMouseButtonDown(0)) {
            EventDetect.status = "doubleClick";
        }
        else if(Input.GetMouseButtonUp(0)) {
            EventDetect.status = "Nothing";
        }*/
        /*switch(EventDetect.status) {
            case "doubleClick":
                image.color = new Color(1, 0, 0);
                break;
            case "swipe":
                image.color = new Color(0, 1, 0);
                break;
            case "holding":
                image.color = new Color(0, 0, 1);
                break;
        }*/
    }

    public void ClickButton() {
        text.SetActive(true);
    }
}
