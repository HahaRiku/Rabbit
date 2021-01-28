using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class FieldManager : MonoBehaviour {
    [Header("Images")]
    public Sprite unknownHK;
    [Header("Parameters")]
    public float 收成每次點擊長高多少;
    public float originalY;
    public float finalY;
    [Header("Prefabs")]
    public GameObject smallCarrotPrefab;
    private int 種植時長 = 30;
    private Image ChosenHouseKeeper;
    private Text ChosenHouseKeeperName;
    private Text ChosenHouseKeeperSkill;
    private int chosenIndex;
    private Button GoButton;
    private GameObject 田園Button;
    private GameObject 收成Button;
    private GameObject 種植中Button;
    private Text 種植中時間Text;
    private RectTransform BigCarrot;
    private Animator HarvestAni;
    private GameObject CloseDetect_Harvest;
    private Button FieldButton;
    private Canvas FieldCanvas;

    // Start is called before the first frame update
    void Start() {
        chosenIndex = 0;
        GameObject ChangeHouseKeeper = transform.GetChild(2).GetChild(2).gameObject;
        ChosenHouseKeeper = ChangeHouseKeeper.transform.GetChild(0).GetComponent<Image>();
        ChosenHouseKeeperName = ChangeHouseKeeper.transform.GetChild(3).GetComponent<Text>();
        ChosenHouseKeeperSkill = ChangeHouseKeeper.transform.GetChild(4).GetComponent<Text>();
        GoButton = transform.GetChild(2).GetChild(3).GetComponent<Button>();
        ChangeHouseKeeperRoutine();

        田園Button = transform.GetChild(0).GetChild(0).gameObject;
        種植中Button = transform.GetChild(0).GetChild(1).gameObject;
        收成Button = transform.GetChild(3).gameObject;
        種植中時間Text = 種植中Button.transform.GetChild(1).GetComponent<Text>();
        BigCarrot = transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        HarvestAni = BigCarrot.parent.parent.GetComponent<Animator>();
        CloseDetect_Harvest = transform.GetChild(4).gameObject;
        FieldButton = transform.GetChild(0).GetComponent<Button>();
        FieldCanvas = GetComponent<Canvas>();
        CheckPlantTime();
    }

    // Update is called once per frame
    void Update() {
        if(SystemVariables.plantStatus == PlantStatus.種植中) {
            CheckPlantTime();
        }
    }

    public void LeftChangeHouseKeeper() {
        chosenIndex = (chosenIndex == 0) ? HouseKeeperSystem.GetKeeperCount() - 1 : chosenIndex - 1;
        ChangeHouseKeeperRoutine();

    }

    public void RightChangeHouseKeeper() {
        chosenIndex = (chosenIndex == HouseKeeperSystem.GetKeeperCount() - 1) ? 0 : chosenIndex + 1;
        ChangeHouseKeeperRoutine();
    }

    private void ChangeHouseKeeperRoutine() {
        bool got = HouseKeeperSystem.GetGotByIndex(chosenIndex);
        ChosenHouseKeeper.sprite = (got) ? HouseKeeperSystem.GetSpriteByIndex(chosenIndex) : unknownHK;
        ChosenHouseKeeperName.text = (got) ? HouseKeeperSystem.GetNameByIndex(chosenIndex) : "？？？";
        HouseKeeperSkill sk = HouseKeeperSystem.GetSkillByIndex(chosenIndex);
        ChosenHouseKeeperSkill.text = (got) ? (sk.skill.ToString() + ((sk.percentage > 0) ? "+" : "") + sk.percentage.ToString() + "%") : "";
        GoButton.interactable = got;
    }

    public void Go() {
        SystemVariables.種植開始時間 = DateTime.Now;
        SystemVariables.currentHKindex = chosenIndex;
        CheckPlantTime();
    }

    private void CheckPlantTime() {
        TimeSpan diff = DateTime.Now.Subtract(SystemVariables.種植開始時間);
        if (diff.TotalSeconds >= 種植時長) {
            if(SystemVariables.plantStatus == PlantStatus.田園) {
                田園Button.SetActive(true);
                收成Button.SetActive(false);
                FieldButton.interactable = true;
            }
            else if(SystemVariables.plantStatus == PlantStatus.種植中) {
                田園Button.SetActive(false);
                收成Button.SetActive(true);
                SystemVariables.plantStatus = PlantStatus.收成;
            }
            種植中Button.SetActive(false);
        }
        else {
            田園Button.SetActive(false);
            FieldButton.interactable = false;
            種植中Button.SetActive(true);
            SystemVariables.plantStatus = PlantStatus.種植中;
            int leftTime = 種植時長 - (int)diff.TotalSeconds;
            種植中時間Text.text = ((leftTime < 600) ? "0" : "") + (leftTime / 60).ToString() + ":" + ((leftTime % 60 < 10) ? "0" : "") + (leftTime % 60).ToString();
        }
    }

    public void 收成() {
        BigCarrot.gameObject.SetActive(true);
        BigCarrot.anchoredPosition = new Vector2(BigCarrot.anchoredPosition.x, originalY);
        
    }

    public void ClickCarrot() {
        BigCarrot.anchoredPosition = new Vector2(BigCarrot.anchoredPosition.x, BigCarrot.anchoredPosition.y + 收成每次點擊長高多少);
        if(BigCarrot.anchoredPosition.y >= finalY) {
            BigCarrot.gameObject.SetActive(false);
            //carrot animation
            StartCoroutine(CarrotAnimation(10, 0.1f));
        }
    }

    private IEnumerator CarrotAnimation(int carrotCount, float generateTimeGap) {
        for(int i = 0; i < carrotCount - 1; i++) {
            GenerateSmallCarrotByDotween();
            yield return new WaitForSeconds(generateTimeGap);
        }
        GenerateSmallCarrotByDotween();
        yield return new WaitForSeconds(0.5f);
        SystemVariables.plantStatus = PlantStatus.田園;
        HarvestAni.SetTrigger("CloseHarvest");
        CloseDetect_Harvest.SetActive(false);
        FieldCanvas.sortingOrder = 0;
        CheckPlantTime();
    }

    private void GenerateSmallCarrotByDotween() {
        GameObject g = Instantiate(smallCarrotPrefab, transform);
        RectTransform gRT = g.GetComponent<RectTransform>();
        DOTween.To(() => { return gRT.anchoredPosition; }, v => { gRT.anchoredPosition = v; }, new Vector2(15, 658), 0.5f).OnComplete(() => { Destroy(g); SystemVariables.CarrotCount += 1; });
    }
}
