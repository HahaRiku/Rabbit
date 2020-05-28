using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : MonoBehaviour {
    public Sprite unknownHK;
    private Image ChosenHouseKeeper;
    private Text ChosenHouseKeeperName;
    private Text ChosenHouseKeeperSkill;
    private int chosenIndex;
    private Button GoButton;

    // Start is called before the first frame update
    void Start() {
        chosenIndex = 0;
        GameObject ChangeHouseKeeper = transform.GetChild(2).GetChild(2).gameObject;
        ChosenHouseKeeper = ChangeHouseKeeper.transform.GetChild(0).GetComponent<Image>();
        ChosenHouseKeeperName = ChangeHouseKeeper.transform.GetChild(3).GetComponent<Text>();
        ChosenHouseKeeperSkill = ChangeHouseKeeper.transform.GetChild(4).GetComponent<Text>();
        GoButton = transform.GetChild(2).GetChild(3).GetComponent<Button>();
        ChangeHouseKeeperRoutine();
    }

    // Update is called once per frame
    void Update() {
        
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

    }
}
