using System.Collections;
using Game.Player;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class DisplayFirstText : MonoBehaviour, IGameStart {
        private Text text;
        private float displayTime = 0.5f;
        private void Start() {
            text = GetComponent<Text>();
            StartCoroutine(Flashing());
        }

        IEnumerator Flashing() {
            var color = text.color;
            while (true) {
                yield return new WaitForSeconds(displayTime);
                text.color = new Color(color.r, color.g, color.b, 0.01f);
                yield return new WaitForSeconds(displayTime);
                text.color = color;
            }
        }

        public void StartGame(PlayerStatusManager player) {
            Destroy(gameObject);
        }
    }
}
