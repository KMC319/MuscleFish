using System.Collections;
using Game.System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Game.Player {
    /// <summary>
    /// プレイヤーのステータスを管理する
    /// これを基に背景の速さとか色々変わる
    /// </summary>
    public class PlayerStatusManager : MonoBehaviour, IGameStart {
        [SerializeField] private EFishName name;
        [SerializeField] private float speed;
        [SerializeField] private Text goalText;
        private VideoPlayer videoPlayer;
        private BackGroundMusicManager bgmer;
        private AudioSource se;
        public EFishName Name => name;
        private float distance;

        private int direction = 1;

        public int Direction {
            get => direction;
            set {
                direction = value;
                ApplyLevelChange();
            }
        }

        private bool isGaming;
        public bool IsFloating { get; set; }
        public float Speed => speed * Mathf.Pow(1.4f, level) * Direction;
        public float MaxSpeed => speed * Mathf.Pow(1.4f, 5);
        public float Distance => distance;

        private int level = 0;

        public int Level {
            get => Mathf.Clamp(level, 0, 5);
            set {
                var old = Level;
                level = Mathf.Clamp(value, -2, 5);
                if (old != Level) ApplyLevelChange();
            }
        }

        private void Awake() {
            bgmer = FindObjectOfType<BackGroundMusicManager>();
            se = GetComponent<AudioSource>();
        }

        public void StartGame(PlayerStatusManager player) {
            if (player != this) return;
            se.PlayOneShot(se.clip);
            videoPlayer = FindObjectOfType<VideoPlayer>();
            isGaming = true;
            Level = 1;
        }

        private void Update() {
            if (!isGaming) return;
            if (IsFloating) {
                var max = Mathf.Clamp(40 - Mathf.Abs(transform.position.y), 0, 25 * Time.deltaTime);
                transform.position += new Vector3(0, Mathf.Clamp(25 * Time.deltaTime, 0, max));
            }
        }

        private void FixedUpdate() {
            if (!isGaming) return;
            distance += Speed * Time.fixedDeltaTime;
            if (GameController.Instance.Distance - distance <= 0) {
                isGaming = false;
                Destroy(GetComponent<controller>());
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Destroy(GetComponent<BoxCollider2D>());
                GameController.Instance.EndGame(EEnding.Goal);
                StartCoroutine(GoalMove());
            }
        }

        public void Bomb() {
            bgmer.StopWithFade();
            videoPlayer.Play();
            isGaming = false;
            Destroy(GetComponent<controller>());
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameController.Instance.EndGame(EEnding.Bomb);
        }

        public void Catch() {
            se.PlayOneShot(se.clip);
            bgmer.StopWithFade();
            isGaming = false;
            Destroy(GetComponent<controller>());
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(GetComponent<BoxCollider2D>());
            GameController.Instance.EndGame(EEnding.Net);
        }

        private void ApplyLevelChange() {
            if (!isGaming) return;
            bgmer.PlayWithFade(Mathf.Clamp(Direction * (Level - 1), 0, 4));
        }

        public void SoundUo() {
            se.PlayOneShot(se.clip);
        }

        private IEnumerator GoalMove() {
            goalText.gameObject.SetActive(true);
            while (transform.position.x < 200) {
                transform.position += new Vector3(Speed * Time.deltaTime, 0);
                yield return null;
            }
        }
    }
}
