using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HouseKeeperSystem {
    public static HouseKeepersData data = Resources.Load<HouseKeepersData>("HouseKeepersData");

    public static Sprite GetSpriteByIndex(int index) {
        return data.houseKeeperList[index].sprite;
    }

    public static bool GetGotByIndex(int index) {
        return data.houseKeeperList[index].got;
    }

    public static bool GetGotByName(string name) {
        foreach (OneHouseKeeper ohk in data.houseKeeperList) {
            if (name == ohk.name) {
                return ohk.got;
            }
        }
        Debug.Log("GetGorByName傳入不存在的管家名稱：" + name);
        return false;
    }

    public static int GetGotNumber() {
        int count = 0;
        foreach (OneHouseKeeper hk in data.houseKeeperList) {
            if (hk.got) {
                count++;
            }
        }
        return count;
    }

    public static string GetNameByIndex(int index) {
        return data.houseKeeperList[index].name;
    }

    public static HouseKeeperSkill GetSkillByIndex(int index) {
        return data.houseKeeperList[index].skill;
    }

    public static Sprite GetHintByIndex(int index) {
        return data.houseKeeperList[index].Hint;
    }
}
