using Game.Player;
using UnityEngine;

namespace Game.Gimmick {
    /// <summary>
    /// 取得するとプレイヤーに影響を与えて消える
    /// アイテムの親クラス
    /// </summary>
    public abstract class ItemBase : GimmickBase {
        /// <summary>
        /// アイテム効果の発動
        /// コライダーに当たると呼ばれる
        /// </summary>
        protected abstract void ActivateItemEffect(PlayerStatusManager status);

        private void OnTriggerEnter2D(Collider2D other) {
            var temp = other.GetComponent<PlayerStatusManager>();
            if (temp == null) return;
            ActivateItemEffect(temp);
            Destroy(gameObject);
        }
    }
}
