using System.Linq;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class DisplayScore : MonoBehaviour {
        [SerializeField] private bool isAutoDisplay;
        [SerializeField] private GameMode gameMode;
        private Text[] texts;

        private void Start() {
            texts = GetComponentsInChildren<Text>();
            if (isAutoDisplay) Display(gameMode);
        }

        public void Display(GameMode mode) {
            var key = mode == GameMode.TimeAttack ? GameData.TimeAttackDataKey : GameData.EndlessDataKey;
            var scores = PlayerPrefs.GetString(key, "0,0,0,0,0,")
                .Split(',')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(float.Parse)
                .Select(x => x <= 0 ? "-----" : $"{x:F2}")
                .ToArray();
            var unit = mode == GameMode.TimeAttack ? "びょう" : "めーとる";
            var num = 1;
            foreach (var text in texts) {
                text.text = $"{num}. {scores[num - 1]} {unit}";
                num++;
            }
        }
    }
}
