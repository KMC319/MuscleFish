using UnityEngine;

public class EscapeQuit : MonoBehaviour {
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
