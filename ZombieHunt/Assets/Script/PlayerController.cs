//using UnityEngine;
//using UnityEngine.EventSystems;

//public class PlayerController : MonoBehaviour
//{
//    private PlayerShoot playerShoot;
//    public float moveSpeed = 5f;

//    private Rigidbody2D rb;

//    public Animator animator;

//    private bool canShoot;



//    private void Awake()
//    {

//        rb = GetComponent<Rigidbody2D>();
//        playerShoot = GetComponent<PlayerShoot>();
//    }

//    private void Update()
//    {
//        Movement();
//        LookAtMouse();
//        Shoot();


//    }

//    private void FireBulletTowardsMouse()
//    {
//        // Fare pozisyonunu dünya koordinatlarýna çevir
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        mousePosition.z = 0f;

//        // Karakterin pozisyonunu dünya koordinatlarýnda al
//        Vector3 characterPosition = transform.position;

//        // Fare pozisyonunu karakter pozisyonuna göre hesapla ve normalize et
//        Vector3 shootDirection = (mousePosition - characterPosition).normalized;

//        // Shooter sýnýfýna mermi ateþleme yönlendirmesini yap
//        playerShoot.FireBullet(shootDirection);
//    }

//    private void Shoot()
//    {
//        if (canShoot)
//        {
//            if (Input.GetMouseButton(0))
//            {
//                animator.SetTrigger("IsShoot");
//                FireBulletTowardsMouse();
//            }
//        }
//    }

//    private void Movement()
//    {
//        // Yatay ve dikey giriþleri al
//        float moveHorizontal = Input.GetAxis("Horizontal");
//        float moveVertical = Input.GetAxis("Vertical");

//        // Hareket vektörünü oluþtur
//        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

//        // Hareket vektörünü normalize et (uzunluðunu 1'e sabitle)
//        movement.Normalize();

//        // Rigidbody'yi kullanarak karakteri hareket ettir
//        rb.velocity = movement * moveSpeed;
//        Animation(movement);
//    }

//    private void LookAtMouse()
//    {
//        // Fare pozisyonunu dünya koordinatlarýna çevir
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//        // Karakterin pozisyonunu dünya koordinatlarýnda al
//        Vector3 characterPosition = transform.position;

//        // Fare pozisyonunu karakter pozisyonuna göre hesapla
//        Vector3 lookDirection = mousePosition - characterPosition;

//        // Hedef yönün açýsýný radyan olarak hesapla
//        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

//        // Karakteri yönünü güncelle
//        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//    }

//    private void Animation(Vector2 movement)
//    {
//        // Animasyon kodu

//        animator.SetFloat("AnimMoveX", movement.x);
//        animator.SetFloat("AnimMoveY", movement.y);

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            animator.SetTrigger("IsMeleeAttack");
//        }
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            animator.SetTrigger("IsReload");
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            animator.SetTrigger("IsFlashlight");
//            canShoot = false; // Ateþ etmeyi devre dýþý býrak
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            animator.SetTrigger("IsKnife");
//            canShoot = false; // Ateþ etmeyi devre dýþý býrak
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha3))
//        {
//            animator.SetTrigger("IsHandgun");
//            canShoot = true; // Ateþ etmeyi etkinleþtir
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha4))
//        {
//            animator.SetTrigger("IsShotgun");
//            canShoot = true; // Ateþ etmeyi etkinleþtir
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha5))
//        {
//            animator.SetTrigger("IsRifle");
//            canShoot = true; // Ateþ etmeyi etkinleþtir
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D rigid;
    Vector2 movement;
    Vector2 mousePos;
    public Camera came;

    public Animator animator;
    private PlayerShoot playerShoot;
    bool isAuto;

    private bool canShoot;

    public int damageAmount;

    Enemy enemy;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePos = came.ScreenToWorldPoint(Input.mousePosition);

        Animation(movement);

        if (canShoot)
        {
            playerShoot.ShootAuto(isAuto);
        }
    }

    public void Animation(Vector2 movement)
    {
        // Animasyon kodu

        animator.SetFloat("AnimMoveX", movement.x);
        animator.SetFloat("AnimMoveY", movement.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("IsMeleeAttack");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("IsReload");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("IsFlashlight");
            canShoot = false; // Ateþ etmeyi devre dýþý býrak
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetTrigger("IsKnife");
            canShoot = false; // Ateþ etmeyi devre dýþý býrak
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetTrigger("IsHandgun");
            canShoot = true; // Ateþ etmeyi etkinleþtir
            if ((playerShoot.bulletPrefab = playerShoot.shotgunPrefab) || (playerShoot.bulletPrefab = playerShoot.riflePrefab))
            {
                playerShoot.bulletPrefab = playerShoot.pistolPrefab;
                playerShoot.currentAmmo = 12;
                playerShoot.maxAmmo = 12;
                isAuto = false;
                damageAmount = 20;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animator.SetTrigger("IsShotgun");
            canShoot = true; // Ateþ etmeyi etkinleþtir
            playerShoot.bulletPrefab = playerShoot.shotgunPrefab;
            playerShoot.currentAmmo = 7;
            playerShoot.maxAmmo = 7;
            isAuto = false;
            damageAmount = 50;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            animator.SetTrigger("IsRifle");
            canShoot = true; // Ateþ etmeyi etkinleþtir
            playerShoot.bulletPrefab = playerShoot.riflePrefab;
            playerShoot.currentAmmo = 30;
            playerShoot.maxAmmo = 30;
            isAuto = true;
            damageAmount = 35;
        }
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePos - rigid.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rigid.rotation = angle;


    }
}
