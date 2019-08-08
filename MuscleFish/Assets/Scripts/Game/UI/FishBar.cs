using Game.Player;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class FishBar : MonoBehaviour, IGameStart ,IGameEnd{
        [SerializeField] private SpriteRenderer movableImg;
        [SerializeField] private float size = 32;
        private PlayerStatusManager player;
        private bool isGaming;

        // Update is called once per frame
        void Update() {
            if (!isGaming) return;
            movableImg.transform.localPosition =
                new Vector3((player.Distance / GameController.Instance.Distance * 2 - 1) * -size, movableImg.transform.localPosition.y);
        }

        public void StartGame(PlayerStatusManager player) {
            this.player = player;
            isGaming = true;
        }
        

        public void EndGame() {
            Destroy(gameObject);
        }
    }
}
