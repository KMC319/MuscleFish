using System;
using System.Linq;
using Game.Player;
using Game.System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.UI {
    public class GameOverCanvas : MonoBehaviour {
        [SerializeField] private Image cookedImage;
        [SerializeField] private Text messageText;
        [SerializeField] private Text rankText;
        [SerializeField] private ImageSetting[] images;
        [SerializeField] private MessageSetting[] messages;
        [SerializeField] private string[] ranks;
        private PlayerStatusManager player;

        public void SetObject() {
            var level = Mathf.Clamp(player.Level, 1, 5) - 1;
            Debug.Log(level);
            var scoreType = level > 2 ? EScoreType.High : EScoreType.Low;
            var cookedType = GameController.Instance.EndingType == EEnding.Bomb ? ECookedType.Bomb : ECookedType.Raw;
            cookedImage.sprite = images.First(x => x.name == GameController.Instance.Name && x.menu == cookedType).sprite;
            var temp = messages.Where(x => x.menu == cookedType || x.menu == ECookedType.Both).Where(x => x.score == scoreType).ToArray();
            messageText.text = temp[Random.Range(0, temp.Length)].message;
            rankText.text = "おいしさランク：" + ranks[level];
        }


        [Serializable]
        private struct ImageSetting {
            public Sprite sprite;
            public EFishName name;
            public ECookedType menu;
        }

        [Serializable]
        private struct MessageSetting {
            public string message;
            public EScoreType score;
            public ECookedType menu;
        }

        private enum EScoreType {
            High, Low
        }

        private enum ECookedType {
            Raw, Bomb, Both
        }

        public void StartGame(PlayerStatusManager player) {
            this.player = player;
        }
    }
}
