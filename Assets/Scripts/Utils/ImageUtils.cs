using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUtils {

    public static void FittingImg(RectTransform rt, Image img, Sprite sprite, Vector2 defaultSize) {
        if (sprite.texture.height / sprite.texture.width >= defaultSize.y / defaultSize.x) { //以高為基準
            rt.sizeDelta = new Vector2(sprite.texture.width * defaultSize.y / sprite.texture.height, defaultSize.y);
        }
        else {  //以寬為基準
            rt.sizeDelta = new Vector2(defaultSize.x, sprite.texture.height * defaultSize.x / sprite.texture.width);
        }
        img.sprite = sprite;
    }
}
