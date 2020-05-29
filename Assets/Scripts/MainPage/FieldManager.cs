﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldManager : MonoBehaviour {
    public Sprite unknownHK;
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
        CheckPlantTime();
    }

    // Update is called once per frame
    void Update() {
        if(SystemVariables.plantStatus == PlantStatus.種植中) {
            CheckPlantTime();
        }
    }

    public void LeftChangeHouseKeeper() {
        chosenIndex = (chosenIndex == 0) ? HouseKeeperSystem.data.houseKeeperList.Count - 1 : chosenIndex - 1;
        ChangeHouseKeeperRoutine();

    }

    public void RightChangeHouseKeeper() {
        chosenIndex = (chosenIndex == HouseKeeperSystem.data.houseKeeperList.Count - 1) ? 0 : chosenIndex + 1;
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
            種植中Button.SetActive(true);
            SystemVariables.plantStatus = PlantStatus.種植中;
            int leftTime = 種植時長 - (int)diff.TotalSeconds;
            種植中時間Text.text = ((leftTime < 600) ? "0" : "") + (leftTime / 60).ToString() + ":" + ((leftTime % 60 < 10) ? "0" : "") + (leftTime % 60).ToString();
        }
    }
}
