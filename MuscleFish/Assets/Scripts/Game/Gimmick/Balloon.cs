using Game.Player;

namespace Game.Gimmick {
    public class Balloon : ItemBase {
        protected override void ActivateItemEffect(PlayerStatusManager status) {
            status.IsFloating = true;
        }
    }
}
