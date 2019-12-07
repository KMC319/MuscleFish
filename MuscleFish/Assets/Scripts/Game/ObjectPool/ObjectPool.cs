using System.Collections.Generic;
using UnityEngine;

namespace Game.ObjectPool {
    /// <summary>
    /// オブジェクトプール
    /// 消すとき何かさせたいならOnDisableで
    /// </summary>
    public class ObjectPool<T> where T : Component {
        private readonly Dictionary<int, Queue<T>> pooledObjects = new Dictionary<int, Queue<T>>();
        private readonly Dictionary<int, int> poolDatabase = new Dictionary<int, int>();
        private readonly Transform parent;
        public bool IsPooled(T instance) => pooledObjects.ContainsKey(instance.GetInstanceID());
        public bool IsRemainObject(T instance) => pooledObjects.TryGetValue(instance.GetInstanceID(), out var pool) && pool.Count > 0;

        private T Rent(T instance) {
            var key = instance.GetInstanceID();
            if (!pooledObjects.TryGetValue(key, out var pool)) {
                Debug.LogWarning("Pooled new object!");
                pool = new Queue<T>();
                pooledObjects.Add(key, pool);
            }

            if (pool.Count > 0) {
                var obj = pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            } else {
                Debug.LogWarning("Created new object!");
                var obj = Object.Instantiate(instance, new Vector3(), new Quaternion());
                obj.transform.parent = parent;
                return obj;
            }
        }

        public T GetObject(T instance) {
            var instanceTransform = instance.transform;
            var obj = Rent(instance);
            var objTransform = obj.transform;
            objTransform.position = instanceTransform.position;
            objTransform.rotation = instanceTransform.rotation;
            return obj;
        }

        public T GetObject(T instance, Vector3 position) {
            var instanceTransform = instance.transform;
            var obj = Rent(instance);
            var objTransform = obj.transform;
            objTransform.position = position;
            objTransform.rotation = instanceTransform.rotation;
            return obj;
        }

        public T GetObject(T instance, Vector3 position, Quaternion rotation) {
            var obj = Rent(instance);
            var objTransform = obj.transform;
            objTransform.position = position;
            objTransform.rotation = rotation;
            return obj;
        }

        public void Preload(T instance, int num) {
            var key = instance.GetInstanceID();
            if (!pooledObjects.TryGetValue(key, out var pool)) {
                pool = new Queue<T>();
                pooledObjects.Add(key, pool);
            }

            for (var i = 0; i < num; i++) {
                var obj = Object.Instantiate(instance, new Vector3(), new Quaternion());
                obj.transform.SetParent(parent);
                pool.Enqueue(obj);
                poolDatabase.Add(obj.GetInstanceID(), key);
                obj.gameObject.SetActive(false);
            }
        }

        public void ReturnObject(T instance) {
            if (!poolDatabase.TryGetValue(instance.GetInstanceID(), out var key)) {
                Debug.LogWarning(instance + "is not pooled object!");
                Object.Destroy(instance);
                return;
            }

            if (!pooledObjects.TryGetValue(key, out var pool)) {
                Debug.LogWarning("Pool is broken! New pool is created automatically.");
                pool = new Queue<T>();
                pooledObjects.Add(key, pool);
            }

            pool.Enqueue(instance);
            instance.gameObject.SetActive(false);
        }

        public ObjectPool(Transform parent) {
            this.parent = parent;
        }

        public ObjectPool(T poolObj, Transform parent) {
            this.parent = parent;
            RegisterInstance(poolObj);
        }

        public ObjectPool(IEnumerable<T> poolList, Transform parent) {
            this.parent = parent;
            foreach (var poolData in poolList) {
                RegisterInstance(poolData);
            }
        }

        private void RegisterInstance(T instance) {
            var key = instance.GetInstanceID();
            if (pooledObjects.ContainsKey(key)) {
                Debug.LogError(instance + "is already pooled!");
                return;
            }

            pooledObjects.Add(key, new Queue<T>());
        }
    }
}
