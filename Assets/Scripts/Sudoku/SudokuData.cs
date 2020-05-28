using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SudokuData")]
public class SudokuData : ScriptableObject {
    public Sudoku[] sudokus;
}

[System.Serializable]
public class Sudoku {
    public SudokuRow[] sudoku = new SudokuRow[9];
    public bool played = false;
}

[System.Serializable]
public class SudokuRow {
    public OneNumber[] row = new OneNumber[9];
}

[System.Serializable]
public class OneNumber {
    public int number;
    public bool existedInQues;
}