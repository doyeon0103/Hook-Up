using TMPro;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private LayerMask objectLayer;
    private bool isHooked = false;
    public ShotManager shotManager;
    public ShotManagerHard shotManagerHard;
    public Vector3 hookDir;
    [SerializeField] GameObject hook;
    public void Setup(LayerMask layer)
    {
        objectLayer = layer;
    }
    private void Start()
    {
        shotManager = FindObjectOfType<ShotManager>();
        shotManagerHard = FindObjectOfType<ShotManagerHard>();
    }
    public void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHooked && ((1 << collision.gameObject.layer) & objectLayer) != 0)
        {
            isHooked = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;


            // 후크를 충돌한 오브젝트에 고정
            transform.parent = collision.transform;
            if (shotManager != null)//shotmanager의 함수 실행
            {
                shotManager.onHookTriggered();
            }
            else if (shotManagerHard != null)
            {
                shotManagerHard.onHookTriggered();
            }
        }
    }

    public void RemoveHook()
    {
        Destroy(gameObject);
    }
}
