using Game.System;
using UnityEngine;
using UnityEngine.Video;

namespace Game.Player {
    /// <summary>
    /// プレイヤーのステータスを管理する
    /// これを基に背景の速さが変わる
    /// </summary>
    public class PlayerStatusManager : MonoBehaviour, IGameStart {
        [SerializeField] private float speed;
        [SerializeField] private int maxLevel;
        private VideoPlayer videoPlayer;
        private float distance;
        private bool isGaming;
        public float Speed => speed * Level;
        public float Distance => distance;

        private int level;

        public int Level { get => level; set { level = Mathf.Clamp(value, 1, maxLevel); } }

        public void StartGame() {
            videoPlayer = FindObjectOfType<VideoPlayer>();
            distance = GameController.Instance.Distance;
            isGaming = true;
            Level = 1;
        }

        private void Update() {
            if (GameController.Instance.IsDebug) {
                if (Input.GetKeyDown(KeyCode.Q)) Level++;
                if (Input.GetKeyDown(KeyCode.E)) Level--;
            }
        }

        private void FixedUpdate() {
            if (!isGaming) return;
            distance -= Speed * Time.fixedDeltaTime;
        }

        public void Bomb() {
            videoPlayer.Play();
            GameController.Instance.EndGame();
        }
    }
}
