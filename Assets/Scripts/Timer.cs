using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("�ǽð� �÷���Ÿ��")]
    [SerializeField] float playTimer = 0;
    [SerializeField] TMP_Text tmpTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playTime();
    }
    private void playTime()
    {
        playTimer += Time.deltaTime;
        tmpTimer.text = playTimer.ToString();
    }
}
