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
    private bool isClick = true;
    private bool startdrag = false;
    //private Rigidbody2D r;
    private bool dropping;
    float curT;

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

                if(startdrag == false) {
                    startdrag = true;
                    ani.SetTrigger("Drag");
                    Debug.Log("Drag");
                }

                transform.parent.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
            }
        }
        //if (dropping == true && transform.parent.localPosition.y <= 0.02) {
        //    r.gravityScale = 0;
        //}
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
        isBeingHeld = false;
        startdrag = false;
        if (isClick == true) {
            ani.SetTrigger("Click");
        }
        else {
            ani.GetComponent<Animator>().enabled = true;
            ani.SetTrigger("Drop");
            Debug.Log("Drop");
            
            if(transform.parent.localPosition.y > 0.349) {
                //r.gravityScale = 1;
                dropping = true;
                transform.parent.DOMoveY(-0.111f, 1).SetEase(Ease.InCubic);
            }
            //r.useGravity = false;
        }
    }

    
}
