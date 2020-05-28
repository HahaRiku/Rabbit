using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemVariables {
    public static int CarrotCount;
    public static int waiguanValue;
    public static int koganValue;
    public static int xianchiValue;
    public static int classicValue;

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
