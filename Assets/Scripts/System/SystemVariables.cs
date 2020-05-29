using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SystemVariables {
    public static int CarrotCount;
    public static int waiguanValue;
    public static int koganValue;
    public static int xianchiValue;
    public static int classicValue;
    public static DateTime 種植開始時間 = new DateTime(2020, 5, 28, 23, 0, 0);
    public static int currentHKindex;
    public static PlantStatus plantStatus = PlantStatus.田園;

    public static void CheckCarrotCount() {
        if(CarrotCount > 9999999) {
            CarrotCount = 9999999;
        }
    }
}

public static class Init {
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad() {
        SystemVariables.CarrotCount = 0;
        SystemVariables.waiguanValue = 1;
        SystemVariables.koganValue = 1;
        SystemVariables.xianchiValue = 1;
        SystemVariables.classicValue = 1;
        RabbitSystem.Reset();
    }
}

public enum PlantStatus {
    田園,
    種植中,
    收成
}
