using Game.Player;
using Game.System;
using UnityEngine;

namespace Game.UI {
    public class ShipBar : MonoBehaviour, IGameStart {
        [SerializeField] private SpriteRenderer movableImg;
        [SerializeField] private float size = 32;
        private cpu cpu;
        private bool isGaming;

        // Update is called once per frame
        void Update() {
            if (!isGaming) return;
            movableImg.transform.localPosition =
                new Vector3((cpu.speed / GameController.Instance.Distance * 2 - 1) * -size, movableImg.transform.localPosition.y);
        }

        public void StartGame() {
            cpu = FindObjectOfType<cpu>();
            isGaming = false;
        }
    }
}