using System;
using System.Collections;
using Game.Player;
using Game.System;
using UnityEngine;

namespace Game.Gimmick {
    public class Net : MonoBehaviour, IGameStart, IGameEnd {
        private bool isGaming;
        private PlayerStatusManager player;
        private float distance;
        public float Distance => distance;
        private float PlayerDistance => player.Distance - distance;
        private float speed;

        private void FixedUpdate() {
            if (!isGaming) return;
            distance += speed * Time.fixedDeltaTime;
            var x = Mathf.Clamp(PlayerDistance, 0, 50);
            transform.position = new Vector3(player.transform.position.x - x, 0);
            if (PlayerDistance <= 0) player.Catch();
            speed += Time.fixedDeltaTime * player.Level * 2;
            speed = Mathf.Clamp(speed, 0, PlayerDistance >= 40 ? speed : player.MaxSpeed);
        }

        public void StartGame(PlayerStatusManager player) {
            isGaming = true;
            this.player = player;
            speed = 48f;
            distance = player.Distance - 60;
        }

        public void EndGame() {
            isGaming = false;
            if (GameController.Instance.EndingType == EEnding.Net) {
                StartCoroutine(NetEnd());
            }
        }

        IEnumerator NetEnd() {
            player.gameObject.transform.parent = transform;
            while (transform.position.y < 120) {
                transform.position += new Vector3(0, 50 * Time.deltaTime);
                yield return null;
            }
        }
    }
}
