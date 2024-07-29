using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System;
using TMPro;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class ShotManagerHard : MonoBehaviour
{
    BoxCollider2D box2d;
    float verticalVelocity = 0f;
    [SerializeField] protected SpriteRenderer hookSpr;
    public GameObject hookPrefab;// 후크 프리팹
    public float hookSpeed = 10f;  // 후크 발사 속도
    public LayerMask objectLayer;  // 후크가 충돌할 레이어 설정
    Camera mainCam;
    [SerializeField] Transform launchPoint;
    private GameObject currentHook;
    [SerializeField] Transform trsDynamic;
    [SerializeField] bool isShot = true;
    [SerializeField] bool isGround = false;
    [SerializeField] Transform trsHand;
    [SerializeField] Transform trsWeapon;
    [SerializeField] Transform player;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    [Header("게이지 시스템")]
    [SerializeField] float playerForce;
    [SerializeField] Image gauge;
    [SerializeField] GameObject gaugecanvas;
    [SerializeField] bool isGauge = true;
    [SerializeField] bool isGaugeCheck = true;
    [SerializeField] protected bool isHooked = false;

    public Hook hook;
    [Header("이동")]
    Vector3 moveDir;
    [Header("후크")]
    [SerializeField] float hookTimer = 0;
    public GameObject hookGameObject;
    [SerializeField] AudioSource hookSound;

    [Header("실시간 플레이타임")]
    [SerializeField] float playTimer = 0;
    [SerializeField] TMP_Text[] tmpTimer = new TMP_Text[3];

    [Header("체크포인트")]
    [SerializeField] Transform trsCheckPoint;
    [SerializeField] Vector3 trsPlayerCheckPoint;
    [SerializeField] private GameObject goCheckPoint1;
    private CheckPointSk checkPoint;

    [Header("ESC")]
    private bool isEscapeSceneLoaded = false;
    Button button;

    private void Awake()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gaugecanvas.active = false;
        hook = FindObjectOfType<Hook>();
        box2d = GetComponent<BoxCollider2D>();
        checkPoint = FindObjectOfType<CheckPointSk>();
        button = FindObjectOfType<Button>();
    }

    void Start()
    {

    }
    public void checkTimer()
    {
        if (hookTimer > 0)
        {
            hookTimer -= 3 * Time.deltaTime;
            if (hookTimer < 0)
            {
                hookTimer = 0;

            }
        }
        if (hookTimer == 0 && isHooked == false && isShot == false)
        {
            if (currentHook != null)
            {
                currentHook.GetComponent<Hook>().RemoveHook();
                isShot = true;
                hookSpr.enabled = true;
            }
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleEscapeScene();
        }
        returnToCheckPoint();
        playTime();
        checkTimer();
        checkGround();

        if (isHooked == true)
        {
            gaugecanvas.active = true;
            gaugeSystem();
        }

        if (isShot == true && isGround == true)
        {
            checkAim();
            checkPlayerPos();
        }

        if (isGround == true && isShot == true && Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭시 후크 발사, 후크 스프라이트 off, 후크 타이머 작동
        {
            hookSpr.enabled = false;
            shootHook();
            isShot = false;
            hookTimer = 2f;
        }
    }
    public void shootHook()//마우스 포인터대로 후크를 발사
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 worldMousePosition = mainCam.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        currentHook = Instantiate(hookPrefab, launchPoint.position, Quaternion.identity, trsDynamic);
        currentHook.GetComponent<Rigidbody2D>().velocity = direction * hookSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentHook.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //후크가 Object 레이어에 충돌할 때의 처리를 위해 콜백 함수 등록
        currentHook.GetComponent<Hook>().Setup(objectLayer);

        hookSound.Play();
    }
    private void checkAim()//마우스 포인터의 방향에 따라 플레이어의 무기 방향 설정
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;

        float angle = Quaternion.FromToRotation(transform.localScale.x < 0 ? Vector3.left : Vector3.right, fixedPos).eulerAngles.z;
        trsHand.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void checkPlayerPos()//마우스 포인터의 방향에 따라 플레이어가 왼쪽 또는 오른쪽을 보게함
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
    public void gaugeSystem()//게이지가 0일경우 1까지 플러스
    {
        if (isGauge == true && isGaugeCheck == true)
        {
            gauge.fillAmount += 0.01f;
            if (gauge.fillAmount == 1f)
            {
                isGauge = false;
            }
        }
        if (isGauge == false && isGaugeCheck == true)
        {
            gauge.fillAmount -= 0.01f;
            if (gauge.fillAmount == 0f)
            {
                isGauge = true;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            playerForce = gauge.fillAmount;
            isGaugeCheck = false;
            isHooked = false;
            isShot = true;
            gaugecanvas.active = false;
            hookSpr.enabled = true;
            gauge.fillAmount = 0f;
            isGaugeCheck = true;
            if (currentHook != null)
            {
                currentHook.GetComponent<Hook>().RemoveHook();
            }
            Vector3 scale = transform.localScale;
            if (scale.x > 0)
            {
                moveDir.x = playerForce * 8;
                moveDir.y = playerForce * 20;
                rigid.velocity = moveDir;
            }
            if (scale.x < 0)
            {
                moveDir.x = playerForce * -8;
                moveDir.y = playerForce * 20;
                rigid.velocity = moveDir;
            }
        }
    }
    public void onHookTriggered()
    {
        isHooked = true;
    }
    public bool getisHooked()
    {
        return isHooked;
    }

    public void setIsHooked(bool value)
    {
        isHooked = value;
    }
    private void checkGround()
    {
        isGround = false;
        if (verticalVelocity > 0f)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Object"));
        if (hit)
        {
            isGround = true;
        }
    }
    private void playTime()
    {
        playTimer += Time.deltaTime;
        float Timer = playTimer;
        tmpTimer[0].text = ((int)Timer / 3600).ToString("D2");
        tmpTimer[1].text = ((int)Timer / 60 % 60).ToString("D2");
        tmpTimer[2].text = ((int)Timer % 60).ToString("D2");
    }
    public void returnToCheckPoint()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 checkPoint = trsPlayerCheckPoint;
            player.transform.position = checkPoint;
            moveDir.x = 0;
            moveDir.y = 0;
            rigid.velocity = moveDir;
            isGaugeCheck = false;
            isHooked = false;
            isShot = true;
            gaugecanvas.active = false;
            hookSpr.enabled = true;
            gauge.fillAmount = 0f;
            isGaugeCheck = true;
            if (currentHook != null)
            {
                currentHook.GetComponent<Hook>().RemoveHook();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D CheckPoint)
    {
        if (CheckPoint.tag == "CheckPoint")
        {
            goCheckPoint1.tag = ("Untagged");
            checkPoint.setColor();
            trsPlayerCheckPoint = goCheckPoint1.transform.position;
        }
    }
    void toggleEscapeScene()
    {
        if (isEscapeSceneLoaded)
        {
            unloadEscapeScene();
        }
        else
        {
            loadEscapeScene();
        }
    }

    private void loadEscapeScene()
    {
        SceneManager.LoadScene("EscapeSceneHard", LoadSceneMode.Additive);
        isEscapeSceneLoaded = true;
        isShot = false;
        isHooked = false;
    }

    public void unloadEscapeScene()
    {
        SceneManager.UnloadSceneAsync("EscapeSceneHard");
        isEscapeSceneLoaded = false;
        isHooked = false;
        isShot = true;
        gaugecanvas.active = false;
        hookSpr.enabled = true;
        if (currentHook != null)
        {
            currentHook.GetComponent<Hook>().RemoveHook();
        }

    }
}
