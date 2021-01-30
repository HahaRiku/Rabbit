using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTransition : MonoBehaviour {
    
    public void To小屋() {
        if(SceneManager.GetActiveScene().name != "MainPage") {
            SceneManager.ChangingScene("MainPage");
        }
    }

    public void To開發() {
        if(SceneManager.GetActiveScene().name != "Kitchen") {
            SceneManager.ChangingScene("Kitchen");
        }
    }

    public void To圖鑑() {
        if(SceneManager.GetActiveScene().name != "Collections") {
            SceneManager.ChangingScene("Collections");
        }
    }
}
