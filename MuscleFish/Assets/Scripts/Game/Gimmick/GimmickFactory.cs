using System.Collections.Generic;
using UnityEngine;

namespace Game.Gimmick {
    public class GimmickFactory : MonoBehaviour {
        [SerializeField] private List<GimmickBase> gimmicks;

        
        public List<GimmickBase> CreateRandomGimmicks(List<Vector3> positions) {
            var list = new List<GimmickBase>();
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
