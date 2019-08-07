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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "girl")
        {


            this.gameObject.SetActive(false);

            Invoke("GoToGameOver", 1.5f);
        }
        void GoToGameOver()
        {
            SceneManager.LoadScene("GO");
        }
    }
}
