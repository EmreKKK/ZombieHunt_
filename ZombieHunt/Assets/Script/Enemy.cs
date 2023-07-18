//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    [SerializeField] float moveSpeed = 5f;
//    Rigidbody2D _rigidbody;
//    Transform target;
//    Vector2 moveDirection;

//    private void Awake()
//    {
//        _rigidbody = GetComponent<Rigidbody2D>();
//    }
//    void Start()
//    {
//        target = GameObject.Find("Player").transform;
//    }



//    // Update is called once per frame
//    void Update()
//    {
//        if (target)
//        {
//            Vector3 direction = (target.position - transform.position).normalized;
//            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//            _rigidbody.rotation = angle;
//            moveDirection = direction;
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (target)
//        {
//            _rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Bullet bullet;
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D _rigidbody;
    Transform target;
    Vector2 moveDirection;

    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        target = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
        bullet = GetComponent<Bullet>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damageAmount);
                Destroy(collision.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            _rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }






}

