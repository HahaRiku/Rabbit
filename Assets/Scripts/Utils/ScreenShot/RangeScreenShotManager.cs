using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RangeScreenShotManager : MonoBehaviour {

    private RectTransform Range;

    // Start is called before the first frame update
    void Start() {
        Range = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ConfirmScreenshot() {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare() {
        yield return new WaitForEndOfFrame();

        int width = (int)Range.sizeDelta.x;
        int height = (int)Range.sizeDelta.y;
        float x = Range.anchoredPosition.x - Range.sizeDelta.x / 2;
        float y = (Screen.height - Range.anchoredPosition.y) - Range.sizeDelta.y / 2;
        Texture2D ss = new Texture2D(width, height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(x, y, width, height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "sharedImg.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
