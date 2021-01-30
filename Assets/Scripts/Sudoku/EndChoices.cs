using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndChoices : MonoBehaviour {

    public void OtherDifficulties() {
        SceneManager.ChangingScene("Sudoku");
    }

    public void Home() {
        SceneManager.ChangingScene("MainPage");
    }
}
