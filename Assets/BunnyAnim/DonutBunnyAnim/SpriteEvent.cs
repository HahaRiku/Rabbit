using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteEvent : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float holdTime;
    private bool isBeingHeld;
    private bool isClick = false;
    private bool startdrag = false;
    private bool dropping;
    private bool walkdone = true;///
    private float curT;
    private float maxX = 0.694f;
    private float minX = -1.767f;
    private float maxY = 3.163f;
    private float minY = -1.2f;
    Sequence walkSequence;

    //Random random = new Random();
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = transform.parent.GetComponent<Animator>();
        //r = transform.parent.GetComponent<Rigidbody2D>();
        //r.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curT = Time.time;
        if (isBeingHeld == true) {
            if(curT - holdTime >= 0.1) {
                isClick = false;

                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                if (startdrag == false) {
                    startdrag = true;
                    ani.SetTrigger("Drag");
                    Debug.Log("Drag");
                    walkSequence.Kill();
                }
                
                transform.parent.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
            }
        }
        if(dropping == false && isClick == false && isBeingHeld == false && walkdone == true) {
            walkdone = false;//still walking
            Vector3 nextpos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
            walkSequence = DOTween.Sequence();
            walkSequence.Append(transform.parent.DOMove(nextpos, 6).OnComplete(() => { walkdone = true; }));
        }
    }

    private void OnMouseDown() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        startPosX = mousePos.x - transform.parent.localPosition.x;
        startPosY = mousePos.y - transform.parent.localPosition.y;

        holdTime = Time.time;

        isBeingHeld = true;
        isClick = true;
        
    }

    void OnMouseUp() {
        //if (isBeingHeld) {
            isBeingHeld = false;
            startdrag = false;
            if (isClick == true) {
                ani.SetTrigger("Click");
                Debug.Log("Click");
            }
            else {
                ani.GetComponent<Animator>().enabled = true;
                Debug.Log("Drop");

                if (transform.parent.localPosition.y > 0.349) {
                    //r.gravityScale = 1;
                    dropping = true;
                    transform.parent.DOMoveY(-0.111f, 0.78f).SetEase(Ease.InCubic).OnComplete(() => { dropping = false; });
                }
                //r.useGravity = false;
            }
            walkdone = true;///
        //}
        
    }
    /*
    private void OnMouseExit() {
        isBeingHeld = false;
    }
    */
    IEnumerator RestTime() {
        walkdone = false;
        yield return new WaitForSeconds(Random.Range(0, 6));
        walkdone = true;

    }
}
