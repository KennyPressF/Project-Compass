using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float lifeTime;
    [SerializeField] float damageValue;
    [SerializeField] float knockbackValue;

    [SerializeField] private float timer;

    PlayerStats stats;

    private void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    private void OnEnable()
    {
        damageValue = stats.Damage;
        knockbackValue = stats.Knockback;
        lifeTime = stats.Range;

        timer = 0f;
    }

    void Update()
    {
        HandleMovement();
        CheckLifetime();
    }

    void HandleMovement()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        //transform.position += Vector3.up * Time.deltaTime; - doesnt account for rotation
    }

    void CheckLifetime()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damageValue, knockbackValue);
            gameObject.SetActive(false);
        }
    }
}
