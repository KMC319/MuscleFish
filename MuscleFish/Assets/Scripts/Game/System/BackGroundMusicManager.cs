using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game.System {
    public class BackGroundMusicManager : MonoBehaviour {
        [SerializeField] private float fadeTime;
        [SerializeField] private AudioClip[] clips;
        private AudioSource[] sources;
        private AudioSource sourcePlaying;
        private bool isFading;

        private void Start() {
            sources = GetComponents<AudioSource>();
        }

        public void PlayWithFade(int index) {
            if (isFading) {
                Debug.Log("already Fading!");
                return;
            }

            isFading = true;
            var emptySource = sources.First(x => x != sourcePlaying);
            emptySource.clip = clips[index];
            var rate = 0f;
            if (sourcePlaying != null && sourcePlaying.clip != null) {
                rate = sourcePlaying.time / sourcePlaying.clip.length;
                StartCoroutine(FadeOut(sourcePlaying));
            }

            var volume = 0.3f;
            sourcePlaying = emptySource;
            StartCoroutine(FadeIn(emptySource, rate, volume));
        }

        public void StopWithFade() {
            if (sourcePlaying == null) return;
            StartCoroutine(FadeOut(sourcePlaying));
        }

        IEnumerator FadeIn(AudioSource source, float rate, float volume) {
            source.time = rate * source.clip.length;
            source.Play();
            source.DOFade(volume, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            isFading = false;
        }

        IEnumerator FadeOut(AudioSource source) {
            source.DOFade(0f, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            source.Pause();
            source.clip = null;
        }
    }
}
