using System.Collections;
using System.Collections.Generic;
using Game.Gimmick;
using Game.Player;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

namespace Game.System {
    /// <summary>
    /// ゲームシーン全体をコントロールするクラス
    /// 始まりから終わりまで
    /// </summary>
    public class GameController : MonoBehaviour {
        [SerializeField] private float distance;
        [SerializeField] private float minInterval;
        [SerializeField] private float maxInterval;
        [SerializeField] private GimmickFactory factory;
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private GameOverCanvas gameOver;
        [SerializeField] private RandomText clearText;
        [SerializeField] private Text timeText;
        [SerializeField] private RandomText rankText;
        public float Distance => distance;

        public static GameController Instance;
        public EEnding EndingType { get; private set; }
        public EFishName Name { get; private set; }
        private bool isGaming;
        private float time;
        public float GameTime => time;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            CreateGimmick();
        }

        private void FixedUpdate() {
            if (!isGaming) return;
            time += Time.fixedDeltaTime;
        }

        /// <summary>
        /// ギミックの生成
        /// シーンの開始時に呼ばれる
        /// </summary>
        private void CreateGimmick() {
            var temp = distance - 96;
            var positions = new List<Vector3>();
            while (true) {
                var length = Random.Range(minInterval, maxInterval);
                temp -= length;
                if (temp < 0) break;
                var xPos = distance - temp;
                var yPos = Random.Range(-40f, 40f);
                positions.Add(new Vector3(xPos, yPos));
            }

            factory.CreateRandomGimmicks(positions);
        }

        /// <summary>
        /// ゲーム開始する時呼ばれる
        /// </summary>
        public void StartGame(PlayerStatusManager player) {
            Name = player.Name;
            time = 0f;
            isGaming = true;
            foreach (var obj in FindObjectsOfType<Component>()) {
                (obj as IGameStart)?.StartGame(player);
            }
            gameOver.StartGame(player);
        }

        /// <summary>
        /// ゲーム終了した時呼ばれる
        /// </summary>
        public void EndGame(EEnding ending) {
            isGaming = false;
            EndingType = ending;
            foreach (var obj in FindObjectsOfType<Component>()) {
                (obj as IGameEnd)?.EndGame();
            }

            StartCoroutine(EndAnimation());
        }

        IEnumerator EndAnimation() {
            switch (EndingType) {
                case EEnding.Goal:
                    yield return new WaitForSeconds(1.5f);
                    clearText.Display();
                    yield return new WaitForSeconds(1f);
                    timeText.text = $"タイム：{GameTime:00.00}";
                    timeText.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    rankText.Display();
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                    break;
                case EEnding.Bomb:
                    gameOver.SetObject();
                    yield return new WaitForSeconds(6.5f);
                    videoPlayer.gameObject.SetActive(false);
                    gameOver.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                    break;
                case EEnding.Net:
                    gameOver.SetObject();
                    yield return new WaitForSeconds(3);
                    videoPlayer.gameObject.SetActive(false);
                    gameOver.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                    break;
            }
        }
    }
}
