using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RabbitSystem {
    public static RabbitsData data = Resources.Load<RabbitsData>("RabbitsData");

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

    public static void Reset() {
        foreach(OneRabbit rabbit in data.rabbitsList) {
            rabbit.got = false;
        }
    }
}
