using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //int pistolDamage = 20;
    //int shotgunDamage = 50;
    //int rifleDamage = 35;
    Enemy enemy;
    PlayerController playerController;


    public int damageAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Update()
    {

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if ((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
        {
            if (CompareTag("Bullet"))

            {
                Destroy(gameObject);
            }
        }
    }


}
