 
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    //　スタートボタンを押したら実行する
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}