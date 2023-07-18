using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    PlayerController playerCont;
    public float maxBulletDistance = 40f; // Bullet'�n maksimum mesafesi
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject pistolPrefab;
    public GameObject shotgunPrefab;
    public GameObject riflePrefab;


    private bool isReloading = false; // Reload durumunu takip etmek i�in
    public float bulletForce = 20f;

    public int currentAmmo = 30; // Ba�lang��ta 30 mermiye sahip oldu�unu varsayal�m
    public int maxAmmo = 30; // �arj�rdeki maksimum mermi say�s�

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
                    return; // Reload i�lemi s�ras�nda ate� etmeyi engelle
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
            isReloading = true; // Reload durumunu etkinle�tir

            // �arj�r de�i�tirme animasyonunu tetikle

            int ammoToReload = maxAmmo - currentAmmo;
            currentAmmo = maxAmmo;

            Debug.Log("�arj�r de�i�tirildi. Yeni mermi say�s�: " + currentAmmo);

            StartCoroutine(ResumeShootingAfterReload());
        }
        else
        {
            Debug.Log("�arj�r tam dolu.");
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

        isReloading = false; // Reload durumunu devre d��� b�rak
    }

    //public GameObject bulletPrefab; // Mermi prefab�
    //public float bulletSpeed; // Mermi h�z�
    //public Transform Gun;



    //public void FireBullet(Vector3 shootDirection)
    //{
    //    // Mermi olu�tur
    //    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    //    // Mermiye h�z ve y�n uygula
    //    bullet.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * bulletSpeed;

    //    // Mermiyi fare imlecinin oldu�u y�ne do�ru d�nd�r

    //    float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg-90f;
    //    if (angle < 0f)
    //    {
    //        angle += 360f;
    //    }
    //    bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //}
}
