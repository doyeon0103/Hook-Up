using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class CameraMoving : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] BoxCollider2D coll;
    [SerializeField] Transform trsPlayer;
    Bounds curBound;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        checkBound();
        screen();
        Screen.SetResolution(1920, 1080, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (trsPlayer == null) return;
        mainCam.transform.position = new Vector3(
            Mathf.Clamp(trsPlayer.position.x, curBound.min.x, curBound.max.x),Mathf.Clamp(trsPlayer.position.y, curBound.min.y, curBound.max.y),
            mainCam.transform.position.z
            );
    }
    private void checkBound()
    {
        float height = mainCam.orthographicSize + 5;
        float width = height * mainCam.aspect;

        curBound = coll.bounds;

        float minX = curBound.min.x + width;
        float minY = curBound.min.y + height;

        float maxX = curBound.max.x - width;
        float maxY = curBound.max.y - height;

        curBound.SetMinMax(new Vector3(minX,minY), new Vector3(maxX,maxY));
    }
    private void screen()
    {
        float targeRatio = 9.0f / 16.0f;
        float ratio = (float)Screen.width / (float)Screen.height;
        float scaleHeight = ratio / targeRatio;
        float fixedWidth = (float)Screen.width / scaleHeight;
        Screen.SetResolution((int)fixedWidth, Screen.height, true);
    }
}
