using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSk : MonoBehaviour
{
    private LayerMask layerCheckPoint;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setColor()
    {
        spriteRenderer.color = Color.yellow;
    }
}
