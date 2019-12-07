using Game.Player;
using Game.System;
using UnityEngine;

namespace Game.Gimmick {
    /// <summary>
    /// ステージギミック全ての親
    /// ランダム生成の対象にできる
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class GimmickBase : MonoBehaviour, IGameEnd {
        protected Rigidbody2D Rigid;
        protected bool IsActive;
        private float time;
        protected float Alpha;

        private void Awake() {
            Rigid = GetComponent<Rigidbody2D>();
            Alpha = Random.Range(-Mathf.PI, Mathf.PI);
        }

        protected virtual void OnEnable() {
            IsActive = true;
        }

        private void OnDisable() {
            IsActive = false;
        }

        private void Update() {
            if (!IsActive || !GameController.Instance.IsGaming) return;
            time += Time.deltaTime;
            Rigid.velocity = new Vector3(-GameData.Player.Speed, 5f * Mathf.Sin(Alpha * time));
            if (transform.position.x < -200) {
                IsActive = false;
                GimmickFactory.pool.ReturnObject(this);
            }
        }
        
        public void EndGame() {
            Rigid.velocity = Vector2.zero;
            IsActive = false;
        }
    }
}
