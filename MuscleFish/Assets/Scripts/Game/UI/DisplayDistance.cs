using Game.Player;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class DisplayDistance : MonoBehaviour,IGameStart ,IGameEnd {
        private Text text;
        private PlayerStatusManager player;
        private bool isGaming;

        private void Start() {
            text = GetComponent<Text>();
        }

        private void Update() {
            if (!isGaming) return;
            text.text = $"{player.Distance/10f:F2} M";
        }
        
        public void StartGame(PlayerStatusManager player) {
            isGaming = true;
            this.player = player;
        }
        

        public void EndGame() {
            Destroy(gameObject);
        }
    }
}
