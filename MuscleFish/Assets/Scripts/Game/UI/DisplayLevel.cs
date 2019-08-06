using Game.Player;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class DisplayLevel : MonoBehaviour, IGameStart {
        private Text text;
        private PlayerStatusManager player;

        private void Awake() {
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update() {
            if (player != null) text.text = "Level " + player.Level;
        }
        public void StartGame() {
            player = FindObjectOfType<PlayerStatusManager>();
        }
    }
}
