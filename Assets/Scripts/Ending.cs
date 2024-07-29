using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 moveDir;
    [SerializeField] float moveSpeed;
    bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = rigid.position;

        if (isMove == true)
        {
            newPosition.y += moveSpeed * Time.deltaTime;
        }
        rigid.MovePosition(newPosition);
    }
    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.tag == "Player" && Player is BoxCollider2D)
        {
            isMove = true;
        }
    }
}
