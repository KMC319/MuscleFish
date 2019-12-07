using Game.Player;
using UnityEngine;

namespace Game.Gimmick {
    public class Wall : GimmickBase {
        private bool once;
        private void OnTriggerEnter2D(Collider2D other) {
            if (once) return;
            var temp = other.GetComponent<PlayerStatusManager>();
            if (temp == null) return;
            if (temp.Level >= 5) {
                BreakWall();
                once = true;
            }
            else if (transform.position.x - temp.transform.position.x>= 29) {
                temp.Direction = 0;
            }
            Debug.Log(transform.position.x - temp.transform.position.x);
        }

        private void OnTriggerExit2D(Collider2D other) {
            var temp = other.GetComponent<PlayerStatusManager>();
            if (temp == null) return;
            temp.Direction = 1;
        }

        protected override void OnEnable() {
            base.OnEnable();
            Alpha = 0;
        }

        private void BreakWall() {
            IsActive = false;
            Rigid.velocity = Vector2.zero;
            var rand = Random.Range(-Mathf.PI / 2f, Mathf.PI / 2f);
            Rigid.velocity = new Vector2(Mathf.Cos(rand), Mathf.Sin(rand)) * 300;
        }
    }
}
