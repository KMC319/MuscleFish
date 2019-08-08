using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class FadeIn : MonoBehaviour {
        private Image image;
        private void Start() {
            image = GetComponent<Image>();
            StartCoroutine(Fade());
        }

        IEnumerator Fade() {
            var time = 0.2f;
            while (time>0) {
                image.color = new Color(0, 0, 0, time / 0.2f);
                time -= Time.deltaTime;
                yield return null;
            }
            image.color = Color.clear;
            Destroy(gameObject);
        }
    }
}
