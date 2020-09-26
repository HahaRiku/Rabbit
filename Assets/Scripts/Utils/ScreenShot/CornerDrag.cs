using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CornerDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {
    public RangeScreenShotKuangType 點的位子;
    private delegate void ExeFunc();
    private ExeFunc exeFunc;
    private RectTransform parentRT;
    private CanvasScaler ScreenShotCanvas;
    private Vector2 ScreenSize;
    private Image image;

    // Start is called before the first frame update
    void Start() {
        parentRT = transform.parent.GetComponent<RectTransform>();
        ScreenShotCanvas = parentRT.parent.parent.GetComponent<CanvasScaler>();
        image = GetComponent<Image>();
        if (點的位子 == RangeScreenShotKuangType.LeftUp) {
            exeFunc = LeftUp;
        }
        else if (點的位子 == RangeScreenShotKuangType.RightUp) {
            exeFunc = RightUp;
        }
        else if (點的位子 == RangeScreenShotKuangType.LeftDown) {
            exeFunc = LeftDown;
        }
        else {
            exeFunc = RightDown;
        }
    }

    // Update is called once per frame
    void Update() {
    }

    public void OnPointerDown(PointerEventData data) {
        image.color = new Color(110.0f / 255.0f, 110.0f / 255.0f, 110.0f / 255.0f);
        Debug.Log("test down");
    }

    public void OnDrag(PointerEventData data) {
        if(data.dragging) {
            exeFunc();
        }
    }

    public void OnPointerUp(PointerEventData data) {
        image.color = new Color(1, 1, 1);
    }

    //(0,0) at leftDown of screen
    private void LeftUp() {
        Vector2 input = Input.mousePosition;
        Vector2 leftUp = new Vector2(input.x / Screen.width * ScreenShotCanvas.referenceResolution.x, input.y / Screen.height * ScreenShotCanvas.referenceResolution.y);
        Vector2 limit = new Vector2(parentRT.anchoredPosition.x + parentRT.sizeDelta.x / 2, parentRT.anchoredPosition.y - parentRT.sizeDelta.y / 2);
        leftUp = new Vector2((leftUp.x > limit.x) ? limit.x : leftUp.x, (leftUp.y < limit.y) ? limit.y : leftUp.y);
        Vector2 rightUp = new Vector2(parentRT.anchoredPosition.x + parentRT.sizeDelta.x / 2, leftUp.y);
        Vector2 leftDown = new Vector2(leftUp.x, parentRT.anchoredPosition.y - parentRT.sizeDelta.y / 2);
        SetParentRT(leftUp, rightUp, leftDown);
    }

    private void LeftDown() {
        Vector2 input = Input.mousePosition;
        Vector2 leftDown = new Vector2(input.x / Screen.width * ScreenShotCanvas.referenceResolution.x, input.y / Screen.height * ScreenShotCanvas.referenceResolution.y);
        Vector2 limit = new Vector2(parentRT.anchoredPosition.x + parentRT.sizeDelta.x / 2, parentRT.anchoredPosition.y + parentRT.sizeDelta.y / 2);
        leftDown = new Vector2((leftDown.x > limit.x) ? limit.x : leftDown.x, (leftDown.y > limit.y) ? limit.y : leftDown.y);
        Vector2 leftUp = new Vector2(leftDown.x, parentRT.anchoredPosition.y + parentRT.sizeDelta.y / 2);
        Vector2 rightUp = new Vector2(parentRT.anchoredPosition.x + parentRT.sizeDelta.x / 2, leftUp.y);
        SetParentRT(leftUp, rightUp, leftDown);
    }

    private void RightUp() {
        Vector2 input = Input.mousePosition;
        Vector2 rightUp = new Vector2(input.x / Screen.width * ScreenShotCanvas.referenceResolution.x, input.y / Screen.height * ScreenShotCanvas.referenceResolution.y);
        Vector2 limit = new Vector2(parentRT.anchoredPosition.x - parentRT.sizeDelta.x / 2, parentRT.anchoredPosition.y - parentRT.sizeDelta.y / 2);
        rightUp = new Vector2((rightUp.x < limit.x) ? limit.x : rightUp.x, (rightUp.y < limit.y) ? limit.y : rightUp.y);
        Vector2 leftUp = new Vector2(parentRT.anchoredPosition.x - parentRT.sizeDelta.x / 2, rightUp.y);
        Vector2 leftDown = new Vector2(leftUp.x, parentRT.anchoredPosition.y - parentRT.sizeDelta.y / 2);
        SetParentRT(leftUp, rightUp, leftDown);
    }

    private void RightDown() {
        Vector2 input = Input.mousePosition;
        Vector2 rightDown = new Vector2(input.x / Screen.width * ScreenShotCanvas.referenceResolution.x, input.y / Screen.height * ScreenShotCanvas.referenceResolution.y);
        Vector2 limit = new Vector2(parentRT.anchoredPosition.x - parentRT.sizeDelta.x / 2, parentRT.anchoredPosition.y + parentRT.sizeDelta.y / 2);
        rightDown = new Vector2((rightDown.x < limit.x) ? limit.x : rightDown.x, (rightDown.y > limit.y) ? limit.y : rightDown.y);
        Vector2 leftUp = new Vector2(parentRT.anchoredPosition.x - parentRT.sizeDelta.x / 2, parentRT.anchoredPosition.y + parentRT.sizeDelta.y / 2);
        Vector2 leftDown = new Vector2(leftUp.x, rightDown.y);
        Vector2 rightUp = new Vector2(rightDown.x, leftUp.y);
        SetParentRT(leftUp, rightUp, leftDown);
    }

    private void SetParentRT(Vector2 leftUp, Vector2 rightUp, Vector2 leftDown) {
        //Debug.Log("leftUp" + leftUp);
        //Debug.Log("rightUp" + rightUp);
        //Debug.Log("leftDown" + leftDown);
        parentRT.sizeDelta = new Vector2(rightUp.x - leftUp.x, leftUp.y - leftDown.y);
        parentRT.anchoredPosition = new Vector2((leftUp.x + rightUp.x) / 2, (leftUp.y + leftDown.y) / 2);
    }
}

public enum RangeScreenShotKuangType {
    LeftUp,
    RightUp,
    LeftDown,
    RightDown
}
