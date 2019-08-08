using System.Linq;
using Game.Gimmick;
using Game.Player;
using UnityEngine;

namespace Game.System {
    public class PlayerPicker : MonoBehaviour {
        [SerializeField] private PlayerStatusManager[] players;
        private float interval = 0.2f;
        private int index;

        private int Index {
            get => index;
            set {
                var old = Index;
                index = Mathf.Clamp(value, 0, players.Length - 1);
                if (old != Index) SetPosition();
            }
        }

        private void Start() {
            Index = 0;
            SetPosition();
        }

        private void Update() {
            MoveCursor();
            if (Input.GetKeyDown(KeyCode.Space)) PickPlayer();
        }

        private void MoveCursor() {
            var y = 0;
            if (Input.GetKey(KeyCode.DownArrow)) y = 1;
            if (Input.GetKey(KeyCode.UpArrow)) y = -1;

            if (y != 0) {
                if (interval <= 0) {
                    Index = (Index + players.Length + y) % players.Length;
                    interval = 0.2f;
                }

                interval -= Time.deltaTime;
            } else {
                interval = 0;
            }
        }

        private void PickPlayer() {
            players[Index].gameObject.AddComponent<controller>().speed = players[Index].Speed*3;
            foreach (var player in players.Where(x => x != players[Index])) {
                player.gameObject.AddComponent<Fish>();
                Destroy(player);
            }
            GameController.Instance.StartGame(players[Index]);
            Destroy(gameObject);
        }

        private void SetPosition() {
            transform.position = new Vector3(transform.position.x, players[Index].transform.position.y);
        }
    }
}
