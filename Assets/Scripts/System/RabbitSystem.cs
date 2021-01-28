using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RabbitSystem {
    private static RabbitsData data = Resources.Load<RabbitsData>("RabbitsData");
    private static int loveCount = 0;

    public const int maxLoveCount = 2;

    public static void SetRabbitGotOrNot(string name, bool got) {
        foreach(OneRabbit rabbit in data.rabbitsList) {
            if(name == rabbit.name) {
                rabbit.got = got;
                break;
            }
        }
    }

    public static Demand[] GetDemands(string name) {
        foreach(OneRabbit r in data.rabbitsList) {
            if(name == r.name) {
                return r.demands;
            }
        }
        Debug.Log("Error: 要求尋找的Demand的名稱不存在");
        return null;
    }

    public static Demand[] GetDemandsById(int index) {
        if (index > data.rabbitsList.Count) {
            Debug.Log("[Error][GetDemandsById] index: " + index + "超出範圍");
            return null;
        }
        return data.rabbitsList[index].demands;
    }

    public static bool GetRabbitGotByName(string name) {
        foreach(OneRabbit rabbit in data.rabbitsList) {
            if(rabbit.name == name) {
                return rabbit.got;
            }
        }
        Debug.Log("[Error][GetRabbitGotByName] 要求尋找的Rabbit name不存在");
        return false;
    }

    public static bool GetRabbitGotById(int index) {
        if(index > data.rabbitsList.Count) {
            Debug.Log("[Error][GetRabbitGotById] index: " + index + "超出範圍");
            return false;
        }
        return data.rabbitsList[index].got;
    }

    public static Sprite GetRabbitSpriteByName(string name) {
        foreach(OneRabbit rabbit in data.rabbitsList) {
            if(rabbit.name == name) {
                return rabbit.image;
            }
        }
        Debug.Log("[Error][GetRabbitSpriteByName] 要求尋找的Rabbit name: " + name + " 不存在");
        return null;
    }

    public static Sprite GetRabbitSpriteById(int index) {
        if (index > data.rabbitsList.Count) {
            Debug.Log("[Error][GetRabbitSpriteById] index: " + index + "超出範圍");
            return null;
        }
        return data.rabbitsList[index].image;
    }

    public static string GetRabbitNameById(int index) {
        if(index > data.rabbitsList.Count) {
            Debug.Log("[Error][GetRabbitNameById] index: " + index + "超出範圍");
            return "";
        }
        return data.rabbitsList[index].name;
    }

    public static Method GetRabbitMethodById(int index) {
        if (index > data.rabbitsList.Count) {
            Debug.Log("[Error][GetRabbitMethodById] index: " + index + "超出範圍");
            return Method.Oven;
        }
        return data.rabbitsList[index].method;
    }

    public static bool GetRabbitLoveById(int index) {
        if(index > data.rabbitsList.Count) {
            Debug.Log("[Error][GetRabbitLoveById] index: " + index + "超出範圍");
            return false;
        }
        return data.rabbitsList[index].love;
    }

    public static void SetRabbitLoveById(int index, bool love) {
        if (index > data.rabbitsList.Count) {
            Debug.Log("[Error][SetRabbitLoveById] index: " + index + "超出範圍");
            return;
        }
        data.rabbitsList[index].love = love;
        loveCount += love ? 1 : -1;
    }

    public static string GetRabbitDescById(int index) {
        if(index > data.rabbitsList.Count) {
            Debug.Log("[Error][SetRabbitLoveById] index: " + index + "超出範圍");
            return "";
        }
        return data.rabbitsList[index].description;
    }

    public static int GetRabbitLoveCount() {
        return loveCount;
    }

    public static int GetRabbitListCount() {
        return data.rabbitsList.Count;
    }

    public static void Reset() {
        foreach(OneRabbit rabbit in data.rabbitsList) {
            rabbit.got = false;
            rabbit.love = false;
            loveCount = 0;
        }
    }

    public static void CalculateRabbitLoveCount() {
        int count = 0;
        foreach(OneRabbit rabbit in data.rabbitsList) {
            if(rabbit.love) {
                count++;
            }
        }
        loveCount = count;
    }
}
