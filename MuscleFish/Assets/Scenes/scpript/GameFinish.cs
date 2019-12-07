using System.Collections;
using System.Collections.Generic;
using Game.System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour {
    public void OnRetry() {
        SceneManager.LoadScene(GameController.GameMode == GameMode.TimeAttack ? 1 : 3);
    }

    public void OnTitle() {
        SceneManager.LoadScene(0);
    }
}
