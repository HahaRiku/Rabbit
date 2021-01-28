using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitPopup : MonoBehaviour {
    [Header("Sprites")]
    public Sprite 設為最愛;
    public Sprite 已設最愛;
    public Sprite 最愛已滿;

    private Animator ani = null;
    private Text nameText = null;
    private Image rabbitImg = null;
    private RectTransform rabbitRT = null;
    private Image loveImg = null;
    private RectTransform loveRT = null;
    private Button loveBtn = null;
    private Text descText = null;

    private Vector2 rabbitImgDefaultSize;
    private Vector2 loveImgDefaultSize;
    // Start is called before the first frame update
    void Start() {
        ani = GetComponent<Animator>();
        Transform BG = transform.GetChild(1);
        nameText = BG.GetChild(0).GetComponent<Text>();
        rabbitImg = BG.GetChild(1).GetChild(0).GetComponent<Image>();
        rabbitRT = rabbitImg.GetComponent<RectTransform>();
        loveImg = BG.GetChild(2).GetComponent<Image>();
        loveRT = loveImg.GetComponent<RectTransform>();
        loveBtn = loveImg.GetComponent<Button>();
        descText = BG.GetChild(3).GetChild(0).GetComponent<Text>();

        rabbitImgDefaultSize = rabbitImg.GetComponent<RectTransform>().sizeDelta;
        loveImgDefaultSize = loveImg.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Open(int index) {
        SetInfo(index);
        ani.SetTrigger("Open");
    }

    public void Close() {
        ani.SetTrigger("Close");
        loveBtn.onClick.RemoveAllListeners();
    }

    private void SetInfo(int index) {
        nameText.text = RabbitSystem.GetRabbitNameById(index);
        ImageUtils.FittingImg(rabbitRT, rabbitImg, RabbitSystem.GetRabbitSpriteById(index), rabbitImgDefaultSize);
        Debug.Log(RabbitSystem.GetRabbitLoveCount());
        Debug.Log(RabbitSystem.maxLoveCount);
        ImageUtils.FittingImg(loveRT, loveImg, (RabbitSystem.GetRabbitLoveById(index) ? 已設最愛 : ((RabbitSystem.GetRabbitLoveCount() >= RabbitSystem.maxLoveCount) ? 最愛已滿 : 設為最愛)), loveImgDefaultSize);
        loveBtn.onClick.AddListener(delegate () { OnClickLove(index); });
        descText.text = RabbitSystem.GetRabbitDescById(index);
    }

    private void OnClickLove(int index) {
        if (RabbitSystem.GetRabbitLoveCount() >= RabbitSystem.maxLoveCount && !RabbitSystem.GetRabbitLoveById(index))
            return;
        Sprite sprite = null;
        if (RabbitSystem.GetRabbitLoveById(index)) {
            sprite = 設為最愛;
            RabbitSystem.SetRabbitLoveById(index, false);
        }
        else {
            sprite = 已設最愛;
            RabbitSystem.SetRabbitLoveById(index, true);
        }
        ImageUtils.FittingImg(loveRT, loveImg, sprite, loveImgDefaultSize);
    }
}
