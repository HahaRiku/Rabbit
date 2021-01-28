using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageTransition : MonoBehaviour {
    
    public void To小屋() {
        if(SceneManager.GetActiveScene().name != "MainPage") {
            SceneManager.LoadScene("MainPage");
        }
    }

    public void To開發() {
        if(SceneManager.GetActiveScene().name != "Kitchen") {
            SceneManager.LoadScene("Kitchen");
        }
    }

    public void To圖鑑() {
        if(SceneManager.GetActiveScene().name != "Collections") {
            SceneManager.LoadScene("Collections");
        }
    }
}
