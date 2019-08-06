using Game.Player;

namespace Game.Gimmick {
    public class Bomb : ItemBase{
        protected override void ActivateItemEffect(PlayerStatusManager status) {
            status.Bomb();
        }
    }
}
