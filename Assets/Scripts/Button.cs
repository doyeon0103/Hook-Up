using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    ShotManager shotManager;
    ShotManagerHard shotManagerHard;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        Screen.SetResolution(1920, 1080, true);
        shotManager = FindObjectOfType<ShotManager>();
        shotManagerHard = FindObjectOfType<ShotManagerHard>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonClickNormal()
    {
        SceneManager.LoadScene("NormalMode");
    }
    public void ButtonClickHard()
    {
        SceneManager.LoadScene("HardMode");
    }
    public void ButtonClickStart()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void ButtonClickExit()
    {
        Application.Quit();
    }
    public void ButtonClickResume()
    {
        shotManager.unloadEscapeScene();
    }
    public void ButtonClickResumeHard()
    {
        shotManagerHard.unloadEscapeScene();
    }
}
