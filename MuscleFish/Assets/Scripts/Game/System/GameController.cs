using System.Collections.Generic;
using Game.Gimmick;
using Game.Player;
using UnityEngine;

namespace Game.System {
    /// <summary>
    /// ゲームシーン全体をコントロールするクラス
    /// 始まりから終わりまで
    /// </summary>
    public class GameController : MonoBehaviour {
        [SerializeField] private bool isDebug;
        [SerializeField] private float distance;
        [SerializeField] private float minInterval;
        [SerializeField] private float maxInterval;
        [SerializeField] private GimmickFactory factory;
        public float Distance => distance;
        public bool IsDebug => isDebug;

        private List<GimmickBase> gimmicks;

        public static GameController Instance;
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            CreateGimmick();
            if (isDebug) StartGame();
        }

        /// <summary>
        /// ギミックの生成
        /// シーンの開始時に呼ばれる
        /// </summary>
        private void CreateGimmick() {
            var temp = distance - 20;
            var positions = new List<Vector3>();
            while (true) {
                var length = Random.Range(minInterval, maxInterval);
                temp -= length;
                if (temp < 0) break;
                var xPos = distance - temp;
                var yPos = Random.Range(-54f, 54f);
                positions.Add(new Vector3(xPos, yPos));
            }

            gimmicks = factory.CreateRandomGimmicks(positions);
        }

        /// <summary>
        /// ゲーム開始
        /// 3/2/1/スタートの時呼ばれる
        /// </summary>
        public void StartGame() {
            var player = FindObjectOfType<PlayerStatusManager>();

            foreach (var gimmick in gimmicks) {
                gimmick.Player = player;
            }

            foreach (var obj in FindObjectsOfType<Component>()) {
                (obj as IGameStart)?.StartGame();
            }
        }

        /// <summary>
        /// ゲーム終了
        /// ゴールした時呼ばれる
        /// </summary>
        public void EndGame() {
            foreach (var obj in FindObjectsOfType<Component>()) {
                (obj as IGameEnd)?.EndGame();
            }
        }
    }
}
