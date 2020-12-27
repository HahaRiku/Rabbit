using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGrid : MonoBehaviour {
    private SudokuGM GM;
    private GameObject Kuang;
    // Start is called before the first frame update
    void Start() {
        GM = FindObjectOfType<SudokuGM>();
        Kuang = transform.parent.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Click() {
        GM.ChooseGrid(gameObject);
    }
}
