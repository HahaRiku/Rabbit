using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FieldButtonPressDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    private float startTime;
    private float longPressLimitation = 1.0f;
    private Animator hintAni;
    private Image hintImage;
    private bool open = false;
    private bool down = false;

    void Start() {
        hintAni = transform.GetChild(2).GetComponent<Animator>();
        hintImage = transform.GetChild(2).GetChild(0).GetComponent<Image>();
    }

    void Update() {
        if(down) {
            if (!open && Time.time - startTime >= longPressLimitation) {
                open = true;
                hintImage.sprite = HouseKeeperSystem.GetHintByIndex(SystemVariables.currentHKindex);
                hintAni.SetTrigger("OpenHint");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(SystemVariables.plantStatus == PlantStatus.種植中) {
            startTime = Time.time;
            down = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if(SystemVariables.plantStatus == PlantStatus.種植中) {
            open = false;
            down = false;
            hintAni.SetTrigger("CloseHint");
        }
    }
}
