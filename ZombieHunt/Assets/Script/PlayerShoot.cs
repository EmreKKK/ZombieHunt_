using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    PlayerController playerCont;
    public float maxBulletDistance = 40f; // Bullet'ýn maksimum mesafesi
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject pistolPrefab;
    public GameObject shotgunPrefab;
    public GameObject riflePrefab;


    private bool isReloading = false; // Reload durumunu takip etmek için
    public float bulletForce = 20f;

    public int currentAmmo = 30; // Baþlangýçta 30 mermiye sahip olduðunu varsayalým
    public int maxAmmo = 30; // Þarjördeki maksimum mermi sayýsý

    void Start()
    {
        playerCont = GetComponent<PlayerController>();
    }

    void Update()
    {
    }


    public void ShootAuto(bool isAuto)
    {
        if (isAuto)
        {
            if (Input.GetButton("Fire1"))
            {
                if (isReloading)
                {
                    return; // Reload iþlemi sýrasýnda ateþ etmeyi engelle
                }

                if (currentAmmo > 0)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                    //StartCoroutine(DestroyBulletAfterDistance(bullet));
                    currentAmmo--;
                }
                else
                {
                    playerCont.animator.SetTrigger("IsReload");
                    Reload();
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentAmmo > 0)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                    //StartCoroutine(DestroyBulletAfterDistance(bullet));
                    currentAmmo--;
                }
                else
                {
                    playerCont.animator.SetTrigger("IsReload");
                    Reload();
                }
            }
        }
    }

    public void Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            isReloading = true; // Reload durumunu etkinleþtir

            // Þarjör deðiþtirme animasyonunu tetikle

            int ammoToReload = maxAmmo - currentAmmo;
            currentAmmo = maxAmmo;

            Debug.Log("Þarjör deðiþtirildi. Yeni mermi sayýsý: " + currentAmmo);

            StartCoroutine(ResumeShootingAfterReload());
        }
        else
        {
            Debug.Log("Þarjör tam dolu.");
        }
    }

    //private IEnumerator DestroyBulletAfterDistance(GameObject bullet)
    //{
    //    yield return new WaitUntil(() => Vector2.Distance(transform.position, bullet.transform.position) > maxBulletDistance);
    //    Destroy(bullet);
    //}

    private IEnumerator ResumeShootingAfterReload()
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle

        isReloading = false; // Reload durumunu devre dýþý býrak
    }

    //public GameObject bulletPrefab; // Mermi prefabý
    //public float bulletSpeed; // Mermi hýzý
    //public Transform Gun;



    //public void FireBullet(Vector3 shootDirection)
    //{
    //    // Mermi oluþtur
    //    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    //    // Mermiye hýz ve yön uygula
    //    bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * bulletSpeed;

    //    // Mermiyi fare imlecinin olduðu yöne doðru döndür

    //    float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg-90f;
    //    if (angle < 0f)
    //    {
    //        angle += 360f;
    //    }
    //    bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //}
}
