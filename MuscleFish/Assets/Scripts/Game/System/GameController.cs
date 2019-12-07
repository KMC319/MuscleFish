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
        private HighScoreManager highScoreManager;
        public static GameMode GameMode { get; set; }
        public float Distance => distance;

        public static GameController Instance;
        public EEnding EndingType { get; private set; }
        public EFishName Name { get; private set; }
        public bool IsGaming { get; private set; }
        private float time;
        public float GameTime => time;
        private float length;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            highScoreManager = new HighScoreManager();
            length = 96;
            var xPos = 130;
            var yPos = Random.Range(-40f, 40f);
            factory.CreateGimmick(Vector3.right * xPos + Vector3.up * yPos, 1);
        }

        private void FixedUpdate() {
            if (!IsGaming) return;
            time += Time.fixedDeltaTime;
            if (GameMode == GameMode.Endless) {
                distance = GameData.Player.Distance + 192;
            }

            length -= GameData.Player.Speed * Time.fixedDeltaTime;
            if (length < 0) {
                length = Random.Range(minInterval, maxInterval);
                CreateGimmick();
            }
        }

        /// <summary>
        /// ギミックの生成
        /// </summary>
        private void CreateGimmick() {
            var xPos = 130;
            var yPos = Random.Range(-54f, 54f);
            factory.CreateRandomGimmick(Vector3.right * xPos + Vector3.up * yPos);
        }

        /// <summary>
        /// ゲーム開始する時呼ばれる
        /// </summary>
        public void StartGame(PlayerStatusManager player) {
            Name = player.Name;
            time = 0f;
            IsGaming = true;
            GameData.Player = player;
            foreach (var obj in FindObjectsOfType<Component>()) {
                (obj as IGameStart)?.StartGame(player);
            }

            gameOver.StartGame(player);
        }

        /// <summary>
        /// ゲーム終了した時呼ばれる
        /// </summary>
        public void EndGame(EEnding ending) {
            IsGaming = false;
            EndingType = ending;
            foreach (var obj in FindObjectsOfType<Component>()) {
                (obj as IGameEnd)?.EndGame();
            }

            StartCoroutine(EndAnimation());
        }

        IEnumerator EndAnimation() {
            GameData.Score = GameMode == GameMode.TimeAttack ? GameTime : (GameData.Player.Distance / 10f);
            switch (EndingType) {
                case EEnding.Goal:
                    highScoreManager.UpdateHighScore(GameMode);
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
                    if(GameMode == GameMode.Endless)highScoreManager.UpdateHighScore(GameMode);
                    gameOver.SetObject();
                    yield return new WaitForSeconds(6.5f);
                    videoPlayer.gameObject.SetActive(false);
                    gameOver.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                    break;
                case EEnding.Net:
                    if(GameMode == GameMode.Endless)highScoreManager.UpdateHighScore(GameMode);
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
