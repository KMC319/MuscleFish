using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



       

        if (Input.GetKey("up"))
            {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            }
            else if (Input.GetKey("down"))
            {
            GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            }
            else {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }



        




       
    }
}
