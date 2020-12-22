using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {
    private float max_width;
    private float max_height;
    private RectTransform targetImageRT = null;
    private RectTransform targetKuangRT = null;
    private Animator openAni = null;
    private Texture2D currentTexture = null;
    private GameObject rangeScreenshot = null;
    private Image targetImg = null;

    void Start() {
        RectTransform BgRt = transform.GetChild(0).GetComponent<RectTransform>();
        max_width = BgRt.sizeDelta.x * (200.0f/277.0f);
        max_height = BgRt.sizeDelta.y * (280.0f/410.0f);
        targetImageRT = BgRt.GetChild(0).GetComponent<RectTransform>();
        targetKuangRT = targetImageRT.GetChild(0).GetComponent<RectTransform>();
        openAni = GetComponent<Animator>();
        rangeScreenshot = transform.parent.GetChild(2).gameObject;
        targetImg = targetImageRT.GetComponent<Image>();
    }

    public void OpenResult(Texture2D texture) {
        if (texture.width <= max_width && texture.height <= max_height) {
            targetImageRT.sizeDelta = new Vector2(texture.width, texture.height);
        }
        else if((texture.width / max_width) >= (texture.height / max_height)) {    //比較寬高誰超出的比例較多 以該者為基準 縮至限制內 寬高同乘一比例
            targetImageRT.sizeDelta = new Vector2(max_width, texture.height * (max_width / texture.width));
        }
        else {
            targetImageRT.sizeDelta = new Vector2(texture.width * (max_height / texture.height), max_height);
        }
        targetKuangRT.sizeDelta = new Vector2(targetImageRT.sizeDelta.x + 10, targetImageRT.sizeDelta.y + 10);
        targetImg.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        currentTexture = texture;
        openAni.SetTrigger("Open");
        rangeScreenshot.SetActive(false);
    }

    public void Share() {
        string filePath = Path.Combine(Application.temporaryCachePath, "sharedImg.png");
        File.WriteAllBytes(filePath, currentTexture.EncodeToPNG());
        new NativeShare().AddFile(filePath)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }

    public void SaveToLocal() {
        DateTime localDate = DateTime.Now;
        NativeGallery.SaveImageToGallery(currentTexture, "Rabbit", "Rabbit-photo-" + localDate.ToString("yyyyMMdd-HHmmss") + ".png");
    }

    public void Close() {
        // To avoid memory leaks
        Destroy(currentTexture);
        currentTexture = null;
    }
}
