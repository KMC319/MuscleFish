using Game.Player;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class DisplayLevel : MonoBehaviour, IGameStart, IGameEnd {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private SpriteRenderer max;
        private new SpriteRenderer renderer;
        private PlayerStatusManager player;

        private void Awake() {
            renderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update() {
            if (player == null) return;
            renderer.sprite = sprites[player.Level];
            max.enabled = player.Level > 4;
            if (player.Direction == 0) renderer.sprite = null;
        }

        public void StartGame(PlayerStatusManager player) {
            this.player = player;
        }

        public void EndGame() {
            Destroy(gameObject);
        }
    }
}
