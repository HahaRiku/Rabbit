using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour {
    private Animator ani;
    private bool aniDone = true;
    private bool open = false;
    
    // Start is called before the first frame update
    void Start() {
        ani = GetComponent<Animator>();
        aniDone = true;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OpenSettingUI() {
        if(aniDone && !open) {
            ani.SetTrigger("Open");
            StartCoroutine(Waiting());
            open = true;
        }
    }

    public void CloseSettingUI() {
        if(aniDone && open) {
            ani.SetTrigger("Close");
            StartCoroutine(Waiting());
            open = false;
        }
    }

    private IEnumerator Waiting() {
        aniDone = false;
        yield return new WaitForSeconds(0.5f);
        aniDone = true;
    }
}
