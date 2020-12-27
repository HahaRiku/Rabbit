using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuGM : MonoBehaviour {

    public GameObject DifficultyCanvas;
    public GameObject GameCanvas;
    public Sprite[] 一到九圖案;
    public Sprite 正確;
    public Sprite 失敗;
    public Sprite Transparent;
    public Sprite QuestionPanel;
    public Sprite SelectedPanel;

    private GameObject[,] grids = new GameObject[9, 9];
    private Image[,] images = new Image[9, 9];
    private int[,] recordedNumbers = new int[9, 9];
    private SudokuDifficulty difficulty;
    private bool start = false;
    private int sudokuIndex;

    private int chosenGridPosRow;
    private int chosenGridPosCol;

    private GameObject EndPanel;
    private Image EndImage;

    // Start is called before the first frame update
    void Start() {
        GameObject Question = GameCanvas.transform.GetChild(0).GetChild(0).gameObject;
        for(int i = 0; i < 9; i++) {
            for(int j = 0; j < 9; j++) {
                if (i >= 0 && i <= 2) {
                    GameObject threeNine = Question.transform.GetChild(0).gameObject;
                    if(j >= 0 && j <= 2) {
                        grids[i, j] = threeNine.transform.GetChild(0).GetChild(i).GetChild(j).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                    else if (j >= 3 && j <= 5) {
                        grids[i, j] = threeNine.transform.GetChild(1).GetChild(i).GetChild(j - 3).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                    else if (j >= 6 && j <= 8) {
                        grids[i, j] = threeNine.transform.GetChild(2).GetChild(i).GetChild(j - 6).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                }
                else if(i >= 3 && i <= 5) {
                    GameObject threeNine = Question.transform.GetChild(1).gameObject;
                    if (j >= 0 && j <= 2) {
                        grids[i, j] = threeNine.transform.GetChild(0).GetChild(i - 3).GetChild(j).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                    else if (j >= 3 && j <= 5) {
                        grids[i, j] = threeNine.transform.GetChild(1).GetChild(i - 3).GetChild(j - 3).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                    else if (j >= 6 && j <= 8) {
                        grids[i, j] = threeNine.transform.GetChild(2).GetChild(i - 3).GetChild(j - 6).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                }
                else if(i >= 6 && i <= 8) {
                    GameObject threeNine = Question.transform.GetChild(2).gameObject;
                    if (j >= 0 && j <= 2) {
                        grids[i, j] = threeNine.transform.GetChild(0).GetChild(i - 6).GetChild(j).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                    else if (j >= 3 && j <= 5) {
                        grids[i, j] = threeNine.transform.GetChild(1).GetChild(i - 6).GetChild(j - 3).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                    else if (j >= 6 && j <= 8) {
                        grids[i, j] = threeNine.transform.GetChild(2).GetChild(i - 6).GetChild(j - 6).GetChild(1).gameObject;
                        images[i, j] = grids[i, j].GetComponent<Image>();
                    }
                }
            }
        }

        DifficultyCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        EndPanel = GameCanvas.transform.GetChild(1).gameObject;
        EndImage = EndPanel.transform.GetChild(1).GetComponent<Image>();    
        Initialization();
    }

    // Update is called once per frame
    void Update() {
        if(start) {
            start = false;
            ClearAllGrids();
            sudokuIndex = SudokuDataManagement.GetQuestionIndex(difficulty);
            AssignQuestion();
        }
    }

    public void SetDifficulty(int d) {
        difficulty = (SudokuDifficulty)d;

        DifficultyCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        start = true;
    }

    public void ChooseGrid(GameObject g) {
        int tempRow = -1, tempCol = -1;
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                if (grids[i, j] == g) {
                    tempRow = i;
                    tempCol = j;
                }
            }
        }
        if (!SudokuDataManagement.GetExistedInQues(difficulty, sudokuIndex, tempRow, tempCol)) {
            if (chosenGridPosRow != -1) {
                grids[chosenGridPosRow, chosenGridPosCol].transform.parent.GetChild(0).gameObject.SetActive(false);
            }

            g.transform.parent.GetChild(0).gameObject.SetActive(true);
            chosenGridPosRow = tempRow;
            chosenGridPosCol = tempCol;
        }
    }

    public bool IsGridBeenChosen() {
        return (chosenGridPosRow != -1) ? true : false;
    }

    public void ClickNumber(int number) {   //0 represent eraser
        if(IsGridBeenChosen()) {
            if(number != 0) {
                images[chosenGridPosRow, chosenGridPosCol].sprite = 一到九圖案[number - 1];
                recordedNumbers[chosenGridPosRow, chosenGridPosCol] = number;
            }
            else {
                images[chosenGridPosRow, chosenGridPosCol].sprite = null;
                recordedNumbers[chosenGridPosRow, chosenGridPosCol] = 0;
            }
            grids[chosenGridPosRow, chosenGridPosCol].transform.parent.GetChild(0).gameObject.SetActive(false);
            chosenGridPosRow = -1;
            chosenGridPosCol = -1;
        }
    }

    public void ClickConfirm() {
        if (chosenGridPosRow != -1) {
            grids[chosenGridPosRow, chosenGridPosCol].transform.parent.GetChild(0).gameObject.SetActive(false);
        }
        chosenGridPosRow = -1;
        chosenGridPosCol = -1;
        for (int i = 0; i < 9; i++) {
            for(int j = 0; j < 9; j++) {
                if(recordedNumbers[i, j] != SudokuDataManagement.GetNumber(difficulty, sudokuIndex, i, j)) {
                    EndImage.sprite = 失敗;
                    EndPanel.SetActive(true);
                    return;
                }
            }
        }
        EndImage.sprite = 正確;
        EndPanel.SetActive(true);
        SudokuDataManagement.SetIndexPlayed(sudokuIndex, difficulty);
    }

    private void ClearAllGrids() {
        for(int i = 0; i < 9; i++) {
            for(int j = 0; j < 9; j++) {
                images[i, j].sprite = Transparent;
                GameObject tempG = images[i, j].transform.parent.GetChild(0).gameObject;
                tempG.SetActive(false);
                tempG.GetComponent<Image>().sprite = SelectedPanel;
            }
        }
        recordedNumbers = new int[9, 9];
    }

    public void Restart() {
        Initialization();
        ClearAllGrids();
        AssignQuestion();
    }

    public void Next() {
        ClearAllGrids();
        Initialization();
        start = true;
    }

    private void AssignQuestion() {
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                int number = SudokuDataManagement.GetNumber(difficulty, sudokuIndex, i, j);
                if (SudokuDataManagement.GetExistedInQues(difficulty, sudokuIndex, i, j)) {
                    images[i, j].sprite = 一到九圖案[number - 1];
                    GameObject tempG = images[i, j].transform.parent.GetChild(0).gameObject;
                    tempG.SetActive(true);
                    tempG.GetComponent<Image>().sprite = QuestionPanel;
                    recordedNumbers[i, j] = number;
                }
                else {
                    recordedNumbers[i, j] = 0;
                }
            }
        }
    }

    private void Initialization() {
        chosenGridPosRow = -1;
        chosenGridPosCol = -1;
        EndPanel.SetActive(false);
    }
}
