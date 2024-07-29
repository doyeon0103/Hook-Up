using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTile : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.tag == "Player")
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
