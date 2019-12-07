using Game.Player;
using Game.System;
using UnityEngine;

namespace Game.UI {
    public class StartDestroy: MonoBehaviour, IGameStart {
        public void StartGame(PlayerStatusManager player) {
            Destroy(gameObject);
        }
    }
}
