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
        protected Rigidbody2D Rigid;
        protected bool IsGaming;
        private float time;
        protected float Alpha;

        private PlayerStatusManager player;

        private void Awake() {
            Rigid = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if (!IsGaming) return;
            time += Time.deltaTime;
            Rigid.velocity = new Vector3(-player.Speed, 5f * Mathf.Sin(Alpha * time));
            if(transform.position.x < -200) Destroy(gameObject);
        }

        public virtual void StartGame(PlayerStatusManager player) {
            IsGaming = true;
            this.player = player;
            Alpha = Random.Range(-Mathf.PI, Mathf.PI);
        }

        public void EndGame() {
            Rigid.velocity = Vector2.zero;
            IsGaming = false;
        }
    }
}
