using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllScreenShotManager : MonoBehaviour {
    private ResultManager resultManager = null;

    void Start() {
        resultManager = transform.GetChild(3).GetComponent<ResultManager>();
    }
    
    public void TakeScreenShot() {
        //TODO: close all of the ui

        StartCoroutine(ScreenShot());
    }

    IEnumerator ScreenShot() {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        resultManager.OpenResult(ss);
    }
}
