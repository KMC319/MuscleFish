using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpu : MonoBehaviour
{
    // Start is called before the first frame update
   
        GameObject Player;
        GameObject homingObj;
        public float speed;
    void Start()
    {
        Player = GameObject.Find("Player");
        homingObj = GameObject.Find("Homing");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector2.MoveTowards
            (this.transform.position, new Vector2
            (Player.transform.position.x, Player.transform.position.y)
            , speed * Time.deltaTime);
    }
}
