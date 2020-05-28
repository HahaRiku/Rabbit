using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataImporter : MonoBehaviour {

    public SudokuDifficulty 匯入難度;

    // Start is called before the first frame update
    void Start() {
        string text;
        if(匯入難度 == SudokuDifficulty.Easy) {
            text = File.ReadAllText(Application.dataPath + "\\素材\\SudokuData\\easy.txt");
        }
        else {
            text = File.ReadAllText(Application.dataPath + "\\素材\\SudokuData\\normal.txt");
        }
        bool putAnswerTime = true;
        int rowNum = 0;
        Sudoku sudoku = new Sudoku {
            sudoku = new SudokuRow[9],
            played = false
        };
        SudokuRow row = new SudokuRow {
            row = new OneNumber[9]
        };
        int columnIndex = 0;
        for (int i = 0; i < text.Length; i++) {
            if(text[i] == '\r') {
                i++;
                if(putAnswerTime) {
                    sudoku.sudoku[rowNum] = row;
                    row = new SudokuRow {
                        row = new OneNumber[9]
                    };
                }
                else {
                    if(columnIndex != 9) {
                        for (int j = columnIndex; j < 9; j++) {
                            sudoku.sudoku[rowNum].row[j].existedInQues = false;
                        }
                    }
                    columnIndex = 0;
                }
                rowNum++;
                if (rowNum == 9) {
                    rowNum = 0;
                    if (!putAnswerTime) {
                        //put final sudoku into data
                        SudokuDataManagement.AddSudoku(sudoku, 匯入難度);

                        sudoku = new Sudoku {
                            sudoku = new SudokuRow[9],
                            played = false
                        };
                    }
                    putAnswerTime = !putAnswerTime;
                }
            }
            else {
                if(putAnswerTime) { //put answer
                    row.row[columnIndex] = new OneNumber {
                        number = text[i] - 48,
                        existedInQues = false
                    };
                    columnIndex = (columnIndex == 8) ? 0 : columnIndex + 1;
                }
                else {  //indicate the question
                    int checkIndex = columnIndex;
                    while(/*text[i] != '\n' && */sudoku.sudoku[rowNum].row[checkIndex].number != text[i] - 48) {
                        sudoku.sudoku[rowNum].row[checkIndex].existedInQues = false;
                        checkIndex++;
                    }
                    sudoku.sudoku[rowNum].row[checkIndex].existedInQues = true;
                    columnIndex = checkIndex + 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
