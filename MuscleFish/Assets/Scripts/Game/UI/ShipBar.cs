using Game.Gimmick;
using Game.Player;
using Game.System;
using UnityEngine;

namespace Game.UI {
    public class ShipBar : MonoBehaviour, IGameStart {
        [SerializeField] private SpriteRenderer movableImg;
        [SerializeField] private int size = 32;
        [SerializeField] private Net net;
        private bool isGaming;

        void Update() {
            if (!isGaming) return;
            movableImg.transform.localPosition =
                new Vector3((net.Distance / GameController.Instance.Distance * 2 - 1) * -size, movableImg.transform.localPosition.y);
        }

        public void StartGame(PlayerStatusManager player) {
            isGaming = true;
        }
    }
}
