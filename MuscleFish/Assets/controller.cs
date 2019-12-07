using System.Collections;
using System.Collections.Generic;
using Game.System;
using UnityEngine;

public class controller : MonoBehaviour {
    public float speed = GameData.Player.Speed;

    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        var y = Input.GetAxisRaw("Vertical");
        if (y != 0) {
            y = y > 0 ? 1 : -1;
            var s = y * speed;
            rigid.velocity = Vector2.up * s;
        } else {
            rigid.velocity = Vector2.zero;
        }
    }
}
