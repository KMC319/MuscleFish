using Game.Player;

namespace Game.Gimmick {
    public class Protein : ItemBase {
        protected override void ActivateItemEffect(PlayerStatusManager status) {
            status.Level++;
            status.SoundUo();
        }
    }
}
