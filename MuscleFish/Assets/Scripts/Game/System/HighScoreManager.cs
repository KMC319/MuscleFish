using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.System {
    public class HighScoreManager {
        private List<float> scores;
        private bool updated;

        public float[] UpdateHighScore(GameMode mode) {
            var key = mode == GameMode.TimeAttack ? GameData.TimeAttackDataKey : GameData.EndlessDataKey;
            updated = true;
            var texts = PlayerPrefs.GetString(key, "0,0,0,0,0,").Split(',');
            scores = texts.Where(x => !string.IsNullOrEmpty(x))
                .Select(float.Parse)
                .Select(x => x <= 0
                    ? mode == GameMode.TimeAttack ? 999 
                    : 0
                    : x)
                .ToList();
            scores.Add(GameData.Score);
            scores = mode == GameMode.TimeAttack
                ? scores.OrderBy(x => x).ToList()
                : scores.OrderByDescending(x => x).ToList();
            var hiScores = scores.Take(5).ToArray();
            var result = "";
            foreach (var score in hiScores) {
                result += $"{(score == 999 ? 0 : score)},";
            }

            Debug.Log(result);
            PlayerPrefs.SetString(key, result);
            return hiScores;
        }
    }
}
