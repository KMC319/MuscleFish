using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCanvas : MonoBehaviour
{
    static Canvas _canvas;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }
    public static void SetActive(string name,bool b)
    {
        foreach(Transform child in _canvas.transform)
        {
            if(child.name == name)
            {
                child.gameObject.SetActive(b);
                return;
            }
        }
        Debug.LogWarning("Not found objname:" + name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
