using System;
using Game.Player;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class DisplayTime : MonoBehaviour, IGameStart ,IGameEnd {
        private Text text;
        private bool isGaming;

        private void Start() {
            text = GetComponent<Text>();
        }

        void Update() {
            if (!isGaming) return;
            text.text = $"{GameController.Instance.GameTime:00.00}";
        }

        public void StartGame(PlayerStatusManager player) {
            isGaming = true;
        }
        

        public void EndGame() {
            Destroy(gameObject);
        }
    }
}
