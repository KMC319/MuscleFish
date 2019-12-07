using UnityEngine;
using UnityEngine.EventSystems;

namespace Title {
    public class Title : MonoBehaviour {
        [SerializeField] private UnityEngine.UI.Button start;
        private GameObject beforeSelectedGameObject;

        private void Start() {
            beforeSelectedGameObject = start.gameObject;
            ReSetEventSystem();
        }

        private void ReSetEventSystem() {
            EventSystem.current.SetSelectedGameObject(beforeSelectedGameObject);
        }

        private void Update() {
            if (EventSystem.current.currentSelectedGameObject == beforeSelectedGameObject) return;
            if (EventSystem.current.currentSelectedGameObject == null) {
                ReSetEventSystem();
            } else {
                beforeSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}
