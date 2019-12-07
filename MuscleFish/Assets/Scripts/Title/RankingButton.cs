using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Title {
    public class RankingButton : MonoBehaviour {
        [SerializeField] private CanvasGroup image;
        private bool isOn;
        
        private void Start() {
            image.alpha = 0;
        }
        
        public void OnButton() {
            if(isOn) return;
            image.alpha = 1;
            StartCoroutine(WaitClose());
            isOn = true;
        }

        private IEnumerator WaitClose() {
            yield return null;
            while (!Input.anyKeyDown) {
                yield return null;
            }
            image.alpha = 0;
            isOn = false;
        }
    }
}
