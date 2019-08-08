using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.UI {
    public class RandomText : MonoBehaviour {
        [SerializeField, TextArea(2, 2)] private string[] messages;

        public void Display() {
            GetComponent<Text>().text = string.Format(messages[Random.Range(0, messages.Length)]);
            gameObject.SetActive(true);
        }
    }
}
