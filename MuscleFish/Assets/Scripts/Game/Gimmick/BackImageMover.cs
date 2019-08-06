using Game.Player;
using Game.System;
using UnityEngine;

namespace Game.Gimmick {
    /// <summary>
    /// 背景を移動させるクラス
    /// </summary>
    public class BackImageMover : MonoBehaviour,IGameStart,IGameEnd{
        [SerializeField] private SpriteRenderer[] sprites;
        private PlayerStatusManager player;
        private bool isGaming;

        private void FixedUpdate() {
            if(!isGaming) return;
            Move();
        }

        private void Move() {
            var speed = player.Speed * Time.fixedDeltaTime;
            foreach (var sprite in sprites) {
                sprite.transform.position -= new Vector3(speed, 0);
                if (sprite.transform.position.x <= -192) SetEndOfSprites(sprite);
            }
        }

        ///     <summary>
        /// スプライトのポジションを最後尾に設置する
        /// </summary>
        private void SetEndOfSprites(SpriteRenderer sprite) {
            sprite.transform.position += new Vector3(sprites.Length * 192, sprite.transform.position.y);
        }

        public void StartGame() {
            player = FindObjectOfType<PlayerStatusManager>();
            isGaming = true;
        }

        public void EndGame() {
            isGaming = false;
        }
    }
}
