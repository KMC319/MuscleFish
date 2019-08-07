using Game.Player;

namespace Game.Gimmick {
    public class Ice  : ItemBase{
        protected override void ActivateItemEffect(PlayerStatusManager status) {
            status.Level--;
        }
    }
}
