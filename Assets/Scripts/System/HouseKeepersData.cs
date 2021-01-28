using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseKeepersData")]
public class HouseKeepersData : ScriptableObject{
    public List<OneHouseKeeper> houseKeeperList = new List<OneHouseKeeper>();
}

[System.Serializable]
public class OneHouseKeeper {
    public string name;
    public Sprite sprite;
    public bool got;
    public HouseKeeperSkill skill;
    public Sprite Hint;
    public string skillStr;
    public string description;
}

public enum HouseKeeperSkillType {
    獲得紅蘿蔔量,
    花費時間,
}

[System.Serializable]
public class HouseKeeperSkill {
    public HouseKeeperSkillType skill;
    public int percentage;
}
