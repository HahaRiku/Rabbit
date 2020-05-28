using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RabbitsData")]
public class RabbitsData : ScriptableObject {
    public List<OneRabbit> rabbitsList = new List<OneRabbit>();
}

[System.Serializable]
public class OneRabbit {
    public string name;
    public Method method;
    public Sprite image;
    public Demand[] demands;
    public bool got;
}

[System.Serializable]
public class Demand {
    public int percentage;
    public int 外觀Demand;
    public int 口感Demand;
    public int 香氣Demand;
    public int 經典Demand;
}

[System.Serializable]
public enum Method {
    Refrigerator,
    Oven
}
