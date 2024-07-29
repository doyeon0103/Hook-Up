using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterOnTrigger : MonoBehaviour
{
    string cP1 = "Chapter1";
    string cP2 = "Chapter2";
    string cP3 = "Chapter3";
    string cP4 = "Chapter4";
    string end = "Congratulations!";
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chapter1")
        {
            text.text = cP1;
            text.color = Color.black;
        }
        if (collision.tag == "Chapter2")
        {
            text.text = cP2;
            text.color = Color.white;
        }
        if (collision.tag == "Chapter3")
        {
            text.text = cP3;
            text.color = Color.white;
        }
        if (collision.tag == "Chapter4")
        {
            text.text = cP4;
            text.color = Color.white;
        }
        if (collision.tag == "Ending")
        {
            text.text = end;
            text.color = Color.white;
        }
    }
}
