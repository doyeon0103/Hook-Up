using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter4FakeTile : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 moveDir;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveDistance = 5f;
    bool movingRight = true;
    bool isMove = false;
    private Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        startPosition = rigid.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = rigid.position;

        if (movingRight == true && isMove == true)
        {
            newPosition.x += moveSpeed * Time.deltaTime;
            if (newPosition.x >= startPosition.x + moveDistance)
            {
                newPosition.x = startPosition.x + moveDistance;
                movingRight = false;
            }
        }
        else if (movingRight == false && isMove == true)
        {
            newPosition.x -= moveSpeed * Time.deltaTime;
            if (newPosition.x <= startPosition.x - moveDistance)
            {
                newPosition.x = startPosition.x - moveDistance;
                movingRight = true;
            }
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
    private void OnTriggerExit2D(Collider2D Player)
    {
        if (Player.tag == "Player" && Player is BoxCollider2D)
        {
            isMove = false;
        }
    }
}
