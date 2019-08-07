using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    GameObject objGameOver;

    // Start is called before the first frame update
    void Start()
    {

        this.objGameOver = GameObject.Find("TextGameOver");
        objGameOver.GetComponent<Text>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {



    }
    public void DisplayGameOver()
    {
        objGameOver.GetComponent<Text>().enabled = true;
    }
}
