using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeeperPopup : MonoBehaviour {
    private Animator ani = null;
    private Text nameText = null;
    private Image keeperImg = null;
    private RectTransform keeperImgRT = null;
    private Text skillText = null;
    private Text descText = null;

    private Vector2 keeperImgDefaultSize;

    // Start is called before the first frame update
    void Start() {
        ani = GetComponent<Animator>();
        Transform BG = transform.GetChild(1);
        nameText = BG.GetChild(0).GetComponent<Text>();
        keeperImg = BG.GetChild(1).GetChild(0).GetComponent<Image>();
        keeperImgRT = keeperImg.GetComponent<RectTransform>();
        skillText = BG.GetChild(2).GetChild(0).GetComponent<Text>();
        descText = BG.GetChild(3).GetChild(0).GetComponent<Text>();

        keeperImgDefaultSize = keeperImg.GetComponent<RectTransform>().sizeDelta;
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
    }

    private void SetInfo(int index) {
        nameText.text = HouseKeeperSystem.GetNameByIndex(index);
        ImageUtils.FittingImg(keeperImgRT, keeperImg, HouseKeeperSystem.GetSpriteByIndex(index), keeperImgDefaultSize);
        skillText.text = HouseKeeperSystem.GetSkillStrByIndex(index);
        descText.text = HouseKeeperSystem.GetDescByIndex(index);
    }
}
