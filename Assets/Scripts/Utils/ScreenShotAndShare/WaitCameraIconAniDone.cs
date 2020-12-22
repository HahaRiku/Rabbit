using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCameraIconAniDone : MonoBehaviour {
    private bool isAllScreenShot = false;
    private AllScreenShotManager allScreenShotManager = null;

    void Start() {
        allScreenShotManager = transform.parent.parent.GetComponent<AllScreenShotManager>();
    }

    public void StartWait_Normal() {
        isAllScreenShot = false;
        StartCoroutine(WaitAniDone());
    }

    public void StartWait_AllScreenShot() {
        isAllScreenShot = true;
        StartCoroutine(WaitAniDone());
    }

    IEnumerator WaitAniDone() {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        if(isAllScreenShot) {
            allScreenShotManager.TakeScreenShot();
        }
    }
}
