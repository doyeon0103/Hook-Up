using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveDir;
    Rigidbody2D rigid;
    Camera mainCam;
    [SerializeField] Player player;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        checkAim();
    }
    private void checkAim()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = player.transform.position;
        Vector2 fixedPos = playerPos - mouseWorldPos;
        if (fixedPos.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1.0f;
            transform.localScale = scale;
        }
        else if (fixedPos.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1.0f;
            transform.localScale = scale;
        }
    }
}

