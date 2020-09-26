using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCameraIconAniDone : MonoBehaviour {
    public void StartWait() {
        StartCoroutine(WaitAniDone());
    }

    IEnumerator WaitAniDone() {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
