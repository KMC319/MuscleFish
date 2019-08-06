using Game.Player;
using Game.System;
using UnityEngine;

namespace Game.Gimmick {
    /// <summary>
    /// ステージギミック全ての親
    /// ランダム生成の対象にできる
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class GimmickBase : MonoBehaviour, IGameStart, IGameEnd {
        private Rigidbody2D rigid;
        private bool isGaming;
        private float time;
        private float alpha;

        public PlayerStatusManager Player { get; set; }

        private void Awake() {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if (!isGaming) return;
            time += Time.deltaTime;
            rigid.velocity = new Vector3(-Player.Speed, 5f * Mathf.Sin(alpha * time));
        }

        public void StartGame() {
            isGaming = true;
            alpha = Random.Range(-Mathf.PI, Mathf.PI);
        }

        public void EndGame() {
            rigid.velocity = Vector2.zero;
            isGaming = false;
        }
    }
}
