using System.Collections;
using Game.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Title {
    public class StartGame : MonoBehaviour {
        [SerializeField] private Image image;

        private bool isOn;
        //　スタートボタンを押したら実行する
        public void LoadGame() {
            GameController.GameMode = GameMode.TimeAttack;
            SceneManager.LoadScene(1);
        }

        public void LoadEndless() {
            GameController.GameMode = GameMode.Endless;
            SceneManager.LoadScene(3);
        }

        public void Display() {
            if(isOn) return;
            image.gameObject.SetActive(true);
            StartCoroutine(WaitClose());
            isOn = true;
        }

        private IEnumerator WaitClose() {
            yield return null;
            while (!Input.anyKeyDown) {
                yield return null;
            }
            image.gameObject.SetActive(false);
            isOn = false;
        }
    }
}
