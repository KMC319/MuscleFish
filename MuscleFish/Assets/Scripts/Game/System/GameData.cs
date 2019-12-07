using Game.Player;
using UnityEngine;

namespace Game.System {
    public static class GameData {
        public static PlayerStatusManager Player { get; set; }
        public static string TimeAttackDataKey { get; } = "TimeAttackData";
        public static string EndlessDataKey { get; private set; } = "EndlessData";
        public static float Score { get; set; }
    }
}
