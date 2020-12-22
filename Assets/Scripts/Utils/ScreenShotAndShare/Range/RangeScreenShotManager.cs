using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RangeScreenShotManager : MonoBehaviour {

    private RectTransform Range;
    private ResultManager resultManager;
    private RectTransform screenshotCanvasRT;

    // Start is called before the first frame update
    void Start() {
        Range = transform.GetChild(0).GetComponent<RectTransform>();
        resultManager = transform.parent.GetChild(3).GetComponent<ResultManager>();
        screenshotCanvasRT = transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ConfirmScreenshot() {
        StartCoroutine(TakeScreenshot());
    }

    private IEnumerator TakeScreenshot() {
        yield return new WaitForEndOfFrame();

        int width = (int)(Range.sizeDelta.x * (Screen.width / screenshotCanvasRT.sizeDelta.x));
        int height = (int)(Range.sizeDelta.y * (Screen.height / screenshotCanvasRT.sizeDelta.y));
        float x = (Range.anchoredPosition.x - Range.sizeDelta.x / 2) * (Screen.width / screenshotCanvasRT.sizeDelta.x);
        float y = (Range.anchoredPosition.y - Range.sizeDelta.y / 2) * (Screen.height / screenshotCanvasRT.sizeDelta.y);
        Texture2D ss = new Texture2D(width, height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(x, y, width, height), 0, 0);
        ss.Apply();

        //歸位
        Range.sizeDelta = new Vector2(100, 100);
        Range.anchoredPosition = new Vector2(180, 360);

        resultManager.OpenResult(ss);        

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
