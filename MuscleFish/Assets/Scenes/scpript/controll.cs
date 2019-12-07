using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class controll : MonoBehaviour
{
       
 public Vector2 SPEED = new Vector2(1f, 1f);
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
        var y = Input.GetAxisRaw("Vertical");
        if (y == 0) return;
        y = y > 0 ? 1 : -1;
        Position.y += y * SPEED.y;
        transform.position = Position;
    }
}
