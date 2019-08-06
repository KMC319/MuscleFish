using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controll : MonoBehaviour
{
    public Vector2 SPEED = new Vector2(0.05f, 0.05f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 Position = transform.position;

        if (Input.GetKey("up"))
        {
            Position.y += SPEED.y;
        }
        else if (Input.GetKey("down"))
        {
            Position.y -= SPEED.y;
        }
        transform.position = Position;
    }

}
