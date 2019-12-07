using System.Collections.Generic;
using Game.ObjectPool;
using UnityEngine;

namespace Game.Gimmick {
    public class GimmickFactory : MonoBehaviour {
        [SerializeField] private List<GimmickBase> gimmicks;
        public static ObjectPool<GimmickBase> pool;

        private void Awake() {
            pool = new ObjectPool<GimmickBase>(gimmicks, transform);
            foreach (var gimmick in gimmicks) {
                pool.Preload(gimmick, 30);
            }
        }

        public void CreateRandomGimmick(Vector3 position) {
            var obj = pool.GetObject(gimmicks[Random.Range(0, gimmicks.Count)], position);
        }

        public void CreateGimmick(Vector3 position, int num) {
            var obj = pool.GetObject(gimmicks[num], position);
        }
        
        public List<GimmickBase> CreateRandomGimmicks(List<Vector3> positions) {
            var list = new List<GimmickBase>();
            
            var first = Instantiate(gimmicks[1]);
            first.transform.position = positions[0];
            list.Add(first);
            positions.RemoveAt(0);
            foreach (var position in positions) {
                var gimmick = gimmicks[Random.Range(0, gimmicks.Count)];
                var obj = Instantiate(gimmick);
                obj.transform.position = position;
                list.Add(obj);
            }
            return list;
        }
    }
}
