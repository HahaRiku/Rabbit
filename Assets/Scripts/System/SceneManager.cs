using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager {
    public static string LastScene { get; private set; }

    public static void ChangingScene(string sceneName) {
        LastScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public static UnityEngine.SceneManagement.Scene GetActiveScene() {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }
}

