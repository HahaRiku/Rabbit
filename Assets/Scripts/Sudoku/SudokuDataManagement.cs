using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SudokuDataManagement {
    public static SudokuData data_easy = Resources.Load<SudokuData>("SudokuData_easy");
    public static SudokuData data_normal = Resources.Load<SudokuData>("SudokuData_normal");
    public static int notPlayedSudokuNum_easy = data_easy.sudokus.Length;
    public static int notPlayedSudokuNum_normal = data_normal.sudokus.Length;

    public static int GetQuestionIndex(SudokuDifficulty difficulty) {
        if(difficulty == SudokuDifficulty.Easy) {
            int randomNum = Random.Range(0, notPlayedSudokuNum_easy);
            int count = 0;
            for (int i = 0; i < data_easy.sudokus.Length; i++) {
                if (!data_easy.sudokus[i].played) {
                    if (count == randomNum) {
                        return i;
                    }
                    else {
                        count++;
                    }
                }
            }
            Debug.Log("Something wrong in GetQuestionIndex of SudokuDataManagement");
            return 0;
        }
        else {
            int randomNum = Random.Range(0, notPlayedSudokuNum_normal);
            int count = 0;
            for (int i = 0; i < data_normal.sudokus.Length; i++) {
                if (!data_normal.sudokus[i].played) {
                    if (count == randomNum) {
                        return i;
                    }
                    else {
                        count++;
                    }
                }
            }
            Debug.Log("Something wrong in GetQuestionIndex of SudokuDataManagement");
            return 0;
        }
    }

    public static void SetIndexPlayed(int index, SudokuDifficulty difficulty) {
        if(difficulty == SudokuDifficulty.Easy) {
            data_easy.sudokus[index].played = true;
            notPlayedSudokuNum_easy--;
        }
        else {
            data_normal.sudokus[index].played = true;
            notPlayedSudokuNum_normal--;
        }
    }

    public static void FlushPlayedBoolean(SudokuDifficulty difficulty) {
        if(difficulty == SudokuDifficulty.Easy) {
            foreach (Sudoku sudoku in data_easy.sudokus) {
                sudoku.played = false;
            }
        }
        else {
            foreach (Sudoku sudoku in data_easy.sudokus) {
                sudoku.played = false;
            }
        }
    }

    public static void AddSudoku(Sudoku sudoku, SudokuDifficulty difficulty) {
        if(difficulty == SudokuDifficulty.Easy) {
            Sudoku[] temp = new Sudoku[data_easy.sudokus.Length + 1];
            for(int i = 0; i < data_easy.sudokus.Length; i++) {
                temp[i] = data_easy.sudokus[i];
            }
            temp[data_easy.sudokus.Length] = sudoku;
            data_easy.sudokus = temp;
            EditorUtility.SetDirty(data_easy);
        }
        else {
            Sudoku[] temp = new Sudoku[data_normal.sudokus.Length + 1];
            for (int i = 0; i < data_normal.sudokus.Length; i++) {
                temp[i] = data_normal.sudokus[i];
            }
            temp[data_normal.sudokus.Length] = sudoku;
            data_normal.sudokus = temp;
            EditorUtility.SetDirty(data_normal);
        }
    }

    public static bool GetExistedInQues(SudokuDifficulty difficulty, int sudokuIndex, int rowNum, int colNum) {
        if(difficulty == SudokuDifficulty.Easy) {
            return data_easy.sudokus[sudokuIndex].sudoku[rowNum].row[colNum].existedInQues;
        }
        else {
            return data_normal.sudokus[sudokuIndex].sudoku[rowNum].row[colNum].existedInQues;
        }
    }

    public static int GetNumber(SudokuDifficulty difficulty, int sudokuIndex, int rowNum, int colNum) {
        if(difficulty == SudokuDifficulty.Easy) {
            return data_easy.sudokus[sudokuIndex].sudoku[rowNum].row[colNum].number;
        }
        else {
            return data_normal.sudokus[sudokuIndex].sudoku[rowNum].row[colNum].number;
        }
    }
}

public enum SudokuDifficulty {
    Easy, 
    Normal
}
